using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronization.ESAS.UtilityComponents
{
    public interface IEmailService
    {
        void SendStatusMail(string message);
        void SendStatusMail(string subject, string message);
    }
}
