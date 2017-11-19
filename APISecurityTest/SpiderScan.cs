using System;
using OWASPZAPDotNetAPI;
using System.Threading;

namespace APISecurityTest
{
    /// <summary>
    /// Handle Sprider Scan 
    /// </summary>
    class SpiderScan
    {
        private static ClientApi _zapClient = null;
        private IApiResponse _response;

        public SpiderScan(ClientApi zapClient)
        {
            _zapClient = zapClient;
        }
        /// <summary>
        /// Start Spider Scan on the Target URL
        /// </summary>
        /// <param name="zapApiKey"> ZAP API Key</param>
        /// <param name="targetUrl" >Url to Test for Security </param>
        /// <returns> Spider ID</returns>
        public string StartSpidering(string zapApiKey, string targetUrl)
        {
            _response = _zapClient.spider.scan(zapApiKey, targetUrl, "", "", "", "");
            return ((ApiResponseElement)_response).Value;
        }

        /// <summary>
        /// Wait till Spider check complete scaning 100%
        /// </summary>
        /// <param name="spideringId"></param>
        public void CheckSpideringProgress(string spideringId)
        {
            int progress;
            while (true)
            {
                Thread.Sleep(10000);
                progress = int.Parse(((ApiResponseElement)_zapClient.spider.status(spideringId)).Value);
                if (progress >= 100)
                {
                    break;
                }
            }

            Thread.Sleep(5000);
        }

    }
}
