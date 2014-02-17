// -----------------------------------------------------------------------
// <copyright file="MemberBetListService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Services.Implements
{
    using System.Collections.Generic;
    using System.Linq;
    using Apollo.BetList.Extensions;
    using Apollo.BetList.Extensions.RTFHelpers;
    using Apollo.CMS.BetList.Criteria.MemberBetList;
    using Apollo.CMS.BetList.Helpers;
    using Apollo.CMS.BetList.Models.GeneralBetTicket;
    using Apollo.CMS.BetList.Models.MemberBetList;
    using Apollo.CMS.BetList.Repositories.Implements;
    using Apollo.CMS.BetList.Repositories.Interfaces;
    using Apollo.CMS.BetList.Services.Interfaces;
    using Apollo.CMS.BetList.ViewModels.GeneralBetTicket;
    using Apollo.CMS.BetList.ViewModels.MemberBetList;
    using Apollo.CMS.Core.Constants;
    using Apollo.CMS.Core.ExcelUtility;
    using Apollo.CMS.Core.Models;
    using Apollo.CMS.Core.Resources;
    using Apollo.CMS.Core.Services;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.Common;
    using Fanex.NAP.Core.Web;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Member's bet list service.
    /// </summary>
    public class MemberBetListService : BaseService, IMemberBetListService
    {
        private static List<BetTicket> _ticketList;
        private readonly IMemberBetListRepository _memberBetListRepository;

        public MemberBetListService() :
            this(null)
        {
        }

        public MemberBetListService(IMemberBetListRepository memberBetListRepository)
        {
            this._memberBetListRepository = memberBetListRepository ?? new MemberBetListRepository();
        }

        /// <summary>
        /// Gets the paged bet list.
        /// </summary>
        /// <param name="filter">The criteria that are used to query data.</param>
        /// <returns>The bet items of a page.</returns>
        public BetList GetPagedBetList(MemberBetListFilter filter)
        {
            BetList result = null;

            if (filter.IsLoadNew || _ticketList == null)
            {
                GetBetTicket(filter);
            }

            if (_ticketList.Any())
            {
                result = SeparateBetListToPage(filter);
            }

            return result;
        }

        /// <summary>
        /// Gets the detailed win loss of the whole line.
        /// </summary>
        /// <param name="filter">The criteria that are used to query data.</param>
        /// <returns>The win loss list.</returns>
        public IEnumerable<DetailedWinLossItem> GetDetailWinLossList(DetailedWinLossFilter filter)
        {
            BalanceUp balanceUp = this.BalanceUp;
            var detailedWinLossCriteria = new GetDetailedWinLossCriteria
            {
                TransId = filter.TransId,
                IsBackupData = balanceUp.BalanceUpDay > filter.TransDate
            };
            List<DetailedWinLossItem> result = null;
            IEnumerable<DetailedWinLoss> rawData = this.GetListData<DetailedWinLoss>(detailedWinLossCriteria);

            if ((rawData != null) && rawData.Any())
            {
                result = new List<DetailedWinLossItem>();

                foreach (DetailedWinLoss record in rawData)
                {
                    result.Add(new DetailedWinLossItem
                    {
                        Username = record.Username,
                        PositionTaking = record.PositionTaking,
                        WinLoss = record.WinLoss,
                        Discount = record.Discount,
                        Comm = record.Comm,
                        IsSuper = Converter.ToInt(record.AgentLevel) == (int)HighLevelAgents.Super
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the parlay sub tickets.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The HTML formatted string that represents the sub-tickets.</returns>
        public string GetParlaySubTickets(GetParlaySubTicketsCriteria criteria)
        {
            IEnumerable<ParlaySubTicket> rawData = this.GetListData<ParlaySubTicket>(criteria);

            return BetListHelper.ConvertParlaySubTicketList(rawData);
        }

        /// <summary>
        /// Gets the system parlay details.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The SystemParlayDetails object that represents the system parlay details.</returns>
        public SystemParlayDetails GetSystemParlayDetails(GetSystemParlayDetailsCriteria criteria)
        {
            BalanceUp balanceUp = this.BalanceUp;
            string result = string.Empty;

            criteria.BalanceUpDay = balanceUp.BalanceUpDay;
            criteria.MoveBetsDay = balanceUp.MoveBetsDay;

            return SystemParlayDetailsHelper.Map(this._memberBetListRepository.GetSystemParlayData(criteria));
        }

        /// <summary>
        /// Exports the member bet list report to Excel file.
        /// </summary>
        /// <param name="filter">The criteria that are used to query data.</param>
        /// <param name="templatePath">The template's path.</param>
        public void ExportMemberBetList(MemberBetListFilter filter, string templatePath)
        {
            string fileName = string.Format(
                                         ViewLang.BetListReportFileName,
                                         filter.Username,
                                         filter.FromDate.ToShortFormatedDateForFileName(),
                                         filter.ToDate.ToShortFormatedDateForFileName());
            string reportTitle = string.Format(
                                         ViewLang.BetListReportTitle,
                                         filter.FromDate.ToShortFormatedDate(),
                                         filter.ToDate.ToShortFormatedDate(),
                                         filter.Username);
            var writer = new ExcelTemplateWriter(templatePath, fileName, fileName);

            writer.WriteRow(0, new { reportTitle });
            writer.CopyRows(1, 2);

            if (_ticketList == null || _ticketList.Count == 0)
            {
                GetBetTicket(filter);
            }

            FillBetTickets(ref writer);
            FillTotalRow(ref writer);
            writer.CopyRows(6, 1);
            writer.SyncColumnsWidth(9);
            writer.AutosizeColumns(9);
            writer.AutosizeRows(3);
            writer.Render();
        }

        /// <summary>
        /// Fills the bet tickets.
        /// </summary>
        /// <param name="writer">The writer using to write data.</param>
        private void FillBetTickets(ref ExcelTemplateWriter writer)
        {
            BetListExcelGenerator excelGenerator = new BetListExcelGenerator();
            ITicketHelper ticketHelper = new TicketHelper();

            TransTimeRTFHelper transTimeRTFHelper = GetTransTimeRTFHelper(writer);
            ChoiceRTFHelper choiceRTFHelper = GetChoiceRTFHelper(writer);
            OddsRTFHelper oddsRTFHelper = GetOddsRTFHelper(writer);
            StatusRTFHelper statusRTFHelper = GetStatusRTFHelper(writer);
            NumberRTFHelper numberRTFHelper = new NumberRTFHelper
            {
                PositiveFont = oddsRTFHelper.PositiveFont,
                NegativeFont = oddsRTFHelper.NegativeFont
            };

            List<ITicket> tickets = BetListHelper.Map(_ticketList);
            ITicket ticket = null;

            for (int index = 0; index < tickets.Count; index++)
            {
                ticket = tickets[index];
                transTimeRTFHelper.RTFRenderer.Clear();
                choiceRTFHelper.RTFRenderer.Clear();
                oddsRTFHelper.RTFRenderer.Clear();
                statusRTFHelper.RTFRenderer.Clear();

                var row = new object[]
                {
                    index + 1,
                    excelGenerator.BuildTransTime(ticket, transTimeRTFHelper),
                    excelGenerator.BuildChoice(ticket, ticketHelper, choiceRTFHelper),
                    excelGenerator.BuildOdds(ticket, oddsRTFHelper),
                    ticket.Stake,
                    FormatWinLoss(ticket.Winlost, ticket.Comm, numberRTFHelper),
                    excelGenerator.BuildStatus(ticket, ticketHelper, statusRTFHelper, true),
                    FormatCompanyShare(ticket.SuperPositionTaking, ticket.SuperWinlost, numberRTFHelper),
                    FormatCompanyComm(ViewLang.NA, ticket.SuperComm, numberRTFHelper)
                };
                writer.WriteRow(3, row);
            }
        }

        /// <summary>
        /// Fills the total row.
        /// </summary>
        /// <param name="writer">The writer using to write data.</param>
        private void FillTotalRow(ref ExcelTemplateWriter writer)
        {
            NumberRTFHelper numberRTFHelper = GetTotalOddsRTFHelper(writer);

            decimal sumStake = Converter.ToDecimal(_ticketList.Sum(obj => obj.Stake));
            decimal sumWinLoss = 0;
            decimal sumComm = Converter.ToDecimal(_ticketList.Sum(obj => obj.PlayerComm));
            decimal sumCompanyWinLoss = 0;
            decimal sumCompanyComm = Converter.ToDecimal(_ticketList.Sum(obj => obj.CompanyComm));
            string ticketStatus = string.Empty;

            foreach (BetTicket ticket in _ticketList)
            {
                ticketStatus = ticket.Status.ToLower();
                if (!BetStatus.Running.Equals(ticketStatus) && !BetStatus.Waiting.Equals(ticketStatus))
                {
                    sumWinLoss += Converter.ToDecimal(ticket.WinLoss);
                    sumCompanyWinLoss += Converter.ToDecimal(ticket.CompanyWinLoss);
                }
            }

            var row = new object[]
            {
                string.Empty,
                string.Empty,
                _ticketList.Count,
                string.Empty,
                sumStake,
                FormatWinLoss(sumWinLoss, sumComm, numberRTFHelper),
                string.Empty,
                sumCompanyWinLoss,
                sumCompanyComm
            };

            writer.WriteRow(5, row);
        }

        /// <summary>
        /// Gets the transaction time column's fonts.
        /// </summary>
        /// <param name="writer">The writer that is used to get the font.</param>
        /// <returns>TransTimeRTFHelper object.</returns>
        private TransTimeRTFHelper GetTransTimeRTFHelper(ExcelTemplateWriter writer)
        {
            TransTimeRTFHelper transTimeFonts = new TransTimeRTFHelper();

            transTimeFonts.RefNoFont = writer.GetFont(7, 1);
            transTimeFonts.TransTimeFont = writer.GetFont(8, 1);

            return transTimeFonts;
        }

        /// <summary>
        /// Gets the choice column's fonts.
        /// </summary>
        /// <param name="writer">The writer that is used to get the font.</param>
        /// <returns>ChoiceRTFHelper object.</returns>
        private ChoiceRTFHelper GetChoiceRTFHelper(ExcelTemplateWriter writer)
        {
            ChoiceRTFHelper choiceFonts = new ChoiceRTFHelper();

            choiceFonts.FavoriteFont = writer.GetFont(7, 2);
            choiceFonts.UnderdogFont = writer.GetFont(8, 2);
            choiceFonts.HandicapFont = writer.GetFont(9, 2);
            choiceFonts.LiveScoreFont = writer.GetFont(10, 2);
            choiceFonts.BetTypeFont = writer.GetFont(11, 2);
            choiceFonts.HomeTeamFont = writer.GetFont(12, 2);
            choiceFonts.AwayTeamFont = choiceFonts.HomeTeamFont;
            choiceFonts.VsFont = choiceFonts.HomeTeamFont;
            choiceFonts.FirstGoalFont = writer.GetFont(13, 2);
            choiceFonts.LastGoalFont = writer.GetFont(14, 2);
            choiceFonts.SportFont = writer.GetFont(15, 2);
            choiceFonts.LeagueFont = writer.GetFont(16, 2);

            return choiceFonts;
        }

        /// <summary>
        /// Gets the odds column's fonts.
        /// </summary>
        /// <param name="writer">The writer that is used to get the font.</param>
        /// <returns>OddsRTFHelper object.</returns>
        private OddsRTFHelper GetOddsRTFHelper(ExcelTemplateWriter writer)
        {
            OddsRTFHelper oddsFonts = new OddsRTFHelper();

            oddsFonts.NegativeFont = writer.GetFont(7, 3);
            oddsFonts.PositiveFont = writer.GetFont(8, 3);
            oddsFonts.OddsNameFont = writer.GetFont(9, 3);

            return oddsFonts;
        }

        /// <summary>
        /// Gets the status column's fonts.
        /// </summary>
        /// <param name="writer">The writer that is used to get the font.</param>
        /// <returns>StatusRTFHelper object.</returns>
        private StatusRTFHelper GetStatusRTFHelper(ExcelTemplateWriter writer)
        {
            StatusRTFHelper statusFonts = new StatusRTFHelper();

            statusFonts.StatusFont = writer.GetFont(7, 6);
            statusFonts.IPFont = writer.GetFont(8, 6);

            return statusFonts;
        }

        /// <summary>
        /// Gets the total odds row's fonts.
        /// </summary>
        /// <param name="writer">The writer that is used to get the font.</param>
        /// <returns>NumberRTFHelper object.</returns>
        private NumberRTFHelper GetTotalOddsRTFHelper(ExcelTemplateWriter writer)
        {
            NumberRTFHelper numberFonts = new NumberRTFHelper();

            numberFonts.NegativeFont = writer.GetFont(10, 3);
            numberFonts.PositiveFont = writer.GetFont(11, 3);

            return numberFonts;
        }

        /// <summary>
        /// Formats the win loss.
        /// </summary>
        /// <param name="winLoss">The win loss value.</param>
        /// <param name="comm">The commission value.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object.</returns>
        private IRichTextString FormatWinLoss(decimal winLoss, decimal comm, NumberRTFHelper rtfHelper)
        {
            IFont winLossFont = winLoss < 0 ? rtfHelper.NegativeFont : rtfHelper.PositiveFont;
            IFont commFont = comm < 0 ? rtfHelper.NegativeFont : rtfHelper.PositiveFont;

            rtfHelper.RTFRenderer.Clear();
            rtfHelper.RTFRenderer.AddText(winLoss.Format(2), winLossFont);
            rtfHelper.RTFRenderer.AddText(string.Format("\n{0}", comm.Format(2)), commFont);

            return rtfHelper.RTFRenderer.Render();
        }

        /// <summary>
        /// Formats the company share.
        /// </summary>
        /// <param name="share">The share value.</param>
        /// <param name="winLoss">The win loss value.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object.</returns>
        private IRichTextString FormatCompanyShare(decimal share, decimal winLoss, NumberRTFHelper rtfHelper)
        {
            IFont shareFont = share < 0 ? rtfHelper.NegativeFont : rtfHelper.PositiveFont;
            IFont winLossFont = winLoss < 0 ? rtfHelper.NegativeFont : rtfHelper.PositiveFont;

            rtfHelper.RTFRenderer.Clear();
            rtfHelper.RTFRenderer.AddText(string.Format("{0}%\n", (share * 100).Format(2)), shareFont);
            rtfHelper.RTFRenderer.AddText(winLoss.Format(2), winLossFont);

            return rtfHelper.RTFRenderer.Render();
        }

        /// <summary>
        /// Formats the company commission.
        /// </summary>
        /// <param name="discount">The discount value.</param>
        /// <param name="comm">The commission value.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object.</returns>
        private IRichTextString FormatCompanyComm(string discount, decimal comm, NumberRTFHelper rtfHelper)
        {
            IFont commFont = comm < 0 ? rtfHelper.NegativeFont : rtfHelper.PositiveFont;

            rtfHelper.RTFRenderer.Clear();
            rtfHelper.RTFRenderer.AddText(string.Format("{0}\n", discount), rtfHelper.PositiveFont);
            rtfHelper.RTFRenderer.AddText(comm.Format(2), commFont);

            return rtfHelper.RTFRenderer.Render();
        }

        /// <summary>
        /// Separate the bet list into pages.
        /// </summary>
        /// <param name="filter">The filter that is used to query data.</param>
        /// <returns>Formatted bet list.</returns>
        private BetList SeparateBetListToPage(MemberBetListFilter filter)
        {
            BetList result = null;
            List<BetTicket> pagedData = null;
            int startIndex = filter.PageIndex * filter.PageSize;
            int endIndex = startIndex + filter.PageSize;

            endIndex = endIndex > _ticketList.Count ? _ticketList.Count : endIndex;
            pagedData = _ticketList.GetRange(startIndex, endIndex - startIndex);
            result = BetListHelper.ConvertToBetList(pagedData);
            result.TotalItems = _ticketList.Count;

            return result;
        }

        /// <summary>
        /// Gets the bet ticket list.
        /// </summary>
        /// <param name="filter">The filter that is used to query data.</param>
        private void GetBetTicket(MemberBetListFilter filter)
        {
            BalanceUp balanceUp = this.BalanceUp;
            var betListCriteria = new GetMemberBetListCriteria
            {
                MoveBetsDay = balanceUp.MoveBetsDay,
                CustId = filter.CustId
            };

            _ticketList = new List<BetTicket>();

            if ((filter.FromDate < balanceUp.BalanceUpDay) && (balanceUp.BalanceUpDay < filter.ToDate))
            {
                // Gets data from archive database.
                betListCriteria.FromDate = filter.FromDate;
                betListCriteria.ToDate = balanceUp.BalanceUpDay.AddDays(-1);
                betListCriteria.IsBackupData = true;
                _ticketList.AddRange(this.GetListData<BetTicket>(betListCriteria));

                // Gets data from current using database.
                betListCriteria.FromDate = balanceUp.BalanceUpDay;
                betListCriteria.ToDate = filter.ToDate;
                betListCriteria.IsBackupData = false;
                _ticketList.AddRange(this.GetListData<BetTicket>(betListCriteria));
            }
            else
            {
                betListCriteria.FromDate = filter.FromDate;
                betListCriteria.ToDate = filter.ToDate;
                betListCriteria.IsBackupData = balanceUp.BalanceUpDay > filter.ToDate;
                _ticketList.AddRange(this.GetListData<BetTicket>(betListCriteria));
                _ticketList = _ticketList.OrderBy(obj => obj.TransId).ToList();
            }
        }
    }
}