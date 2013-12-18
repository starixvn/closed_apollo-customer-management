// --------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
// </history>

namespace Apollo.CMS.Websites
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Web.Optimization;
    using BundleTransformer.Core.Orderers;
    using BundleTransformer.Core.Transformers;

    /// <summary>
    /// Bundle Configuration.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundle collections.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterCommonBundles(bundles);
            RegisterLoginBundles(bundles);
            RegisterMessageListBundles(bundles);
            RegisterNewMessageBundles(bundles);
            RegisterAccountInfoBundles(bundles);
            RegisterTreeviewBundles(bundles);
            RegisterAccountListBundles(bundles);

            bundles.FileSetOrderList.Clear();
        }

        /// <summary>
        /// Registers the common bundles.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        private static void RegisterCommonBundles(BundleCollection bundles)
        {
            bundles.AddAndInclude(
                "~/site/common",
                "~/content/bootstrap/css/bootstrap.css",
                "~/content/lib/jquery-ui/jquery-ui-1.9.1.custom.css",
                "~/content/lib/jquery-ui/jquery.selectBoxIt.css",
                "~/scripts/lib/msgbox/msgbox-0.0.1.css",
                "~/scripts/lib/tipsy/tipsy-1.0.0.css",
                "~/content/site/custom.less",
                "~/content/site/index.less");

            bundles.Add(new ScriptBundle("~/scripts/headjs").Include(
                "~/scripts/lib/html5-libs/head.js"));

            bundles.Add(new ScriptBundle("~/scripts/repondjs").Include(
                "~/scripts/lib/respond.js"));

            bundles.Add(new ScriptBundle("~/Scripts/ReCheckin").Include(
                    "~/Areas/UserLogin/scripts/ReCheckIn.js"));

            bundles.Add(new ScriptBundle("~/scripts/commonJS").Include(
                "~/scripts/lib/jQuery/jquery-1.8.2.js",
                "~/scripts/lib/jQuery/jquery-ui-1.8.24.js",
                "~/scripts/lib/jQuery/jquery-migrate-1.2.1.js",
                "~/scripts/lib/jquery/jquery.selectBoxIt.js",
                "~/scripts/lib/jquery/jquery.cookie.js",
                "~/content/bootstrap/js/bootstrap.js",
                "~/scripts/lib/msgbox/msgbox-0.0.1.js",
                "~/scripts/lib/tipsy/tipsy-1.0.0.js",
                "~/scripts/lib/namespace.js",
                "~/scripts/lib/Underscore/underscore.js",
                "~/scripts/_layout.js",
                "~/scripts/cms.js",                
                "~/Scripts/Validator.js"));
        }

        /// <summary>
        /// Registers bundles for login page.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        private static void RegisterLoginBundles(BundleCollection bundles)
        {
            bundles.AddAndInclude(
                "~/site/login",
                "~/Areas/UserLogin/themes/login.less");

            bundles.Add(new ScriptBundle("~/scripts/loginJS").Include(
                "~/scripts/lib/jQuery/jquery-1.8.2.js",
                "~/scripts/lib/jQuery/jquery.validate*",
                "~/scripts/lib/jQuery/jquery.cookie.js",
                "~/content/bootstrap/js/bootstrap.js",
                "~/Areas/UserLogin/scripts/login.js"));
            bundles.FileSetOrderList.Clear();
        }

        /// <summary>
        /// Registers bundles for message list function.
        /// </summary>
        /// <param name="bundles">The bundles collection.</param>
        private static void RegisterMessageListBundles(BundleCollection bundles)
        {
            bundles.AddAndInclude(
                "~/site/messageList",
                "~/areas/message/resources/messagelist/message.less");

            bundles.Add(new ScriptBundle("~/scripts/messageList").Include(
                "~/Areas/Message/Resources/MessageList/MessageList.js"));

            bundles.AddAndInclude(
                 "~/styles/fanexGrid",
                 "~/scripts/lib/Fanex.Grid/Fanex.Grid.css",
                 "~/scripts/lib/Fanex.Grid/Fanex.Pagination.css",
                "~/content/site/customFanexGrid.less");

            bundles.Add(new ScriptBundle("~/scripts/fanexGrid").Include(
                "~/scripts/lib/Fanex.Grid/Fanex.Grid.js",
                "~/scripts/lib/Fanex.Grid/Fanex.Pagination.js",
                "~/scripts/lib/Fanex.Grid/moment.js",
                "~/scripts/lib/Fanex.Grid/numeral.js"));
        }

        /// <summary>
        /// Registers bundles for new message page.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        private static void RegisterNewMessageBundles(BundleCollection bundles)
        {
            bundles.AddAndInclude(
                "~/site/newMessage",
                "~/areas/message/resources/newmessage/newMessage.less");

            bundles.Add(new ScriptBundle("~/scripts/newPrivateMessage").Include(
                "~/Areas/Message/Resources/NewMessage/PrivateMessage.js"));

            bundles.Add(new ScriptBundle("~/scripts/newPublicMessage").Include(
                "~/Areas/Message/Resources/NewMessage/PublicMessage.js"));
        }

        /// <summary>
        /// Registers bundles for left panel.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        private static void RegisterAccountInfoBundles(BundleCollection bundles)
        {
            bundles.AddAndInclude(
                "~/site/accountInfo",
                "~/Areas/Account/Resources/AccountInfo/accountInfo.less");

            bundles.Add(new ScriptBundle("~/scripts/searchBar").Include(
                "~/Areas/Account/Resources/AccountInfo/SearchBar.js"));

            bundles.Add(new ScriptBundle("~/scripts/leftPanel").Include(
                "~/Areas/Account/Resources/AccountInfo/AccountInfo.js"));
        }

        /// <summary>
        /// Registers the tree view bundles.
        /// </summary>
        /// <param name="bundles">The bundles collection.</param>
        private static void RegisterTreeviewBundles(BundleCollection bundles)
        {
            bundles.AddAndInclude(
                "~/site/treeView",
                "~/Areas/Account/Resources/TreeView/treeview.css");

            bundles.Add(new ScriptBundle("~/scripts/treeView").Include(
                "~/Areas/Account/Resources/TreeView/jquery.treeview.js",
                "~/Areas/Account/Resources/TreeView/jquery.treeview.async.js",
                "~/Areas/Account/Resources/TreeView/treeview.js"));
        }

        /// <summary>
        /// Registers bundles for account list function.
        /// </summary>
        /// <param name="bundles">The bundles collection.</param>
        private static void RegisterAccountListBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/agentList").Include(
                "~/Areas/Account/Resources/AccountList/AgentList.js"));

            bundles.Add(new ScriptBundle("~/scripts/memberList").Include(
                "~/Areas/Account/Resources/AccountList/MemberList.js"));

            bundles.Add(new ScriptBundle("~/scripts/shadowList").Include(
                "~/Areas/Account/Resources/AccountList/ShadowList.js"));
        }
    }

    /// <summary>
    /// Bundle Collection extension.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Need to review here.")]
    internal static class BundleExtensions
    {
        /// <summary>
        /// Creates a bundle, include the virtual paths to the created bundle, 
        /// and add it to the bundle collection.
        /// </summary>
        /// <param name="bundles">The bundle collection which the virtual paths are included.</param>
        /// <param name="bundleVirtualPath">Virtual path of the created bundle.</param>
        /// <param name="virtualPaths">The virtual paths that needed to add to bundle collection.</param>
        public static void AddAndInclude(
            this BundleCollection bundles,
            string bundleVirtualPath,
            params string[] virtualPaths)
        {
            bundles.Add(new Bundle(bundleVirtualPath)
            {
                Transforms = { new CssTransformer(), new CssMinify() },
                Orderer = new NullOrderer()
            }.Include(virtualPaths));
        }

        /// <summary>
        /// Gets relative path from absolute path.
        /// </summary>
        /// <param name="context">The context using to get the path.</param>
        /// <param name="path">The absolute path.</param>
        /// <returns>The relative path.</returns>
        public static string RelativeFromAbsolutePath(this HttpContextBase context, string path)
        {
            var request = context.Request;
            var applicationPath = request.PhysicalApplicationPath;
            var virtualDir = request.ApplicationPath;
            virtualDir = virtualDir == "/" ? virtualDir : (virtualDir + "/");
            return path.Replace(applicationPath, virtualDir).Replace(@"\", "/");
        }
    }
}