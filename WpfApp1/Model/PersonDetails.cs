using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class PersonDetails : PropertyChangedBase
    {

        public PersonDetails()
        {

        }

        private string _name;
        private string _address;
        private string _country;
        private string _postalZip;
        private string _email;
        private string _phone;

        
        public string name
        {
            get=> _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged();
                }
            }
        }

        public string country
        {
            get { return _country; }
            set
            {
                if(_country != value)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        public string postalZip
        {
            get => _postalZip;  
            set
            {
                if (_postalZip != value)
                {
                    _postalZip = value;
                    OnPropertyChanged();
                }
            }
        }

        public string email
        {
            get => _email;
            set
            {
                if(_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string phone
        {
            get => _phone;
            set
            {
                if(_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged();
                }
            }
        }
        

    }
}
