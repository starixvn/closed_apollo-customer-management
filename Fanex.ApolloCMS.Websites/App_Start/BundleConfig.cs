// --------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
// </history>

namespace Fanex.ApolloCMS.Websites
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
        }

        /// <summary>
        /// Registers the common bundles.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        private static void RegisterCommonBundles(BundleCollection bundles)
        {
            bundles.AddAndInclude(
                "~/site/layout",
                "~/content/bootstrap/css/bootstrap.css",
                "~/content/bootstrap/css/bootstrap-ie7.css",
                "~/content/themes/jquery-ui/jquery-ui-1.9.1.custom.css",
                "~/content/themes/jquery-ui/jquery.selectBoxIt.css",
                "~/content/site/layout.less");

            bundles.Add(new ScriptBundle("~/scripts/headjs").Include(
                "~/scripts/lib/html5-libs/head.js"));

            bundles.Add(new ScriptBundle("~/scripts/commonJS").Include(
                "~/scripts/lib/jQuery/jquery-1.8.2.js",
                "~/scripts/lib/jQuery/jquery.easing.1.3.js",
                "~/scripts/lib/jQuery/jquery-ui-1.8.24.js",
                "~/scripts/lib/jQuery/jquery.unobtrusive-ajax.js",
                "~/scripts/lib/jQuery/jquery.validate*",
                "~/scripts/lib/jQuery/jquery.selectBoxIt.js",
                "~/scripts/lib/jQuery/jquery.cookie.js",
                "~/content/bootstrap/js/bootstrap.js",
                "~/scripts/lib/respond.js",
                "~/scripts/_layout.js"));
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