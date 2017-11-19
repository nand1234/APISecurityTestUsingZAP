
using OWASPZAPDotNetAPI;
using System.IO;

namespace APISecurityTest
{
    class GenerateReports
    {
        private string reportLocation = "F:\\Periscope-Doc\\";
        private static ClientApi _zapClient = null;
        private string _zapApiKey;

        /// <summary>
        /// Contrcutor to ZAP API Key
        /// </summary>
        /// <param name="zapApiKey"></param>
        public GenerateReports(string zapApiKey, ClientApi zapClient)
        {
            _zapApiKey = zapApiKey;
            _zapClient = zapClient;

        }
        /// <summary>
        /// Generate Xml Report
        /// </summary>
        /// <param name="filename"></param>
        public void GenerateXmlReport(string filename)
        {
            var fileName = $"F:\\Periscope-Doc\\{filename}.xml";
            File.WriteAllBytes(fileName, _zapClient.core.xmlreport(_zapApiKey));
        }

        /// <summary>
        /// Generate HTML Report
        /// </summary>
        /// <param name="filename"></param>
        public void GenerateHTMLReport(string filename)
        {
            var fileName = $"F:\\Periscope-Doc\\{filename}.html";
            File.WriteAllBytes(fileName, _zapClient.core.htmlreport(_zapApiKey));
        }

        /// <summary>
        /// Generate MarkDown Report
        /// </summary>
        /// <param name="filename"></param>
        public void GenerateMarkdownReport(string filename)
        {
            var fileName = $"F:\\Periscope-Doc\\{filename}.md";
            File.WriteAllBytes(fileName, _zapClient.core.mdreport(_zapApiKey));
        }
    }
}
