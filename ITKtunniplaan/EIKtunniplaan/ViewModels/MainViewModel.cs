using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.Phone.Net.NetworkInformation;
using System.Net;
using System.Globalization;
using EIKtunniplaan.ViewModels;
using System.IO.IsolatedStorage;
using System.IO;


namespace EIKtunniplaan
{
    public class MainViewModel : INotifyPropertyChanged
    {
         Kirjeldus kirjeldus;
        public int sisestatudNadalaNr;
         //private string save_ical = "schedule.ics";

        public MainViewModel()
        {
            this.esmasp = new ObservableCollection<ItemViewModel>();
            this.teisip = new ObservableCollection<ItemViewModel>();
            this.kolmap = new ObservableCollection<ItemViewModel>();
            this.neljap = new ObservableCollection<ItemViewModel>();
            this.reede = new ObservableCollection<ItemViewModel>();
            this.laup = new ObservableCollection<ItemViewModel>();
            this.pyhap = new ObservableCollection<ItemViewModel>();

            this.sisestatudNadalaNr = kaesolevaNadalaNumber();
        }

        /// <summary>
        /// A collections for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> esmasp { get; private set; }
        public ObservableCollection<ItemViewModel> teisip { get; private set; }
        public ObservableCollection<ItemViewModel> kolmap { get; private set; }
        public ObservableCollection<ItemViewModel> neljap { get; private set; }
        public ObservableCollection<ItemViewModel> reede { get; private set; }
        public ObservableCollection<ItemViewModel> laup { get; private set; }
        public ObservableCollection<ItemViewModel> pyhap { get; private set; }


        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /////////////////////////////////DOWNLOAD//////////////////////////
        #region ANDMETE URL
        /// <summary>
        /// kui on netiühendus ja kõik korras, saab netist tunniplaani URLi,
        /// muidu võtab varem salvestatud failist.
        /// </summary>
        public void LoadData()
        {
            ////kui on netiühendus korras
            //if (NetworkInterface.GetIsNetworkAvailable() == true)
            //{
            downloadString(new Uri(@"http://enos.itcollege.ee/~mtoompuu/c/ical2.ics"), LoeAndmed);
            //downloadString(new Uri(@"https://itcollege.ois.ee/et/schedule"), LoeAndmed);
            //}
            //else
            //{
                //string vanaAvaleheXMl = LoadData_xml(FILE_NAME_StationItemViewModel);
                //jaamadeNimekiri(vanaAvaleheXMl);
                //LoeJaamaAndmed(vanaAvaleheXMl);

                //string vanaPrognoosXMl = LoadData_xml(FILE_NAME_Forecast);
                //naita_prognoose(vanaPrognoosXMl);

            //}                


            this.IsDataLoaded = true;
        }
        #endregion 
        #region download
        private void downloadString(Uri url, Action<String> handler)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.GetEncoding("utf-8");
            webClient.DownloadStringAsync(url);
            webClient.DownloadStringCompleted += (Object sender, DownloadStringCompletedEventArgs e) => downloadStringCompleted(sender, e, handler);
        }

        private void downloadStringCompleted(Object sender, DownloadStringCompletedEventArgs e, Action<String> handler)
        {
            handler(e.Result);
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        ////////////////////////
        #region LOEB ANDMED SISSE
        public void LoeAndmed(string sisu)
        {
            esmasp.Clear();
            teisip.Clear();
            kolmap.Clear();
            neljap.Clear();
            reede.Clear();
            laup.Clear();
            pyhap.Clear();

            string vastus = sisu;
            string[] jupitajad = new string[1];
            jupitajad[0] = "END:VEVENT";

            string[] jupid = vastus.Split(jupitajad, StringSplitOptions.RemoveEmptyEntries);


            List<Aine> esimeseds = anna_andmed(jupid);
            foreach (Aine x in esimeseds)
            {
                int nadalaNR = teeAeg(x.Tunnialgus);
                ItemViewModel ivm = new ItemViewModel();
                ivm.Ainenimi = x.Pealkiri;
                ivm.Alguskellaaeg = TeeKuuPaevaRida(x.Tunnialgus);
                ivm.L6pukellaaeg = "Lõpp:" + x.Tunnilopp ;
                ivm.NadalaP += x.NadalaPaev;

                List<Kirjeldus> teisedasjad = new List<Kirjeldus>();
                teisedasjad = x.Kirjed;
                foreach (Kirjeldus k in teisedasjad)
                {

                    ivm.AineCode = "Ainekood: " + k.Ainekood;
                    ivm.Ruhmad = "Rühmad: " + k.RyhmaNR;
                    ivm.Kellaaeg = k.Aeg;
                    ivm.Ruum = "ruum: " + k.RuumiNR;
                    ivm.Oppejoud = k.Oppejoud;
                    ivm.Tyyp = "Tüüp: " + k.Tyyp;
                    ivm.Periood = "Periood: " + k.Periood;
                    ivm.Sagedus = "Sagedus: " + k.Sagedus;
                    ivm.Voor = "Voor: " + k.Voor;
                }
                                                                                   
                //kollektsioneeri päevade kaupa
                if (sisestatudNadalaNr == nadalaNR)//sisestatudNadalaNr
                {
                    if (ivm.NadalaP.ToLower().StartsWith("esmasp"))
                        esmasp.Add(ivm);
                    if (ivm.NadalaP.ToLower().StartsWith("teisip"))
                        teisip.Add(ivm);
                    if (ivm.NadalaP.ToLower().StartsWith("kolmap"))
                        kolmap.Add(ivm);
                    if (ivm.NadalaP.ToLower().StartsWith("neljap"))
                        neljap.Add(ivm);
                    if (ivm.NadalaP.ToLower().StartsWith("reed"))
                        reede.Add(ivm);
                    if (ivm.NadalaP.ToLower().StartsWith("laup"))
                        laup.Add(ivm);
                    if (ivm.NadalaP.ToLower().StartsWith("pühap"))
                        pyhap.Add(ivm);
                }
            }

            //TaidaAndmed();
        }
        public int kaesolevaNadalaNumber()
        {
            //käespoleva nädala nr, vaja muuta interaktiivseks
            // Gets the Calendar instance associated with a CultureInfo.
            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            // Displays the number of the current week relative to the beginning of the year.
            //Console.WriteLine("The CalendarWeekRule used for the en-US culture is {0}.", myCWR);
            //Console.WriteLine("The FirstDayOfWeek used for the en-US culture is {0}.", myFirstDOW);
            return myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);
        }
        /// <summary>
        /// loeb andmed sisse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public List<Aine> anna_andmed(string[] algusjupid)
        {

            List<Aine> listi = new List<Aine>();



            for (int i = 1; i < algusjupid.Length; i++)//algusjupid.Length
            {

                Aine mingidAsjad = new Aine();

                string tulem = algusjupid[i];
                string[] vahekoht = new String[1];
                vahekoht[0] = "\n";
                string[] tykk = tulem.Split(vahekoht, StringSplitOptions.RemoveEmptyEntries);

                foreach (var x in tykk)
                {


                    if (x.Contains("SUMMARY:"))
                    {
                        mingidAsjad.Pealkiri = x.Substring(LeiaKoolon(x), x.Length - LeiaKoolon(x));
                    }
                    if (x.Contains("DTSTART:"))
                    {
                        mingidAsjad.Tunnialgus = TeisendaKuupaev(tykk[4].Substring(8)).ToString();
                        mingidAsjad.Tunnilopp = TeisendaKuupaev(tykk[5].Substring(6)).ToString();
                        mingidAsjad.NadalaPaev = annaPaevanimi(tykk);
                    }
                    if (x.Contains("DESCRIPTION:"))
                    {
                        
                        List<Kirjeldus> teisedasjad = new List<Kirjeldus>();

                        string[] jupitajake = new string[1];
                        jupitajake[0] = "\\n";
                        string[] kirjeldused = x.Split(jupitajake, StringSplitOptions.RemoveEmptyEntries);

                        kirjeldus = new Kirjeldus();
                        foreach (var y in kirjeldused)
                        {


                            if (y.Contains("Ainekood:"))
                            {
                                kirjeldus.Ainekood = y.Substring(LeiaKoolon(y), y.Length - LeiaKoolon(y));
                            }
                            if (y.Contains("hmad:"))
                            {
                                kirjeldus.RyhmaNR = y.Substring(LeiaKoolon(y), y.Length - LeiaKoolon(y));
                            }
                            if (y.Contains("Aeg:"))
                            {
                                kirjeldus.Aeg = y.Substring(LeiaKoolon(y), y.Length - LeiaKoolon(y));
                            }
                            if (y.Contains("Ruum:"))
                            {
                                kirjeldus.RuumiNR = y.Substring(LeiaKoolon(y), y.Length - LeiaKoolon(y));
                            }
                            if (y.Contains("ppej"))
                            {
                                kirjeldus.Oppejoud = y.Substring(LeiaKoolon(y), y.Length - LeiaKoolon(y));
                            }
                            if (y.Contains("p:"))
                            {
                                kirjeldus.Tyyp = y.Substring(LeiaKoolon(y), y.Length - LeiaKoolon(y));
                            }
                            if (y.Contains("Periood:"))
                            {
                                kirjeldus.Periood = y.Substring(LeiaKoolon(y), y.Length - LeiaKoolon(y));
                            }
                            if (y.Contains("Sagedus:"))
                            {
                                kirjeldus.Sagedus = y.Substring(LeiaKoolon(y), y.Length - LeiaKoolon(y));
                            }
                            if (y.Contains("Voor:"))
                            {
                                kirjeldus.Voor = y.Substring(LeiaKoolon(y), y.Length - LeiaKoolon(y));
                            }

                        }
                        teisedasjad.Add(kirjeldus);
                        mingidAsjad.Kirjed = teisedasjad;
                        listi.Add(mingidAsjad);

                    }//Description



                }//foreach tykk




            }//foreach

            return listi;
        }
        /// <summary>
        /// siit saab stringi indexi, mis asub kooloni järel
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int LeiaKoolon(string s)
        {
            int koolon = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ':')
                    koolon = s.IndexOf(':') + 1;

            }
            return koolon;
        }
        #endregion
        ////////////////////////

        #region PRAEGUSE NÄDALA NUMBER ARVESTADES JAANUARIST
        public int nadalaNR(DateTime dt)
        {
            int nr = 0;
            // Gets the Calendar instance associated with a CultureInfo.
            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            // Displays the number of the current week relative to the beginning of the year.
            //Console.WriteLine("The CalendarWeekRule used for the en-US culture is {0}.", myCWR);
            //Console.WriteLine("The FirstDayOfWeek used for the en-US culture is {0}.", myFirstDOW);
            nr = myCal.GetWeekOfYear(dt, myCWR, myFirstDOW);

            // Displays the total number of weeks in the current year.
            //DateTime LastDay = new System.DateTime(DateTime.Now.Year, 12, 31);
            //Console.WriteLine("There are {0} weeks in the current year ({1}).", myCal.GetWeekOfYear(LastDay, myCWR, myFirstDOW), LastDay.Year);
            return nr;
        }

        public int teeAeg(string kuup)
        {
            CultureInfo cf = CultureInfo.CurrentCulture;
            DateTime dtime = DateTime.Parse(kuup, cf);
            return nadalaNR(dtime);
        }
        #endregion

        #region KUUPAEVADE ja NÄDALAPÄEVADE TEISENDUSED
        //Tagastab sõne kujul 'kp. kuunimi aastanumber'.
        public string TeeKuuPaevaRida(string kuup)//    0m/0d/yyyy 7:45:00 AM   
        {
            CultureInfo cf = CultureInfo.CurrentCulture;
            DateTime dtime = DateTime.Parse(kuup, cf);
            // The dateTime now contains the right date/time so to format the string,
            // use the standard formatting methods of the DateTime object.
            String dt = dtime.ToShortDateString();//dt on kujul (m)m/(d)d/yyyy
            char[] jupitaja = new char[2];
            jupitaja[0] = '/';
            string[] jupid = dt.Split(jupitaja);
            string paev = jupid[1];
            string kuu = jupid[0];
            string aasta = jupid[2];
            string tulemus = paev + '.' + annaKuuNimi(Convert.ToInt16(kuu)) + ' ' + aasta;
            string printDate = tulemus;
            return printDate;
        }
         /// <summary>
        /// Tagastab kuu eestikeelse nimetuse kuu nr järgi.
        /// </summary>
        /// <param name="kuuNr"></param>
        /// <returns></returns>
        public string annaKuuNimi(int kuuNr)
        {
            if (kuuNr == 01)
                return "jaanuar";
            if (kuuNr == 02)
                return "veebruar";
            if (kuuNr == 03)
                return "märts";
            if (kuuNr == 04)
                return "aprill";
            if (kuuNr == 05)
                return "mai";
            if (kuuNr == 06)
                return "juuni";
            if (kuuNr == 07)
                return "juuli";
            if (kuuNr == 08)
                return "august";
            if (kuuNr == 09)
                return "september";
            if (kuuNr == 10)
                return "oktoober";
            if (kuuNr == 11)
                return "november";
            if (kuuNr == 12)
                return "detsember";
            else return "";
        }
       
        /// <summary>
        /// Nädalapäev
        /// </summary>
        /// <param name="tykijupid"></param>
        /// <returns></returns>
        public string annaPaevanimi(string[] tykijupid)
        {
            // kuupäeva nädalapäevaks teisendaja
            Int32 aasta = int.Parse(tykijupid[4].Substring(8, 4));
            Int32 kuu = int.Parse(tykijupid[4].Substring(12, 2));
            Int32 paev = int.Parse(tykijupid[4].Substring(14, 2));

            var dateValue = new DateTime(aasta, kuu, paev); // (aasta, kuu, päev)

            string nadalapaev = "";
            var npaev = (int)dateValue.DayOfWeek;

            switch (npaev)
            {
                case 1:
                    nadalapaev = "Esmaspäev";
                    break;

                case 2:
                    nadalapaev = "Teisipäev";
                    break;

                case 3:
                    nadalapaev = "Kolmapäev";
                    break;

                case 4:
                    nadalapaev = "Neljapäev";
                    break;

                case 5:
                    nadalapaev = "Reede";
                    break;

                case 6:
                    nadalapaev = "Laupäev";
                    break;

                case 7:
                    nadalapaev = "Pühapäev";
                    break;

                default:
                    nadalapaev = "nädalapäev määramata";
                    break;
            }

            return nadalapaev; // Näitab nädalapäeva nime
            // end  kuupäeva nädalapäevaks teisendaja
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strKuuPaev"></param>
        /// <returns></returns>
        public DateTime TeisendaKuupaev(string strKuuPaev)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.FullDateTimePattern = "yyyyMMddTHHmm";
            return DateTime.ParseExact(strKuuPaev.Substring(0, strKuuPaev.Length - 3), "yyyyMMddTHHmm", dtfi);
        }
        #endregion

        #region ISOLATED STORAGE

        public DateTime xmlSalvestamisAeg;
        public void SaveData_ical(string data, string fileName)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(fileName, FileMode.Create, isf))
                {
                    using (StreamWriter sw = new StreamWriter(isfs))
                    {
                        try
                        {
                            sw.Write(data);
                            sw.Close();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (sw == null) sw.Dispose();
                        }
                    }
                }
            }
        }

        public string LoadData_ical(string fileName)
        {
            string data = String.Empty;
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(fileName, FileMode.Open, isf))
                {
                    using (StreamReader sr = new StreamReader(isfs))
                    {
                        try
                        {
                            string lineOfData = String.Empty;

                            while ((lineOfData = sr.ReadLine()) != null)
                                data += lineOfData;

                            sr.Close();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (sr == null) sr.Dispose();
                        }
                    }
                }
            }
            return data;
        }

        #endregion
       
    }
}