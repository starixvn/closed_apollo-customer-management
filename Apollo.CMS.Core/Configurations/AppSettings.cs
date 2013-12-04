// --------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.04">Created</change>
// </history>

namespace Apollo.CMS.Core.Configurations
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Application settings.
    /// </summary>
    public static class AppSettings
    {
        public static int NewPrivateMessageMaxRowsLimit
        {
            get
            {
                return Convert.ToInt32(GetValue("NewPrivateMessageMaxRowsLimit"));
            }
        }

        public static int MessageListMonthRange
        {
            get
            {
                return Convert.ToInt32(GetValue("MessageListMonthRange"));
            }
        }

        /// <summary>
        /// Gets the value from web configuration.
        /// </summary>
        /// <param name="key">The key which is used to identify the value.</param>
        /// <returns>Associated value of passed key.</returns>
        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}