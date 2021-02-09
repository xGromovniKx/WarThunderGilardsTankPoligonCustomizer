using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class GlobalneVarijabile
    {
        private static List<Tuple<string, string, string, string>> listaSviTenkovi = new List<Tuple<string, string, string, string>>();
        public static List<Tuple<string, string, string, string>> listaSviTenkoviSave
        {
          get { return listaSviTenkovi; }
          set { listaSviTenkovi = value; }
        }

        // fajl za snimiti
        private static string strOsnovniPB = string.Empty;
        public static string strPoligonSaveAs
        {
            get { return strOsnovniPB; }
            set { strOsnovniPB = value; }
        }
    }
}
