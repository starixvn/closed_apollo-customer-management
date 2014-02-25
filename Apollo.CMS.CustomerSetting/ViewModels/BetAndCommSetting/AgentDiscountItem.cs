// --------------------------------------------------------------------------
// <copyright file="AgentDiscountItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting
{
    using Apollo.CMS.CustomerSetting.Constants;
    using Apollo.CMS.CustomerSetting.Models.BetAndCommSetting;
    using Fanex.Common;

    /// <summary>
    /// Commission item for customer of agent level.
    /// </summary>
    public class AgentDiscountItem
    {
        public AgentDiscountItem()
        {
        }

        public AgentDiscountItem(params AgentDiscountInfo[] discountList)
        {
            if (discountList != null)
            {
                AgentDiscountInfo discountInfo = null;

                if (discountList.Length > 0)
                {
                    discountInfo = discountList[0];

                    if (discountInfo != null)
                    {
                        this.SportType = discountInfo.SportType;
                        this.BetType = discountInfo.BetType;
                        this.DiscountA = (discountInfo.DiscountD * 100).ToString(SettingConstants.DecimalFormat);
                        this.DiscountB = (discountInfo.DiscountC * 100).ToString(SettingConstants.DecimalFormat);
                        this.DiscountC = (discountInfo.DiscountB * 100).ToString(SettingConstants.DecimalFormat);
                        this.DiscountD = (discountInfo.DiscountA * 100).ToString(SettingConstants.DecimalFormat);
                        this.UplineMinDiscountA = (Converter.ToDecimal(discountInfo.UplineMinDiscountD) * 100).ToString(SettingConstants.DecimalFormat);
                        this.UplineMinDiscountB = (Converter.ToDecimal(discountInfo.UplineMinDiscountC) * 100).ToString(SettingConstants.DecimalFormat);
                        this.UplineMinDiscountC = (Converter.ToDecimal(discountInfo.UplineMinDiscountB) * 100).ToString(SettingConstants.DecimalFormat);
                        this.UplineMinDiscountD = (Converter.ToDecimal(discountInfo.UplineMinDiscountA) * 100).ToString(SettingConstants.DecimalFormat);
                    }
                }

                if (discountList.Length > 1)
                {
                    discountInfo = discountList[1];

                    if (discountInfo != null)
                    {
                        this.Discount1x2 = (discountInfo.DiscountA * 100).ToString(SettingConstants.DecimalFormat);
                        this.UplineMinDiscount1x2 = (Converter.ToDecimal(discountInfo.UplineMinDiscountA) * 100).ToString(SettingConstants.DecimalFormat);
                    }
                }

                if (discountList.Length > 2)
                {
                    discountInfo = discountList[2];

                    if (discountInfo != null)
                    {
                        this.DiscountOthers = (discountInfo.DiscountA * 100).ToString(SettingConstants.DecimalFormat);
                        this.UplineMinDiscountOthers = (Converter.ToDecimal(discountInfo.UplineMinDiscountA) * 100).ToString(SettingConstants.DecimalFormat);
                    }
                }
            }
        }

        public short SportType { get; set; }

        public short BetType { get; set; }

        public string DiscountA { get; set; }

        public string DiscountB { get; set; }

        public string DiscountC { get; set; }

        public string DiscountD { get; set; }

        public string Discount1x2 { get; set; }

        public string DiscountOthers { get; set; }

        public string UplineMinDiscountA { get; set; }

        public string UplineMinDiscountB { get; set; }

        public string UplineMinDiscountC { get; set; }

        public string UplineMinDiscountD { get; set; }

        public string UplineMinDiscount1x2 { get; set; }

        public string UplineMinDiscountOthers { get; set; }
    }
}