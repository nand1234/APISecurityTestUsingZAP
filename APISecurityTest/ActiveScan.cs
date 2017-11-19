using System;
using OWASPZAPDotNetAPI;
using System.Threading;
using RestSharp;

namespace APISecurityTest
{
    /// <summary>
    /// Handle Sprider Scan 
    /// </summary>
    class ActiveScan
    {
        private static ClientApi _zapClient = null;
        private IApiResponse _response;

        public ActiveScan(ClientApi zapClient)
        {
            _zapClient = zapClient;
        }
        /// <summary>
        /// Start Spider Scan on the Target URL
        /// </summary>
        /// <param name="zapApiKey"> ZAP API Key</param>
        /// <param name="targetUrl" >Url to Test for Security </param>
        /// <returns> Active Scan ID</returns>
        public string StartActiveScan(string zapApiKey, string targetUrl)
        {
            _response = _zapClient.ascan.scan(zapApiKey, targetUrl, "", "", "", "", "", "");
            return ((ApiResponseElement)_response).Value;
        }

        /// <summary>
        /// Wait till Spider check complete scaning 100%
        /// </summary>
        /// <param name="spideringId"></param>
        public void CheckActiveScanProgress(string activeScanId)
        {
            int progress;
            while (true)
            {
                Thread.Sleep(10000);
                progress = int.Parse(((ApiResponseElement)_zapClient.ascan.status(activeScanId)).Value);

                if (progress >= 100)
                {
                    break;
                }
            }

            Thread.Sleep(5000);
        }
    }
}

