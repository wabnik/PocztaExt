using Soneta.Business;
using Soneta.Handel;
using Soneta.Types;

namespace Soneta.Examples.PocztaExt.WiadomosciExt
{
    public class DokumentyWiadomosciParams : ContextBase
    {
        private const string Key = "Soneta.PocztaExt.DokumentyWiadomosci";

        public DokumentyWiadomosciParams(Context cx) : base(cx)
        {
            Load();
        }

        [Context]
        [Caption("Definicja")]
        public DefDokHandlowego Definicja
        {
            get
            {
                if (Context.Contains(typeof(DefDokHandlowego)))
                    return (DefDokHandlowego)Context[typeof(DefDokHandlowego)];
                return null;
            }
            set
            {
                Context[typeof(DefDokHandlowego)] = value;
                Save();
            }
        }

        protected void Load()
        {
            SetContext(typeof(DefDokHandlowego), Session.Login.Load(this, Key, "Definicja"));
        }

        protected void Save()
        {
            Session.Login.Save(this, Key, "Definicja");
        }
    }
}