using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soneta.Business;
using Soneta.Handel;
using Soneta.CRM;
using Soneta.Examples.PocztaExt.WiadomosciExt;


[assembly: NewRow(typeof(WiadomoscExt))]


namespace Soneta.Examples.PocztaExt.WiadomosciExt
{
 
    public class WiadomoscExt : PocztaExtModule.WiadomoscExtRow
    {

     
        public WiadomoscExt(RowCreator creator) : base(creator)        
        {          
        }
        public WiadomoscExt(WiadomoscEmail wiadomosc, DokumentHandlowy dokument) : base(wiadomosc,dokument)
        {
        }

        public override string ToString()
        {
            return  (Wiadomosc == null) ? ""  : Wiadomosc.ToString(); 
        }
  
        protected override void OnAdded()
        {         
            base.OnAdded();     
        }    
        
    }
}