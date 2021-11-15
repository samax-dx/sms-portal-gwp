using System;
using System.Collections.Generic;

namespace SmsGateway
{
    public class SmsTask
    {
        public string CampaignName { get; set; }
        public string SenderId { get; set; }
        public string MobileNumbers { get; set; }
        public string Message { get; set; }
        public bool Is_Unicode { get; set; } = true;
        public bool Is_Flash { get; set; } = false;
        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>() {
                { "CampaignName", this.CampaignName },
                { "SenderId", this.SenderId },
                { "MobileNumbers", this.MobileNumbers },
                { "Message", this.Message },
                { "Is_Unicode", this.Is_Unicode },
                { "Is_Flash", this.Is_Flash }
            };
        }
    }
}
