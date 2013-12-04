// --------------------------------------------------------------------------
// <copyright file="Upline.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.13">Created</change>
// </history>

namespace Apollo.CMS.Message.Models.NewMessage
{
    /// <summary>
    /// Up line model.
    /// </summary>
    public class Upline
    {
        public int TargetId { get; set; }

        public string TargetName { get; set; }
    }
}