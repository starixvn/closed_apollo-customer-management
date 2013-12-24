// --------------------------------------------------------------------------
// <copyright file="StatusHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.05">Created</change>
// </history>

namespace Apollo.CMS.Core.Helpers
{
    using Apollo.CMS.Core.Constants;
    using Apollo.CMS.Core.Resources;
    using Fanex.Common;

    /// <summary>
    /// Status helper.
    /// </summary>
    public static class StatusHelper
    {
        /// <summary>
        /// Gets customer's status based his current status and his up line status as well.
        /// </summary>
        /// <param name="status">The status ID of the customer.</param>
        /// <param name="uplineStatus">The status ID of up line.</param>
        /// <returns>Associate status of the customer.</returns>
        public static string GetStatus(int? status, int? uplineStatus)
        {
            AccountStatus statusEnumVal = (AccountStatus)Converter.ToInt(status);
            AccountStatus uplineStatusEnumVal = (AccountStatus)Converter.ToInt(uplineStatus);
            string statusValue = string.Empty;

            if ((statusEnumVal == AccountStatus.Active) && (uplineStatusEnumVal == AccountStatus.Active))
            {
                statusValue = ViewLang.Active;
            }
            else if ((statusEnumVal == AccountStatus.Stopped) && (uplineStatusEnumVal == AccountStatus.Active))
            {
                statusValue = ViewLang.Stopped;
            }
            else if ((statusEnumVal == AccountStatus.Locked) && (uplineStatusEnumVal == AccountStatus.Active))
            {
                statusValue = ViewLang.Locked;
            }
            else if ((statusEnumVal == AccountStatus.Active) && (uplineStatusEnumVal == AccountStatus.Stopped))
            {
                statusValue = ViewLang.CLock;
            }
            else if ((statusEnumVal == AccountStatus.Stopped) && (uplineStatusEnumVal == AccountStatus.Stopped))
            {
                statusValue = ViewLang.CLock;
            }
            else if ((statusEnumVal == AccountStatus.Locked) && (uplineStatusEnumVal == AccountStatus.Stopped))
            {
                statusValue = ViewLang.CLock;
            }

            return statusValue;
        }

        /// <summary>
        /// Gets the shadow status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The associate status of the shadow.</returns>
        public static string GetShadowStatus(int? status)
        {
            AccountStatus statusEnumVal = (AccountStatus)Converter.ToInt(status);
            string statusValue = string.Empty;

            switch (statusEnumVal)
            {
                case AccountStatus.Active:
                    statusValue = ViewLang.Active;
                    break;
                case AccountStatus.Stopped:
                    statusValue = ViewLang.Stopped;
                    break;
                case AccountStatus.Locked:
                    statusValue = ViewLang.Locked;
                    break;
                case AccountStatus.CLock:
                    statusValue = ViewLang.CLock;
                    break;
            }

            return statusValue;
        }
    }
}