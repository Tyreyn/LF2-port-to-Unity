// <copyright file="AnalyzerPostProcessor.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Editor
{
    using System.Linq;
    using System.Xml.Linq;
    using UnityEditor;

    /// <summary>
    /// Pretend from CS8032 warning.
    /// </summary>
#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name should match first type name
    public class CS8032WarningFixer : AssetPostprocessor
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
    {
        /// <summary>
        /// Pretend from CS8032 warning.
        /// </summary>
#pragma warning disable RCS1213 // Remove unused member declaration.
#pragma warning disable S1172 // Unused method parameters should be removed
        private static string OnGeneratedCSProject(string path, string content)
#pragma warning restore S1172 // Unused method parameters should be removed
#pragma warning restore RCS1213 // Remove unused member declaration.
        {
            var document = XDocument.Parse(content);
            document.Root.Descendants()
                .Where(x => x.Name.LocalName == "Analyzer")
                .Where(x => x.Attribute("Include").Value.Contains("Unity.SourceGenerators"))
                .Remove();
            return document.Declaration + System.Environment.NewLine + document.Root;
        }
    }

    /// <summary>
    /// Code analyzer.
    /// </summary>
    public class AnalyzerPostProcessor : AssetPostprocessor
    {
        /// <summary>
        /// Code analyzer.
        /// </summary>
#pragma warning disable SA1615 // Element return value should be documented
#pragma warning disable RCS1163 // Unused parameter.
#pragma warning disable SA1611 // Element parameters should be documented
        public static string OnGeneratedCSProject(string path, string content)
#pragma warning restore SA1611 // Element parameters should be documented
#pragma warning restore RCS1163 // Unused parameter.
#pragma warning restore SA1615 // Element return value should be documented
        {
            return content.Replace(
                @"C:\Program Files (x86)\Microsoft Visual Studio Tools for Unity\16.0\Analyzers\Microsoft.Unity.Analyzers.dll",
                @"C:\Users\athos\LF2-port-to-Unity\Assets\ErrorProne.Net.CoreAnalyzers.CodeFixes.dll");
        }
    }
}