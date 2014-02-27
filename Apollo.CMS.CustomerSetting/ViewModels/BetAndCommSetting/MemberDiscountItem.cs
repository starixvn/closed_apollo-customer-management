// --------------------------------------------------------------------------
// <copyright file="MemberDiscountItem.cs" company="Nexcel Solutions Vietnam">
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
    /// Commission item for customer who is a member.
    /// </summary>
    public class MemberDiscountItem
    {
        public MemberDiscountItem()
        {
        }

        public MemberDiscountItem(string group, params CustomerDiscountInfo[] discountList)
        {
            if (discountList != null)
            {
                CustomerDiscountInfo discountInfo = null;

                if (discountList.Length > 0)
                {
                    discountInfo = discountList[0];

                    if (discountInfo != null)
                    {
                        this.SportType = discountInfo.SportType;
                        this.BetType = discountInfo.BetType;
                        this.Group = GetActualGroup(group);
                        this.DiscountGroup = (discountInfo.Discount * 100).ToString(SettingConstants.DecimalFormat);
                        this.UplineMinDiscountGroup = (GetUplineGroupComm(discountInfo) * 100).ToString(SettingConstants.DecimalFormat);
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
                        this.SportType1x2 = discountInfo.SportType;
                        this.BetType1x2 = discountInfo.BetType;
                        this.Discount1x2 = (discountInfo.Discount * 100).ToString(SettingConstants.DecimalFormat);
                        this.UplineMinDiscount1x2 = (Converter.ToDecimal(discountInfo.UplineMinDiscountA) * 100).ToString(SettingConstants.DecimalFormat);
                    }
                }

                if (discountList.Length > 2)
                {
                    discountInfo = discountList[2];

                    if (discountInfo != null)
                    {
                        this.SportTypeOthers = discountInfo.SportType;
                        this.BetTypeOthers = discountInfo.BetType;
                        this.DiscountOthers = (discountInfo.Discount * 100).ToString(SettingConstants.DecimalFormat);
                        this.UplineMinDiscountOthers = (Converter.ToDecimal(discountInfo.UplineMinDiscountA) * 100).ToString(SettingConstants.DecimalFormat);
                    }
                }
            }
        }

        public short SportType { get; set; }

        public short BetType { get; set; }

        public short SportType1x2 { get; set; }

        public short BetType1x2 { get; set; }

        public short SportTypeOthers { get; set; }

        public short BetTypeOthers { get; set; }

        public string DiscountGroup { get; set; }

        public string Discount1x2 { get; set; }

        public string DiscountOthers { get; set; }

        public string UplineMinDiscountGroup { get; set; }

        public string UplineMinDiscountA { get; set; }

        public string UplineMinDiscountB { get; set; }

        public string UplineMinDiscountC { get; set; }

        public string UplineMinDiscountD { get; set; }

        public string UplineMinDiscount1x2 { get; set; }

        public string UplineMinDiscountOthers { get; set; }

        public string Group { get; set; }

        /// <summary>
        /// Gets the up-line group commission.
        /// </summary>
        /// <param name="discountInfo">The discount information.</param>
        /// <returns>Group commission.</returns>
        private decimal GetUplineGroupComm(CustomerDiscountInfo discountInfo)
        {
            decimal discount = 0;

            switch (this.Group)
            {
                case "a":
                    discount = discountInfo.UplineMinDiscountD;
                    break;

                case "b":
                    discount = discountInfo.UplineMinDiscountC;
                    break;

                case "c":
                    discount = discountInfo.UplineMinDiscountB;
                    break;

                case "d":
                    discount = discountInfo.UplineMinDiscountA;
                    break;
            }

            return discount;
        }

        /// <summary>
        /// Gets the actual group of the customer.
        /// </summary>
        /// <param name="group">The group got from database.</param>
        /// <returns>The actual group.</returns>
        private string GetActualGroup(string group)
        {
            string actualGroup = string.Empty;

            switch (group)
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