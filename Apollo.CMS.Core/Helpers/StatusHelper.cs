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
        /// <param name="statusId">The status ID of the customer.</param>
        /// <param name="uplineStatusId">The status ID of up line.</param>
        /// <returns>Associate status of the customer.</returns>
        public static string GetStatus(int statusId, int uplineStatusId)
        {
            string status = string.Empty;

            if ((statusId == (int)CustomerStatus.Active) && (uplineStatusId == (int)CustomerStatus.Active))
            {
                status = ViewLang.Active;
            }
            else if ((statusId == (int)CustomerStatus.Stopped) && (uplineStatusId == (int)CustomerStatus.Active))
            {
                status = ViewLang.Stopped;
            }
            else if ((statusId == (int)CustomerStatus.Locked) && (uplineStatusId == (int)CustomerStatus.Active))
            {
                status = ViewLang.Locked;
            }
            else if ((statusId == (int)CustomerStatus.Active) && (uplineStatusId == (int)CustomerStatus.Stopped))
            {
                status = ViewLang.CLock;
            }
            else if ((statusId == (int)CustomerStatus.Stopped) && (uplineStatusId == (int)CustomerStatus.Stopped))
            {
                status = ViewLang.CLock;
            }
            else if ((statusId == (int)CustomerStatus.Locked) && (uplineStatusId == (int)CustomerStatus.Stopped))
            {
                status = ViewLang.CLock;
            }

            return status;
        }
    }
}