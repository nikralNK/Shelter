using System;
using System.ComponentModel;

namespace ShelterApp.Models
{
    public class User : INotifyPropertyChanged
    {
        private int id;
        private string username;
        private string passwordHash;
        private string email;
        private string fullName;
        private string role;
        private string photo;
        private int? idGuardian;
        private DateTime createdAt;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Username
        {
            get => username;
            set { username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string PasswordHash
        {
            get => passwordHash;
            set { passwordHash = value; OnPropertyChanged(nameof(PasswordHash)); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string FullName
        {
            get => fullName;
            set { fullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        public string Role
        {
            get => role;
            set { role = value; OnPropertyChanged(nameof(Role)); }
        }

        public string Photo
        {
            get => photo;
            set { photo = value; OnPropertyChanged(nameof(Photo)); }
        }

        public int? IdGuardian
        {
            get => idGuardian;
            set { idGuardian = value; OnPropertyChanged(nameof(IdGuardian)); }
        }

        public DateTime CreatedAt
        {
            get => createdAt;
            set { createdAt = value; OnPropertyChanged(nameof(CreatedAt)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
