using System.ComponentModel;
using System.Linq;
using Soneta.Business;
using Soneta.Examples.PocztaExt.WiadomosciExt;
using Soneta.Handel;
using Soneta.CRM;

[assembly: Worker(
    typeof(DokumentyWiadomosciWorker),
    typeof(WiadomoscEmail))]

namespace Soneta.Examples.PocztaExt.WiadomosciExt
{
    public class DokumentyWiadomosciWorker
    {
        [Context]
        public WiadomoscEmail Wiadomosc { get; set; }

        [Description("Lista dokumentów powiązanych z wiadomością Email.")]
        public SubTable Dokumenty
        {
            get
            {
                var WiadomosciModule = PocztaExtModule.GetInstance(Wiadomosc);
                return WiadomosciModule.WiadomosciExt.WgWiadomosc[Wiadomosc];
            }
        }
    }
}
