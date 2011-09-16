using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace EIKtunniplaan.ViewModels
{
    
        /// <summary>
        /// Õppeaine tunniplaanis
        /// </summary>
        public class Aine
        {
            private string aasta;
            private string kuu;
            private string kuupaev;
            private string nadalaPaev;

            public string NadalaPaev
            {
                get { return nadalaPaev; }
                set { nadalaPaev = value; }
            }

            private string tunnialgus;
            private string tunnilopp;

            public string Aasta
            {
                get { return aasta; }
                set { aasta = value; }
            }
            public string Kuu
            {
                get { return kuu; }
                set { kuu = value; }
            }
            public string Kuupaev
            {
                get { return kuupaev; }
                set { kuupaev = value; }
            }
            public string Tunnialgus
            {
                get { return tunnialgus; }
                set { tunnialgus = value; }
            }
            public string Tunnilopp
            {
                get { return tunnilopp; }
                set { tunnilopp = value; }
            }



            private string pealkiri;




            public string Pealkiri
            {
                get { return pealkiri; }
                set { pealkiri = value; }
            }




            List<Kirjeldus> kirjed;

            public List<Kirjeldus> Kirjed
            {
                get { return kirjed; }
                set { kirjed = value; }
            }
        }

  
}
