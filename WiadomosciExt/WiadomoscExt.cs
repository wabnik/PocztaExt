using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soneta.Business;
using Soneta.Handel;
using Soneta.CRM;
using Soneta.Examples.PocztaExt.WiadomosciExt;


[assembly: NewRow(typeof(WiadomoscExt))]

//
// Wszystkie obiekty biznesowe zostały umieszczone w jednym namespace, którego nazwa została również 
// umieszczona w pliku *.business.xml (module/@namespace).
//
namespace Soneta.Examples.PocztaExt.WiadomosciExt
{

    //
    // Nazwa klasy obiektu biznesowego brana jest z nazwy elementu table znajdującego się w 
    // *.business.xml (atrybut table/@name).
    // Klasa ta musi dziedziczyć z klasy bazowej znajdującej się w wygenerowanym przez Soneta.Generator module
    // PunktacjaModule i klasy PunktRow (dodano sufiks Row).
    // W klasie tej znajduje się między innymi implementacja properties biznesowych pochodzących z bazy danych.
    //
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