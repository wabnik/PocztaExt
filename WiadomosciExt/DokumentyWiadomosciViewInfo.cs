using Soneta.Business;
using Soneta.Business.Licence;
using Soneta.Business.UI;
using Soneta.Examples.PocztaExt.WiadomosciExt;
using Soneta.Handel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: FolderView("Handel/Dokumenty wysłane",
    Priority = 1000,
    Description = "Dokumenty wysłane",
    BrickColor = FolderViewAttribute.BlueBrick,
    TableName = "DokHandlowe",
    ViewType = typeof(DokumentyWiadomosciViewInfo),
    Contexts = new object[] { LicencjeModułu.All }
)]

namespace Soneta.Examples.PocztaExt.WiadomosciExt
{
    public class DokumentyWiadomosciViewInfo : ViewInfo
    {
        public DokumentyWiadomosciViewInfo()
        {
            //View wiążemy z odpowiednią definicją viewform.xml 
            ResourceName = "DokumentyWiadomosci";

            //Inicjowanie contextu
            InitContext += DokumentyWiadomosciViewInfo_InitContext;

            //Tworzenie view zawerającego 
            CreateView += DokumentyWiadomosciViewInfo_CreateView;
        }

        private void DokumentyWiadomosciViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            args.Context.TryAdd(() => new DokumentyWiadomosciParams(args.Context));
        }

        private void DokumentyWiadomosciViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            DokumentyWiadomosciParams @params;
            if (!args.Context.Get(out @params))
                return;

            args.View = PrepareView(@params);
        }

        private View PrepareView(DokumentyWiadomosciParams @params)
        {
            RowCondition cond = new RowCondition.Exists("WiadomosciExt", "Dokument");

            return HandelModule.GetInstance(@params).DokHandlowe.WgDefinicja[@params.Definicja][cond].CreateView();
        }
    }
}
