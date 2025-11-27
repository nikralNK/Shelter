using System;
using System.ComponentModel;

namespace ShelterApp.Models
{
    public class Favorite : INotifyPropertyChanged
    {
        private int id;
        private int idUser;
        private int idAnimal;
        private DateTime addedAt;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public int IdUser
        {
            get => idUser;
            set { idUser = value; OnPropertyChanged(nameof(IdUser)); }
        }

        public int IdAnimal
        {
            get => idAnimal;
            set { idAnimal = value; OnPropertyChanged(nameof(IdAnimal)); }
        }

        public DateTime AddedAt
        {
            get => addedAt;
            set { addedAt = value; OnPropertyChanged(nameof(AddedAt)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
