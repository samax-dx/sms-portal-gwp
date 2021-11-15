using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmsGateway
{
    public interface ISmsProvider
    {
        Task<string> SendSms(SmsTask smsTask);
    }
}
