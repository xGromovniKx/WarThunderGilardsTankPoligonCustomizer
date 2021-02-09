using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Win32;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {   
        string strTankPoligonBase = string.Empty;
        string strTankPBOsnovni = string.Empty;
        string strTankStart = "tankModels{";
        string strTankEnd = "way{";
        string strTankIme = "name:t=\"";
        string strTankKlasa = "unit_class:t=\"";
        string strZnakNavoda = "\"";
        string strDonjaCrta = "_";
        string strDodajTenkove = "Dodaj_tenkove";
        string strGermSquad = "Dodaj_nemacke_tenkove_u_squad";
        string strRusijaSquad = "Dodaj_ruske_tenkove_u_squad";
        string strAmeriSquad = "Dodaj_americke_tenkove_u_squad";
        string strBritanijaSquad = "Dodaj_britanske_tenkove_u_squad";
        string strWTInstallDir = null;
        Encoding enkodingCirilica = Encoding.GetEncoding(1251);
        Assembly assembly = Assembly.GetExecutingAssembly();
        string strTankPFajlEmbededPutanja = "WindowsFormsApplication1.TankPolygon_Base.blk";
        string strTankPOsnovniEmbPutanja = "WindowsFormsApplication1.TankPolygon_BaseOsnovni.blk";
        string strUsSquadEmbPutanja = "WindowsFormsApplication1.Resources.squad_USATanks.blk";
        string strUkSquadEmbPutanja = "WindowsFormsApplication1.Resources.squad_UKTanks.blk";
        string strUssrSquadEmbPutanja = "WindowsFormsApplication1.Resources.squad_USSRTanks.blk";
        string strGermSquadEmbPutanja = "WindowsFormsApplication1.Resources.squad_GermanTanks.blk";

        public Form1()
        {
            InitializeComponent();
            //var listaRisorsa = assembly.GetManifestResourceNames();
            try
            {
                using (Stream stream1 = assembly.GetManifestResourceStream(strTankPFajlEmbededPutanja))
                using (StreamReader reader1 = new StreamReader(stream1, enkodingCirilica))
                {
                    strTankPoligonBase = reader1.ReadToEnd();
                }
                using (Stream stream2 = assembly.GetManifestResourceStream(strTankPOsnovniEmbPutanja))
                using (StreamReader reader2 = new StreamReader(stream2, enkodingCirilica))
                {
                    strTankPBOsnovni = reader2.ReadToEnd();
                }
                using (Stream stream2 = assembly.GetManifestResourceStream(strUsSquadEmbPutanja))
                using (StreamReader reader2 = new StreamReader(stream2, enkodingCirilica))
                {
                    string red;
                    // citaj sve dok nema vise redova
                    while ((red = reader2.ReadLine()) != null)
                    {
                        listBox1.Items.Add(red);
                    }
                }
                using (Stream stream2 = assembly.GetManifestResourceStream(strGermSquadEmbPutanja))
                using (StreamReader reader2 = new StreamReader(stream2, enkodingCirilica))
                {
                    string red;
                    // citaj sve dok nema vise redova
                    while ((red = reader2.ReadLine()) != null)
                    {
                        listBox2.Items.Add(red);
                    }
                }
                using (Stream stream2 = assembly.GetManifestResourceStream(strUssrSquadEmbPutanja))
                using (StreamReader reader2 = new StreamReader(stream2, enkodingCirilica))
                {
                    string red;
                    // citaj sve dok nema vise redova
                    while ((red = reader2.ReadLine()) != null)
                    {
                        listBox3.Items.Add(red);
                    }
                }
                using (Stream stream2 = assembly.GetManifestResourceStream(strUkSquadEmbPutanja))
                using (StreamReader reader2 = new StreamReader(stream2, enkodingCirilica))
                {
                    string red;
                    // citaj sve dok nema vise redova
                    while ((red = reader2.ReadLine()) != null)
                    {
                        listBox4.Items.Add(red);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error - ", e);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 oProgramu = new Form2();
            oProgramu.ShowDialog();
        }

 
        void pronadjiSveTenkove(string strCeoPoligon, string strStart, string strEnd)
        {
            int intStart;
            int intEnd;
            int intStartIme;
            int intEndIme;
            int intStartKlasa;
            int intEndKlasa;
            int intEndNacija;
            string strIme = string.Empty;
            string strKlasa = string.Empty;
            string strTenk = string.Empty;
            string strNacija = string.Empty;
            try
            {
                if (strCeoPoligon.Contains(strStart) && strCeoPoligon.Contains(strEnd))
                {
                    Match m = Regex.Match(strCeoPoligon, strStart);
                    while (m.Success)
                    {
                        //nadji model tenka
                        intStart = m.Index + strStart.Length;
                        intEnd = strCeoPoligon.IndexOf(strEnd, intStart);
                        strTenk = strCeoPoligon.Substring(intStart - 11, (intEnd + 27) - intStart);
                        //nadji ime tenka
                        intStartIme = strTenk.IndexOf(strTankIme) + strTankIme.Length;
                        intEndIme = strTenk.IndexOf(strZnakNavoda, intStartIme);
                        strIme = strTenk.Substring(intStartIme, intEndIme - intStartIme);
                        //nadji klasu tenka
                        intStartKlasa = strTenk.IndexOf(strTankKlasa) + strTankKlasa.Length;
                        intEndKlasa = strTenk.IndexOf(strZnakNavoda, intStartKlasa);
                        strKlasa = strTenk.Substring(intStartKlasa, intEndKlasa - intStartKlasa);
                        //nadji naciju
                        intEndNacija = strTenk.IndexOf(strDonjaCrta, intStartKlasa);
                        strNacija = strTenk.Substring(intStartKlasa, intEndNacija - intStartKlasa);
                        // popuni listu sa svim podacima
                        GlobalneVarijabile.listaSviTenkoviSave.Add(Tuple.Create(strTenk + "\n" + "\n", strIme, strKlasa, strNacija));
                        m = m.NextMatch();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error - ", e);
            }
        }

        //  SAVE AS ////////////////////
        private void SaveAs(object sender, EventArgs e)
        {
            try
            {
                List<string> strSelektovaniTenkovi = new List<string>();
                foreach (var item in listBox1.SelectedItems)
                {
                    strSelektovaniTenkovi.Add(item.ToString());
                }
                foreach (var item in listBox2.SelectedItems)
                {
                    strSelektovaniTenkovi.Add(item.ToString());
                }
                foreach (var item in listBox3.SelectedItems)
                {
                    strSelektovaniTenkovi.Add(item.ToString());
                }
                foreach (var item in listBox4.SelectedItems)
                {
                    strSelektovaniTenkovi.Add(item.ToString());
                }
                //pronadji sve tenkove
                pronadjiSveTenkove(strTankPoligonBase, strTankStart, strTankEnd);
                // dodaj tenkove u novu listu (red sa 4 itema)
                List<Tuple<string, string, string, string>> listaTenkovaZaUbacivanje = new List<Tuple<string, string, string, string>>();
                foreach (var item in strSelektovaniTenkovi)
                {
                    foreach (var itemLT in GlobalneVarijabile.listaSviTenkoviSave)
                    {
                        if (itemLT.Item2.Equals(item))
                        {
                            listaTenkovaZaUbacivanje.Add(itemLT);
                            break;
                        }
                    }
                }
                // ubaci tenkove u units
                int intUbaciTenkoveStart = strTankPBOsnovni.IndexOf(strDodajTenkove) + strDodajTenkove.Length;
                string strTemp = string.Empty;
                textBox1.Clear();
                foreach (var item in listaTenkovaZaUbacivanje)
                {
                    textBox1.AppendText(item.Item1 + "\r\n\r\n");
                }
                strTemp = strTankPBOsnovni.Insert(intUbaciTenkoveStart + 5, textBox1.Text.ToString());
                strTankPBOsnovni = strTemp;
                textBox1.Clear();
                // ubaci tenkove u squads
                //ubaci Amere
                int intUbaciUsSquadStart = strTankPBOsnovni.IndexOf(strAmeriSquad) + strAmeriSquad.Length;
                foreach (var item in listaTenkovaZaUbacivanje)
                {
                    if (item.Item4 == "us")
                        textBox1.AppendText("\r\n\tsquad_members:t=\"" + item.Item2 + "\"");
                }
                strTemp = strTankPBOsnovni.Insert(intUbaciUsSquadStart + 2, textBox1.Text.ToString());
                strTankPBOsnovni = strTemp;
                textBox1.Clear();
                // ubaci Nemce
                int intUbaciNemceSquadStart = strTankPBOsnovni.IndexOf(strGermSquad) + strGermSquad.Length;
                foreach (var item in listaTenkovaZaUbacivanje)
                {
                    if (item.Item4 == "germ")
                        textBox1.AppendText("\r\n\tsquad_members:t=\"" + item.Item2 + "\"");
                }
                strTemp = strTankPBOsnovni.Insert(intUbaciNemceSquadStart + 2, textBox1.Text.ToString());
                strTankPBOsnovni = strTemp;
                textBox1.Clear();

                // ubaci Ruse
                int intUbaciRuseSquadStart = strTankPBOsnovni.IndexOf(strRusijaSquad) + strRusijaSquad.Length;
                foreach (var item in listaTenkovaZaUbacivanje)
                {
                    if (item.Item4 == "ussr")
                        textBox1.AppendText("\r\n\tsquad_members:t=\"" + item.Item2 + "\"");
                }
                strTemp = strTankPBOsnovni.Insert(intUbaciRuseSquadStart + 2, textBox1.Text.ToString());
                strTankPBOsnovni = strTemp;
                textBox1.Clear();

                // ubaci Britance
                int intUbaciBritanceSquadStart = strTankPBOsnovni.IndexOf(strBritanijaSquad) + strBritanijaSquad.Length;
                foreach (var item in listaTenkovaZaUbacivanje)
                {
                    if (item.Item4 == "uk")
                        textBox1.AppendText("\r\n\tsquad_members:t=\"" + item.Item2 + "\"");
                }
                strTemp = strTankPBOsnovni.Insert(intUbaciBritanceSquadStart + 2, textBox1.Text.ToString());
                strTankPBOsnovni = strTemp;
                textBox1.Clear();

                ///////////////////////////////////////
                // ZAVRSENO PRAVLJENJE SAVE AS FAJLA
                ///////////////////////////////////////

                // provera u text boxu osnovnog sa ubacenim tenkovima i tenkovima u squadovim po nacijama
                textBox1.AppendText(strTankPBOsnovni);
                // otvori save dijalog box i podesi
                // nadji install putanju WT-a
                string strInstDir = nadjiWTInstallDir();
                string strUserMissionsDir = strInstDir + "\\UserMissions\\";
                // otvori save dijalog i podesi ga
                saveFileDialog1.InitialDirectory = strUserMissionsDir;
                saveFileDialog1.Title = "Save TankPolygon_Base.blk file";
                saveFileDialog1.Filter = "War Thunder blk file|*.blk|All files (*.*)|*.*";
                saveFileDialog1.DefaultExt = "blk";
                saveFileDialog1.FileName = "TankPolygon_Base.blk";

                // snimi fajl
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string strOdabraniDirektorijum = Path.GetFullPath(saveFileDialog1.FileName);
                    File.WriteAllText(strOdabraniDirektorijum, strTankPBOsnovni);
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Error - " + ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }

        }

        string nadjiWTInstallDir()
        {
            try
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    using (var key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                    using (var rkKey = key.OpenSubKey(@"Software\Gaijin\WarThunder", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey))
                    {
                        object objRegistrovanaVrednost = rkKey.GetValue("InstallDir");
                        if (rkKey != null && objRegistrovanaVrednost != null)
                        {
                            strWTInstallDir = objRegistrovanaVrednost.ToString();
                            return strWTInstallDir;
                        }
                        else
                        {
                            MessageBox.Show("Program can't find War Thunder \"UserMissions\" directory!\r\nPlease find it manually...", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return "";
                        }
                    }
                }
                else
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Gaijin\WarThunder");
                    object objRegistrovanaVrednost = key.GetValue("InstallDir");
                    if (key != null && objRegistrovanaVrednost != null)
                    {
                        strWTInstallDir = objRegistrovanaVrednost.ToString();
                        return strWTInstallDir;
                    }
                    else
                    {
                        MessageBox.Show("Program can't find War Thunder \"UserMissions\" directory!\r\nPlease find it manually...", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "";
                    }
                }
            }
            catch (Exception e)
            {
                //throw new Exception("Error - " + e.Message);
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                return "";
            }
        }


        private void listBox1_MouseHover(object sender, EventArgs e)
        {
            listBox1.Focus();
        }

        private void listBox2_MouseHover(object sender, EventArgs e)
        {
            listBox2.Focus();
        }

        private void listBox3_MouseHover(object sender, EventArgs e)
        {
            listBox3.Focus();
        }

        private void listBox4_MouseHover(object sender, EventArgs e)
        {
            listBox4.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            listBox1.ClearSelected();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            listBox2.ClearSelected();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            listBox3.ClearSelected();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            listBox4.ClearSelected();
        }

        // SELECT ALL
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.SelectedItems.Clear();
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    listBox1.SetSelected(i, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                listBox2.SelectedItems.Clear();
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    listBox2.SetSelected(i, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                listBox3.SelectedItems.Clear();
                for (int i = 0; i < listBox3.Items.Count; i++)
                {
                    listBox3.SetSelected(i, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                listBox4.SelectedItems.Clear();
                for (int i = 0; i < listBox4.Items.Count; i++)
                {
                    listBox4.SetSelected(i, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // DESELECT ALL
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            listBox1.ClearSelected();
            listBox2.ClearSelected();
            listBox3.ClearSelected();
            listBox4.ClearSelected();
        }

        private void helpReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 helpReference = new Form3();
            helpReference.ShowDialog();
        }
    }
}
