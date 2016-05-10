using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soneta.Business;
using Soneta.Handel;
using Soneta.CRM;
using Soneta.Examples.PocztaExt.WiadomosciExt;
using Soneta.Business.App;

[assembly: NewRow(typeof(WydrukWiadomosci))]

namespace Soneta.Examples.PocztaExt.WiadomosciExt
{
    public class WydrukWiadomosci : PocztaExtModule.WydrukWiadomosciRow
    {
        public WydrukWiadomosci(RowCreator creator) : base(creator)
        {
        }
        public WydrukWiadomosci(DefDokHandlowego def,Operator op) : base(def,op)
        {
        }
        protected override void OnAdded()
        {
            base.OnAdded();

        }
        public override string ToString()
        {
            return Wzorzec;
        }
    }
}


    

   

 