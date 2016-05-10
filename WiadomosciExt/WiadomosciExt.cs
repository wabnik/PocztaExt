using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soneta.Business;
using Soneta.Handel;
using Soneta.CRM;
using Soneta.Examples.PocztaExt.WiadomosciExt;

namespace Soneta.Examples.PocztaExt.WiadomosciExt
{
    public class WiadomosciExt: PocztaExtModule.WiadomoscExtTable
    {
        static WiadomosciExt()
        {

          //  HandelModule.DokumentHandlowySchema.AddStanAfterEdit(PrzeliczanieDniHelper.Przelicz);
        }
    }
}
