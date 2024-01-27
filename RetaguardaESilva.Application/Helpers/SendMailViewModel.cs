using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaguardaESilva.Application.Helpers
{
    public class SendMailViewModel
    {
        public string Emails { get; set; }

        public string Assunto { get; set; }

        public string Corpo { get; set; }

        public bool IsHtml { get; set; }
    }
}