using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual string OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if(PropertyChanged != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, args);
            }
            return propertyName;

        }

        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if(!Equals(storage,value))
            {
                storage = value;

                if(propertyName != null)
                {
                    OnPropertyChanged(propertyName);
                }
                return true;
            }
            return false;
        }

    }
}
