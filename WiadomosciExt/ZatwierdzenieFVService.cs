using System;
using System.Diagnostics;
using Soneta.Business;
using Soneta.Handel;
using Soneta.Examples.PocztaExt.WiadomosciExt;

//
// Przykład serwisu umożliwiający na oprogramowywanie działania zmian dokonywanych
// na dokumentach handlowych. Pozwala na ingerencję programisty na działania
// podejmowane w trakcie zatwierdzania dokumentu, wyliczania ceny, zmiany wartości pozycji, itp
//
// Przykład prezentuje śledzenie zapytań, które będą wysyłane na okna loga programu enova.
//

//
// Każdy serwis programu enova należy najpierw zarejestrować, żeby enova mogłą go
// odszukać. Rejestracji dokonujemy przy pomocy ServiceAttribute.
//
[assembly: Service(typeof(IZmianaDokumentuHandlowego), 	typeof(ZatwierdzenieFVService))]

namespace Soneta.Examples.PocztaExt.WiadomosciExt
{

	public class ZatwierdzenieFVService : IZmianaDokumentuHandlowego {

	
		public void Zatwierdzanie(ZmianaDokumentuHandlowegoArgs args) {
			Trace.WriteLine("Zatwierdzanie "+args.Dokument+", edycja: "+args.Edycja, "Zmiana dokumentu");
		}


		public void Zatwierdzony(ZmianaDokumentuHandlowegoArgs args) {
			PrzeliczanieDniHelper.Przelicz(args.Dokument);
		}

		public void ZmianaStanu(ZmianaStanuDokumentuHandlowegoArgs args) {
		
		}

		
		public void ZmianaWartości(ZmianaDokumentuHandlowegoArgs args) {
		
		}

		public void WyliczenieCenyPozycji(WyliczenieCenyPozycjiDokumentuArgs args) {
	
		}

        public void ZmianaPozycji(ZmianaPozycjiDokumentuArgs args)
        {
           
        }

        public void ZmianaPłatności(ZmianaDokumentuHandlowegoArgs args)
        {
          
        }

    }
}
