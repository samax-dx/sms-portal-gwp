using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Flurl.Http;
using TelcobrightUtil;
using SmsGateway;


namespace sms_portal_gwp
{
    class Program
    {
        private static readonly IDictionary<string, Func<string, Task<string>>> actions = new Dictionary<string, Func<string, Task<string>>> {
            {
                "sendSMS",
                async payloadRaw =>
                {
                    var endpoint = "/SendSMS";
                    IDictionary<string, object> payload = JsonConvert.DeserializeObject<IDictionary<string, object>>(QString.Base64Decode(payloadRaw));

                    payload.Add("ApiKey", ConfigSGW.apiKey);
                    payload.Add("ClientId", ConfigSGW.clientId);

                    IFlurlResponse response = await $"{ConfigSGW.baseUrl}{endpoint}".PostJsonAsync(payload);
                    object resultRaw = await response.GetJsonAsync<object>();

                    return QString.Base64Encode(resultRaw.ToString());
                }
            }
        };

        private static void showHelp()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0018:Inline variable declaration", Justification = "<Pending>")]
        static async Task Main(string[] args)
        {
            EndPoint endPoint = new EndPoint(new HttpConfig()
            {
                BaseUrl = ConfigSGW.baseUrl,
                UrlSuffix = "/SendSMS",
                ApiKey = ConfigSGW.apiKey,
                ClientId = ConfigSGW.clientId
            });

            ISmsProvider smsProvider = new SmsProviderHttp(endPoint);

            var result = await smsProvider.SendSms(new SmsTask()
            {
                CampaignName = "test",
                SenderId = "8809638010035",
                MobileNumbers = "8801717590703,8801754105098",
                Message = "From latest sms-portal-gwp",
            });

            Console.WriteLine(result);

            /*var action = args.ElementAtOrDefault(0);
            var payload = args.ElementAtOrDefault(1);

            if (action == null)
            {
                showHelp();
                Environment.Exit(0);
            }

            Func<string, Task<string>> eval;
            actions.TryGetValue(action, out eval);

            if (eval == null) Environment.Exit(1);

            var result = await eval(payload);
            Console.WriteLine(result);*/
        }
    }
}
