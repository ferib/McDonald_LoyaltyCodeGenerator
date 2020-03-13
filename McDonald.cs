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
        public static McDonald Instance { get; private set; }
        public DiscordWebhookHandler discord { get; set; }

        private DateTime LastGeneratedCode { get; set; }
        private DateTime LastPointsCheck { get; set; }
        private int LastCode { get; set; }
        private int LastPoints { get; set; }
        private string discordUrl = ""; //TODO: set discord webhook url :p

        public McDonald()
        {
            this.HttpClient = new RestClient("https://dif.gmal.app/");
            this.HttpClient.UserAgent = "GMALi/4029 CFNetwork/978.0.7 Darwin/18.7.0"; //Not required but lets stay lowkey //EDIT (12/03/2020): Now checks if string starts with "GMALi" 
            this.LastCode = -1;
            this.LastGeneratedCode = DateTime.MinValue;
            if(this.discordUrl != "")
                this.discord = new DiscordWebhookHandler(this.discordUrl);
            Instance = this;
        }

        public int GetPromoCode()
        {
            //since 11/03/2020, there is a bigger timeout on requesting code's
            
            if (this.LastGeneratedCode.AddMinutes(7) < DateTime.UtcNow) //7min timeout due to new anti spam
            {
                this.LastGeneratedCode = DateTime.UtcNow;
                this.LastCode = GeneratePromoCode();
                Console.WriteLine($"[{DateTime.UtcNow.ToString()}]: {this.LastCode}");
            }
            
            return this.LastCode;
        }


        public int GeneratePromoCode()
        {
            var request = new RestRequest($"plexure/v1/con/v3/consumers/me/verificationtoken?numericToken=true", Method.GET);
            ConfigHeader(ref request);

            //NOTE: change the x-dif-authorization data & Date date, this data can be obtained from sniffing your phone
            request.AddHeader("x-dif-authorization", "Signature keyId=\"AeBu5A9CK03LUaREMNEP95l1JwAKE1Yd7AjToKQjwIcw\",headers=\"(request-target) host date authorization\",signature=\"MEYCIQCC8Mp/KuB+DA6xpsqhBGigYpk4FgoUq7USGVmI5QPzIgIhANqgt1Yli4dUg2/7/tuVmckIOnnBmBbN3exFMl32HdaW\"");
            request.AddHeader("x-acf-sensor-data", "1,i,Tbpp3RkhAXgTCExo4OtcjioGKJWLY5HkhWZrqxZ8Gjt92Kk6FETzlOUuvPQGfru7QuCRgH9AVlyScqxvJryzxxysbkdRSDIBFZl7uOaQz5RcSDQQmJCM77oGqTVEekgQmmkgFxcu0dZWnvl0aVtjFaMXnacUd3QuUEe/oFjRUH8=,T+Gh2BtvTzQyyE7gUK44uFQJCxHSRcu0R/9kGYY+3qc/gkPDapv9GeBBbHg4ReYPx2+m/NaT7ouJGDm5Ijpyyd+aNQtYZUD/J2v3sddoFuzKRLB1QKlZqZNL4BFT8fRRH9ja0Eyp6Wv8CAyOF0JgubV5uia014W0imlLJuAsgQU=$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$22,10,75");
            this.HttpClient.ConfigureWebRequest(r => AddCustomDateHeader(r, "2020-03-12T17:47:53Z"));
            IRestResponse response = HttpClient.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return -1;
            McDoCode mcdo = JsonConvert.DeserializeObject<McDoCode>(response.Content);
            return mcdo.verificationToken; //Tokens are valid for 15 min
        }

        public int GetTotalPoints()
        {
            if (this.LastPointsCheck.AddMinutes(7) < DateTime.UtcNow) //7min timeout due to new anti spam
            {
                this.LastPointsCheck = DateTime.UtcNow;
                OfferPage page = null; //GetOfferPage(); //NOTE: To lazy to fix thise one atm..
                if (page != null && page.instances.Count > 0)
                    this.LastPoints = page.instances.FirstOrDefault().pointsBalance;
                else
                    this.LastPoints = -1;
            }
            return this.LastPoints;
        }

        public OfferPage GetOfferPage()
        {
            var request = new RestRequest($"plexure/v1/off/v3/loyaltycards?merchantId=655", Method.GET);
            ConfigHeader(ref request);

            //NOTE: change the x-dif-authorization data & Date date, this data can be obtained from sniffing your phone
            request.AddHeader("x-dif-authorization", "Signature keyId=\"AeBu5A9CK03LUaREMNEP95l1JwAKE1Yd7AjToKQjwIcw\",headers=\"(request-target) host date authorization\",signature=\"MEYCIQDFmPoi6PGp6dNu5ER57jSCLIDS4Hd46DEVe/oy4VnjrQIhAJ3KfjB9qMdS2aOZf2+v9CHv9+cHW4p0NsuYhtaZ+yvr\"");
            this.HttpClient.ConfigureWebRequest(r => AddCustomDateHeader(r, "2020-02-28T22:19:56Z"));
            IRestResponse response = HttpClient.Execute(request);
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
            //the main thing to do here is copy your authorization headers, the others should do the trick just fine
            request.AddHeader("authority", "dif.gmal.app");
            request.AddHeader("x-vmob-device_os_version", "12.4");
            request.AddHeader("x-vmob-location_accuracy", "65.0");
            request.AddHeader("x-vmob-device_network_type", "wifi");
            request.AddHeader("user-agent", "GMAL/4029 CFNetwork/978.0.7 Darwin/18.7.0");
            request.AddHeader("x-vmob-application_version", "4029");
            request.AddHeader("x-vmob-uid", "1E427934-4707-4BD8-AEAE-708E254183CB"); //newly added since 11/03/2020 !!
            request.AddHeader("x-vmob-device_timezone_id", "Europe/Brussels");
            request.AddHeader("x-vmob-device_utc_offset", "+01:00");
            request.AddHeader("x-vmob-device_screen_resolution", "1125x2436");

            request.AddHeader("authorization", "Bearer e47b22a00a73c20cf0f341535308eaf7ab5053f7a620834a2679c95a9b7872ed"); //NOTE: change with yours !!

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

    class PageData
    {
        public int code { get; set; }
        public int currentPoints { get; set; }
    }
}