using System.ComponentModel;
using System.Linq;
using Soneta.Business;
using Soneta.Examples.PocztaExt.WiadomosciExt;
using Soneta.Handel;
using Soneta.CRM;

[assembly: Worker(
    typeof(WiadomosciDokumentuWorker),
    typeof(DokumentHandlowy))]

namespace Soneta.Examples.PocztaExt.WiadomosciExt
{
    public class WiadomosciDokumentuWorker
    {
        [Context]
        public DokumentHandlowy Dokument { get; set; }

        [Description("Lista wiadomosci związanych z danym dokumentem handlowym.")]
        public SubTable Wiadomosci
        {
            get
            {      
                var WiadomosciModule = PocztaExtModule.GetInstance(Dokument);
                return WiadomosciModule.WiadomosciExt.WgDokument[Dokument];
            }
        }
    }
}
