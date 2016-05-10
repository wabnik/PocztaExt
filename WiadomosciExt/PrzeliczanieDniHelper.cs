using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soneta.Handel;
using Soneta.Handel.RelacjeDokumentow.Api;
using System.Timers;
using Soneta.Business;

namespace Soneta.Examples.PocztaExt.WiadomosciExt
{
    class PrzeliczanieDniHelper
    {
        public static void Przelicz(DokumentHandlowy row)
        {
            Soneta.Business.Log l = new Soneta.Business.Log("Zliczanie Dni", true);
            // Jeżeli dokument nie jest w sesji nie kontynuujemy przeliczania
            if (!row.IsLive || row.Definicja.Symbol!="FV") return;
            DokumentHandlowy fv = (DokumentHandlowy)row;
            DokumentHandlowy zo = fv.Nadrzędne[TypRelacjiHandlowej.Kopiowania];

            if (zo != null)
            {
                foreach(WiadomoscExt w in PocztaExtModule.GetInstance(row).WiadomosciExt.WgDokument[zo])
                {
                    w.CzasRealizacji = (new Soneta.Types.FromTo(w.Wiadomosc.Data, DateTime.Today).Days);
                }

            }
       

        }
    }
}

