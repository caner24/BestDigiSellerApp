using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
