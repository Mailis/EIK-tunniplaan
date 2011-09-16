using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EIKtunniplaan
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string aineNimi;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Ainenimi
        {
            get
            {
                return aineNimi;
            }
            set
            {
                if (value != aineNimi)
                {
                    aineNimi = value;
                    NotifyPropertyChanged("Ainenimi");
                }
            }
        }

        private string ruum;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Ruum
        {
            get
            {
                return ruum;
            }
            set
            {
                if (value != ruum)
                {
                    ruum = value;
                    NotifyPropertyChanged("Ruum");
                }
            }
        }

        private string oppejoud;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Oppejoud
        {
            get
            {
                return oppejoud;
            }
            set
            {
                if (value != oppejoud)
                {
                    oppejoud = value;
                    NotifyPropertyChanged("Oppejoud");
                }
            }
        }

        private string ruhmad;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Ruhmad
        {
            get
            {
                return ruhmad;
            }
            set
            {
                if (value != ruhmad)
                {
                    ruhmad = value;
                    NotifyPropertyChanged("Ruhmad");
                }
            }
        }

        private string kellaaeg;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Kellaaeg
        {
            get
            {
                return kellaaeg;
            }
            set
            {
                if (value != kellaaeg)
                {
                    kellaaeg = value;
                    NotifyPropertyChanged("Kellaaeg");
                }
            }
        }

        private string alguskellaaeg;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Alguskellaaeg
        {
            get
            {
                return alguskellaaeg;
            }
            set
            {
                if (value != alguskellaaeg)
                {
                    alguskellaaeg = value;
                    NotifyPropertyChanged("Alguskellaaeg");
                }
            }
        }

        private string nadalaP;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string NadalaP
        {
            get
            {
                return nadalaP;
            }
            set
            {
                if (value != nadalaP)
                {
                    nadalaP = value;
                    NotifyPropertyChanged("NadalaP");
                }
            }
        }
        private string aineCode;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string AineCode
        {
            get
            {
                return aineCode;
            }
            set
            {
                if (value != aineCode)
                {
                    aineCode = value;
                    NotifyPropertyChanged("AineCode");
                }
            }
        }
        private string l6pukellaaeg;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string L6pukellaaeg
        {
            get
            {
                return l6pukellaaeg;
            }
            set
            {
                if (value != l6pukellaaeg)
                {
                    l6pukellaaeg = value;
                    NotifyPropertyChanged("L6pukellaaeg");
                }
            }
        }

        private string tyyp;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Tyyp
        {
            get
            {
                return tyyp;
            }
            set
            {
                if (value != tyyp)
                {
                    tyyp = value;
                    NotifyPropertyChanged("Tyyp");
                }
            }
        }

        private string periood;
        public string Periood
        {
            get
            {
                return periood;
            }
            set
            {
                if (value != periood)
                {
                    periood = value;
                    NotifyPropertyChanged("Periood");
                }
            }
        }

        private string sagedus;
        public string Sagedus
        {
            get
            {
                return sagedus;
            }
            set
            {
                if (value != sagedus)
                {
                    sagedus = value;
                    NotifyPropertyChanged("Sagedus");
                }
            }
        }

        private string voor;
        public string Voor
        {
            get
            {
                return voor;
            }
            set
            {
                if (value != voor)
                {
                    voor = value;
                    NotifyPropertyChanged("Voor");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}