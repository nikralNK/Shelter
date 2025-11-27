using System;
using System.ComponentModel;

namespace ShelterApp.Models
{
    public class Animal : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string type;
        private string breed;
        private DateTime? dateOfBirth;
        private int? idEnclosure;
        private int? idGuardian;
        private string currentStatus;
        private string gender;
        private string size;
        private string temperament;
        private string photo1;
        private string photo2;
        private string photo3;

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

        public string Type
        {
            get => type;
            set { type = value; OnPropertyChanged(nameof(Type)); }
        }

        public string Breed
        {
            get => breed;
            set { breed = value; OnPropertyChanged(nameof(Breed)); }
        }

        public DateTime? DateOfBirth
        {
            get => dateOfBirth;
            set { dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth)); OnPropertyChanged(nameof(Age)); }
        }

        public int? IdEnclosure
        {
            get => idEnclosure;
            set { idEnclosure = value; OnPropertyChanged(nameof(IdEnclosure)); }
        }

        public int? IdGuardian
        {
            get => idGuardian;
            set { idGuardian = value; OnPropertyChanged(nameof(IdGuardian)); }
        }

        public string CurrentStatus
        {
            get => currentStatus;
            set { currentStatus = value; OnPropertyChanged(nameof(CurrentStatus)); }
        }

        public string Gender
        {
            get => gender;
            set { gender = value; OnPropertyChanged(nameof(Gender)); }
        }

        public string Size
        {
            get => size;
            set { size = value; OnPropertyChanged(nameof(Size)); }
        }

        public string Temperament
        {
            get => temperament;
            set { temperament = value; OnPropertyChanged(nameof(Temperament)); }
        }

        public string Photo1
        {
            get => photo1;
            set { photo1 = value; OnPropertyChanged(nameof(Photo1)); }
        }

        public string Photo2
        {
            get => photo2;
            set { photo2 = value; OnPropertyChanged(nameof(Photo2)); }
        }

        public string Photo3
        {
            get => photo3;
            set { photo3 = value; OnPropertyChanged(nameof(Photo3)); }
        }

        public int Age
        {
            get
            {
                if (dateOfBirth.HasValue)
                {
                    return DateTime.Now.Year - dateOfBirth.Value.Year;
                }
                return 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
