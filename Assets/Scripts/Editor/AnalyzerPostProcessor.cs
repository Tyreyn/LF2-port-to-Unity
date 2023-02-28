using System.Linq;
using System.Xml.Linq;
using UnityEditor;

namespace Editor
{

    public class CS8032WarningFixer : AssetPostprocessor
    {
        private static string OnGeneratedCSProject(string path, string content)
        {
            var document = XDocument.Parse(content);
            document.Root.Descendants()
                .Where(x => x.Name.LocalName == "Analyzer")
                .Where(x => x.Attribute("Include").Value.Contains("Unity.SourceGenerators"))
                .Remove();
            return document.Declaration + System.Environment.NewLine + document.Root;
        }
    }

    public class AnalyzerPostProcessor : AssetPostprocessor
    {
        public static string OnGeneratedCSProject(string path, string content)
        {
            return content.Replace(
                @"C:\Program Files (x86)\Microsoft Visual Studio Tools for Unity\16.0\Analyzers\Microsoft.Unity.Analyzers.dll",
                @"C:\Users\athos\LF2-port-to-Unity\Assets\ErrorProne.Net.CoreAnalyzers.CodeFixes.dll");
        }
    }
}