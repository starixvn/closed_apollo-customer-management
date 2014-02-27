// --------------------------------------------------------------------------
// <copyright file="UpdateBetAndCommFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.26">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Used to save updated commissions and bet limits information from client.
    /// </summary>
    [Serializable]
    public class UpdateBetAndCommFilter
    {
        public int AdminId { get; set; }

        public IEnumerable<UpdatedCommItem> Discounts { get; set; }

        public IEnumerable<UpdatedbetLimitItem> BetLimits { get; set; }

        public string Group { get; set; }

        public int UplineId { get; set; }

        public int CustId { get; set; }

        public bool IsMember { get; set; }

        /// <summary>
        /// Convert list of updated discount to XML.
        /// </summary>
        /// <returns>XML string indicates discounts.</returns>
        public string ConvertDiscounts()
        {
            string result = string.Empty;

            if (this.Discounts != null && this.Discounts.Any())
            {
                StringBuilder discountBuilder = new StringBuilder("<ROOT>");
                string memberDiscountNode = "<Discount SportType=\"{0}\" BetType=\"{1}\" Discount=\"{2}\"></Discount>";
                string agentDiscountNode =
                    "<Discount SportType=\"{0}\" BetType=\"{1}\" DiscountA=\"{2}\" DiscountB=\"{3}\" DiscountC=\"{4}\" DiscountD=\"{5}\"></Discount>";

                if (this.IsMember)
                {
                    foreach (UpdatedCommItem item in this.Discounts)
                    {
                        discountBuilder.AppendFormat(memberDiscountNode, item.SportType, item.BetType, item.DiscountA / 100);
                    }
                }
                else
                {
                    foreach (UpdatedCommItem item in this.Discounts)
                    {
                        discountBuilder.AppendFormat(
                                            agentDiscountNode,
                                            item.SportType,
                                            item.BetType,
                                            item.DiscountD / 100,
                                            item.DiscountC / 100,
                                            item.DiscountB / 100,
                                            item.DiscountA / 100);
                    }
                }

                discountBuilder.Append("</ROOT>");
                result = discountBuilder.ToString();
            }

            return result;
        }

        /// <summary>
        /// Convert list of updated bet limits to XML.
        /// </summary>
        /// <returns>XML string indicates bet limits.</returns>
        public string ConvertBetLimits()
        {
            string result = string.Empty;

            if (this.BetLimits != null && this.BetLimits.Any())
            {
                StringBuilder betLimitBuilder = new StringBuilder("<ROOT>");
                string betLimitNode = "<BetLimit SportType=\"{0}\" BetType=\"{1}\" Min=\"{2}\" Max=\"{3}\" MaxPerMatch=\"{4}\"></BetLimit>";

                foreach (UpdatedbetLimitItem item in this.BetLimits)
                {
                    betLimitBuilder.AppendFormat(betLimitNode, item.SportType, item.BetType, item.Min, item.Max, item.MaxPerMatch);
                }

                betLimitBuilder.Append("</ROOT>");
                result = betLimitBuilder.ToString();
            }

            return result;
        }

        /// <summary>
        /// Gets the actual group of the customer.
        /// </summary>
        /// <returns>The actual group.</returns>
        public string GetActualGroup()
        {
            string actualGroup = string.Empty;

            switch (this.Group)
            {
                case "a":
                    actualGroup = "d";
                    break;

                case "b":
                    actualGroup = "c";
                    break;

                case "c":
                    actualGroup = "b";
                    break;

                case "d":
                    actualGroup = "a";
                    break;
            }

            return actualGroup;
        }
    }
}