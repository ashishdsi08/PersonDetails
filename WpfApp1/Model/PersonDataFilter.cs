using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class PersonDataFilter:PropertyChangedBase
    {
        public string Country { get; set; }

        private bool _isselected;

        public bool IsSelected
        {
            get { return _isselected; }

            set
            {
                if(_isselected != value)
                {
                    _isselected = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
