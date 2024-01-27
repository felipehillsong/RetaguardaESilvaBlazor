using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.ContratosServices
{
    public interface IMailService
    {
        void SendMail(string emails, string subject, string body, bool anexo, int? notaFiscalId, bool? exclusao, bool isHtml = false);
    }
}
