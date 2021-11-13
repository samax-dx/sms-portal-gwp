const fs = require("fs");

const args = process.argv.slice(2);
const apiKey = args[0];
const clientId = args[1];

fs.writeFileSync(
    "ConfigSGW.cs",
    (
`
namespace sms_portal_gwp
{
    public class ConfigSGW
    {
        public static readonly string baseUrl = "http://sms.brilliant.com.bd:6005/api/v2";
        public static readonly string apiKey = "${apiKey}";
        public static readonly string clientId = "${clientId}";
    }
}
`
    )
);
