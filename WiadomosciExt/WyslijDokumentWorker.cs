using System.ComponentModel;
using System.Linq;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Examples.PocztaExt.WiadomosciExt;
using Soneta.Handel;
using Soneta.Business.Licence;
using Soneta.Core;
using Soneta.CRM;
using Soneta.Commands;
using System.IO;
using Soneta.Tools;
using Soneta.Business.Db;
using Soneta.Zadania;

[assembly: Worker(
    typeof(WyslijDokumentWorker),
    typeof(DokumentHandlowy))]

namespace Soneta.Examples.PocztaExt.WiadomosciExt
{
    class WyslijDokumentWorker
    {
        [Context]
        public DokumentHandlowy Dokument { get; set; }
        [Context]
        public Context cx { get; set; }

        string reportname = "";
        string defaultreport = "";

        public string ReportName
        {
            get
            {
                if(reportname=="") reportname = "Faktura:" + Dokument.Numer + "_" + System.DateTime.Now.ToString() + "_";
                return reportname;
            }
        }
        public string DefaultReport
        {
            get
            {
                if (defaultreport == "") defaultreport = PocztaExtModule.GetInstance(Dokument).WydrukiWiad.WgDefinicjaOperator[Dokument.Definicja, Dokument.Session.Login.Operator].Wzorzec;
                return defaultreport;
            }
        }


        [Action("Wyślij dokument", Target = ActionTarget.ToolbarWithText, Mode = ActionMode.SingleSession,
        CommandShortcut = CommandShortcut.F8, Priority = 10,
        Contexts = new object[] { LicencjeModułu.CRM | LicencjeModułu.HAN })]
     
        public WiadomoscEmail WyslijDokument()
        {
            WiadomoscRobocza wr;
            ReportResult rr = UtworzPDF();
            IReportService rs;
            cx.Session.GetService(out rs);


            using (ITransaction t = cx.Session.Logout(true))
            {
                Stream s = rs.GenerateReport(rr);
                Attachment zal = new Soneta.Business.Db.Attachment(Dokument, AttachmentType.Attachments);
                zal.Name = ReportName;
                BusinessModule.GetInstance(Dokument).Attachments.AddRow(zal);
                zal.LoadFromStream(rs.GenerateReport(rr));
                wr = WyslijWiadomosc(s);
                t.CommitUI();
            }
            return wr;
        }

        public bool IsVisibleWyslijDokument()
        {
            Soneta.Zadania.Config.UstawieniaOperatora uo = ZadaniaModule.GetInstance(cx).Config.Operatorzy[cx.Login.Operator];
            if (uo.DomyslneKontoPocztowe != null)
                return true;

            return false;
        }
      

        ReportResult UtworzPDF()
        {

            return new ReportResult()
            {
                Caption = "Wydruk dokumentu" + Dokument.Numer,
                Context = cx,
                TemplateFileName = DefaultReport,
                Format = ReportResultFormat.PDF,          
            };
        }

        public WiadomoscRobocza WyslijWiadomosc(Stream stream)
        {
            string outFileName = "Faktura" + System.Environment.TickCount.ToString();
            WiadomoscRobocza Wiadomosc = new WiadomoscRobocza();
            CRMModule.GetInstance(Dokument).WiadomosciEmail.AddRow(Wiadomosc);
            Wiadomosc.KontoPocztowe = Soneta.Zadania.ZadaniaModule.GetInstance(Dokument).Config.Operatorzy[Dokument.Session.Login.Operator].DomyslneKontoPocztowe;

            if (Dokument.Kontrahent != null)
            {
                Wiadomosc.Temat += "[" + Dokument.Numer + "]";
                Wiadomosc.Tresc += "Witaj," + System.Environment.NewLine + Dokument.Kontrahent.NazwaFormatowana;
                if (Dokument.Kontrahent.EMAIL != null) Wiadomosc.Do = Dokument.Kontrahent.EMAIL;
            }

            Attachment zal = new Soneta.Business.Db.Attachment(Wiadomosc, AttachmentType.Attachments);
            zal.Name = ReportName;
            BusinessModule.GetInstance(Dokument).Attachments.AddRow(zal);
            zal.LoadFromStream(stream);

            var WiadomoscDokumentu = new WiadomoscExt(Wiadomosc, Dokument);
            PocztaExtModule.GetInstance(Dokument).WiadomosciExt.AddRow(WiadomoscDokumentu);
            return Wiadomosc;
        }
    }
}
