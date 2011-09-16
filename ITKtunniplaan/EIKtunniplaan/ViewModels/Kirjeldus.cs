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

namespace EIKtunniplaan.ViewModels
{
    /// <summary>
    /// Õppeaine kirjeldus; ilmub, kui mouseOver.
    /// </summary>
    public class Kirjeldus
    {
        private string tyyp;
        private string sagedus;
        private string periood;
        private string voor;
        private string aeg;
        private string ainekood;
        private string ruumiNR;
        private string oppejoud;
        private string ryhmaNR;


        public string Aeg
        {
            get { return aeg; }
            set { aeg = value; }
        }
        public string Tyyp
        {
            get { return tyyp; }
            set { tyyp = value; }
        }

        public string Sagedus
        {
            get { return sagedus; }
            set { sagedus = value; }
        }

        public string Periood
        {
            get { return periood; }
            set { periood = value; }
        }

        public string Voor
        {
            get { return voor; }
            set { voor = value; }
        }

        public string Ainekood
        {
            get { return ainekood; }
            set { ainekood = value; }
        }
        public string RuumiNR
        {
            get { return ruumiNR; }
            set { ruumiNR = value; }
        }

        public string Oppejoud
        {
            get { return oppejoud; }
            set { oppejoud = value; }
        }

        public string RyhmaNR
        {
            get { return ryhmaNR; }
            set { ryhmaNR = value; }
        }


    }
}
