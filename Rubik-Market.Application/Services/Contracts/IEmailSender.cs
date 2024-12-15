using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Application.Services.Contracts
{
    public interface IEmailSender
    {
       bool SendEmail(string to, string subject, string body);
    }
}
