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
        public static string GetStatus(int status, int uplineStatus)
        {
            string statusValue = string.Empty;

            if ((status == (int)CustomerStatus.Active) && (uplineStatus == (int)CustomerStatus.Active))
            {
                statusValue = ViewLang.Active;
            }
            else if ((status == (int)CustomerStatus.Stopped) && (uplineStatus == (int)CustomerStatus.Active))
            {
                statusValue = ViewLang.Stopped;
            }
            else if ((status == (int)CustomerStatus.Locked) && (uplineStatus == (int)CustomerStatus.Active))
            {
                statusValue = ViewLang.Locked;
            }
            else if ((status == (int)CustomerStatus.Active) && (uplineStatus == (int)CustomerStatus.Stopped))
            {
                statusValue = ViewLang.CLock;
            }
            else if ((status == (int)CustomerStatus.Stopped) && (uplineStatus == (int)CustomerStatus.Stopped))
            {
                statusValue = ViewLang.CLock;
            }
            else if ((status == (int)CustomerStatus.Locked) && (uplineStatus == (int)CustomerStatus.Stopped))
            {
                statusValue = ViewLang.CLock;
            }

            return statusValue;
        }
    }
}