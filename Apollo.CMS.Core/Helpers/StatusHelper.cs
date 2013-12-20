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
        public static string GetStatus(AccountStatus status, AccountStatus uplineStatus)
        {
            string statusValue = string.Empty;

            if ((status == AccountStatus.Active) && (uplineStatus == AccountStatus.Active))
            {
                statusValue = ViewLang.Active;
            }
            else if ((status == AccountStatus.Stopped) && (uplineStatus == AccountStatus.Active))
            {
                statusValue = ViewLang.Stopped;
            }
            else if ((status == AccountStatus.Locked) && (uplineStatus == AccountStatus.Active))
            {
                statusValue = ViewLang.Locked;
            }
            else if ((status == AccountStatus.Active) && (uplineStatus == AccountStatus.Stopped))
            {
                statusValue = ViewLang.CLock;
            }
            else if ((status == AccountStatus.Stopped) && (uplineStatus == AccountStatus.Stopped))
            {
                statusValue = ViewLang.CLock;
            }
            else if ((status == AccountStatus.Locked) && (uplineStatus == AccountStatus.Stopped))
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
        public static string GetShadowStatus(AccountStatus status)
        {
            string statusValue = string.Empty;

            switch (status)
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