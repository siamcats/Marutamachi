using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pcsc;

namespace Marutamachi.Model
{
    public enum MoneyType
    {
        Suica,
        nanaco,
        WAON,
        Edy
    }

    public class Card
    {
        public string Idm { get; set; }

        public string Name { get; set; }

        public CardName CardType { get; set; }

        public MoneyType MoneyType { get; set; }

        public Decimal Balance { get; set; }

        private bool _isSave;
        public bool IsSave
        {
            get { return _isSave; }
            set
            {
                _isSave = value;
                RaisePropertyChanged("IsFavorite");
            }
        }

        public DateTime LastUseTime { get; set; }

        public DateTime LastReadTime { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
