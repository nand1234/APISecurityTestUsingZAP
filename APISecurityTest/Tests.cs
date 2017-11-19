using NUnit.Framework;
using APISecurityTest;
using OWASPZAPDotNetAPI;
using System;
using RestSharp;
using System.Net;

namespace APISecurityTest
{
    [TestFixture]
    public class Tests
    {
        private readonly static string _zapApiKey = "t695pjn4v1ko3p7qfvrgq25t7u";
        private readonly static string _zapUrl = "localhost";
        private readonly static int _zapPort = 8080;
        private readonly string _targetUrl = "http://vievuautomationapi.cloudapp.net"; // Web App Hosted on Azure

        private static ClientApi _zapClient;



        [OneTimeSetUp]
        public void StartZap()
        {
            _zapClient = new ClientApi(_zapUrl, _zapPort, _zapApiKey);
        }

        [SetUp]
        public void RunAPI()
        {
            var client = new RestClient("http://vievuautomationapi.cloudapp.net/api/FeedAPITestData");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            //create a WebProxy to run the API same as Zap Proxy
            WebProxy myproxy = new WebProxy("localhost",8080);
            myproxy.BypassProxyOnLocal = false;
            client.Proxy = myproxy;

            //client.AddDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:57.0) Gecko/20100101 Firefox/57.0");
            var request = new RestRequest("", Method.GET);
         
            // easily add HTTP Headers
            request.AddHeader("Content-Type", "application/json");

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as 
        }

        [Test]
        public void StartActiveScan()
        {
            var scan = new ActiveScan(_zapClient);
            var activeScanId = scan.StartActiveScan(_zapApiKey, _targetUrl);
            scan.CheckActiveScanProgress(activeScanId);
        }

        [Test]
        public void StartrScan()
        {
            var scan = new SpiderScan(_zapClient);
            var activeScanId = scan.StartSpidering(_zapApiKey, _targetUrl);
            scan.CheckSpideringProgress(activeScanId);
        }

        [TearDown]
        public void Dispose()
        {


            var report = new GenerateReports(_zapApiKey, _zapClient);
            var reportFilename = $"{DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss")}_OWASP_ZAP_Report";
            report.GenerateXmlReport(reportFilename);
            report.GenerateHTMLReport(reportFilename);
            report.GenerateMarkdownReport(reportFilename);
            _zapClient.Dispose();
        }
    }
}
