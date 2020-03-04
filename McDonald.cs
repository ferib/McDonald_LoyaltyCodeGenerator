using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Extensions;
using Newtonsoft.Json;
using RestSharp.Serialization.Xml;
using System.Net;
using System.Reflection;

namespace ReverseCoffee
{
    class McDonald
    {
        public RestClient HttpClient { get; set; }
        public RestClient HttpClient2 { get; set; } //second one will be used so we can check points while spamming keys
        public static McDonald Instance { get; private set; }
        public DiscordWebhookHandler discord { get; set; }

        private DateTime LastGeneratedCode { get; set; }
        private int LastCode { get; set; }
        private string discordUrl = ""; //TODO: set discord webhook url :p

        public McDonald()
        {
            this.HttpClient = new RestClient("https://dif.gmal.app/");
            this.HttpClient.UserAgent = "GMAL/4029 CFNetwork/978.0.7 Darwin/18.7.0"; //Not required but lets stay lowkey
            this.LastCode = -1;
            this.LastGeneratedCode = DateTime.MinValue;
            if(this.discordUrl != "")
                this.discord = new DiscordWebhookHandler(this.discordUrl);
            Instance = this;
        }

        public int GetPromoCode()
        {
            if (this.LastGeneratedCode.AddMinutes(1) < DateTime.UtcNow)
            {
                this.LastGeneratedCode = DateTime.UtcNow;
                this.LastCode = GeneratePromoCode();
            }
            return this.LastCode;
        }


        public int GeneratePromoCode()
        {
            var request = new RestRequest($"plexure/v1/con/v3/consumers/me/verificationtoken?numericToken=true", Method.GET);
            ConfigHeader(ref request);

            //NOTE: change the x-dif-authorization data & Date date, this data can be obtained from sniffing your phone
            request.AddHeader("x-dif-authorization", "Signature keyId=\"AeBu5A9CK03LUaREMNEP95l1JwAKE1Yd7AjToKQjwIcw\",headers=\"(request-target) host date authorization\",signature=\"MEUCIQC4EzJs6qEk1G19q1nVV1aZjqhd8alF0oWJezhQIdFOZQIgCfV2NDtG/obJQn9v4ocfDz8azyvrNSTtltPkz0DyFFU=\"");
            this.HttpClient.ConfigureWebRequest(r => AddCustomDateHeader(r, "2019-11-17T22:30:41Z"));
            IRestResponse response = HttpClient.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return -1;
            McDoCode mcdo = JsonConvert.DeserializeObject<McDoCode>(response.Content);
            return mcdo.verificationToken; //Tokens are valid for 15 min
        }

        public int GetTotalPoints()
        {
            var page = GetOfferPage();
            if(page != null && page.instances.Count > 0)
                return page.instances.FirstOrDefault().pointsBalance;
            return -1;
        }

        public OfferPage GetOfferPage()
        {
            var request = new RestRequest($"plexure/v1/off/v3/loyaltycards?merchantId=655", Method.GET);
            ConfigHeader(ref request);

            //NOTE: change the x-dif-authorization data & Date date, this data can be obtained from sniffing your phone
            request.AddHeader("x-dif-authorization", "Signature keyId=\"AeBu5A9CK03LUaREMNEP95l1JwAKE1Yd7AjToKQjwIcw\",headers=\"(request-target) host date authorization\",signature=\"MEYCIQDFmPoi6PGp6dNu5ER57jSCLIDS4Hd46DEVe/oy4VnjrQIhAJ3KfjB9qMdS2aOZf2+v9CHv9+cHW4p0NsuYhtaZ+yvr\"");
            this.HttpClient2.ConfigureWebRequest(r => AddCustomDateHeader(r, "2020 -02-28T22:19:56Z"));
            IRestResponse response = HttpClient2.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            OfferPage[] pageReturn = JsonConvert.DeserializeObject<OfferPage[]>(response.Content);
            return pageReturn.FirstOrDefault();
        }

        private void AddCustomDateHeader(HttpWebRequest request, string date)
        {
            var addWithoutValidate = request.Headers
                .GetType()
                .GetMethod("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);
            addWithoutValidate.Invoke(request.Headers, new[] { "Date", date});
        }

        private void ConfigHeader(ref RestRequest request)
        {
            //moste off the headers are required!
            request.AddHeader("authority", "dif.gmal.app");
            request.AddHeader("x-vmob-device_os_version", "12.4");
            request.AddHeader("x-vmob-location_accuracy", "65.0");
            request.AddHeader("x-vmob-device_network_type", "wifi");
            request.AddHeader("user-agent", "GMAL/4029 CFNetwork/978.0.7 Darwin/18.7.0");
            request.AddHeader("x-vmob-application_version", "4029");
            request.AddHeader("x-vmob-device_timezone_id", "Europe/Brussels");
            request.AddHeader("x-vmob-device_utc_offset", "+01:00");
            request.AddHeader("x-vmob-device_screen_resolution", "1125x2436");

            request.AddHeader("authorization", "Bearer 9a8b1b8c326192dced342726dbcc07d5d7ea5802901a249109408d17e83b0adc"); //NOTE: change with yours!

            request.AddHeader("accept-language", "en-GB");
            request.AddHeader("x-vmob-device_type", "i_p");
            request.AddHeader("x-dif-platform", "ios");
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("x-vmob-cost-center", "merchantId655");
            request.AddHeader("x-vmob-device", "iPhone");
        }
    }


    //Model
    class McDoCode
    {
        public string expiryDate { get; set; }
        public int verificationToken { get; set; }
    }
}