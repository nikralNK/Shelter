using System;
using System.ComponentModel;

namespace ShelterApp.Models
{
    public class Guardian : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string number;
        private string email;
        private string address;
        private DateTime? guardianshipDay;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Number
        {
            get => number;
            set { number = value; OnPropertyChanged(nameof(Number)); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string Address
        {
            get => address;
            set { address = value; OnPropertyChanged(nameof(Address)); }
        }

        public DateTime? GuardianshipDay
        {
            get => guardianshipDay;
            set { guardianshipDay = value; OnPropertyChanged(nameof(GuardianshipDay)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
