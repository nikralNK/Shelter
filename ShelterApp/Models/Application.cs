using System;
using System.ComponentModel;

namespace ShelterApp.Models
{
    public class Application : INotifyPropertyChanged
    {
        private int id;
        private int idGuardian;
        private int idAnimal;
        private DateTime submissionDate;
        private string applicationStatus;
        private int? idEmployee;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public int IdGuardian
        {
            get => idGuardian;
            set { idGuardian = value; OnPropertyChanged(nameof(IdGuardian)); }
        }

        public int IdAnimal
        {
            get => idAnimal;
            set { idAnimal = value; OnPropertyChanged(nameof(IdAnimal)); }
        }

        public DateTime SubmissionDate
        {
            get => submissionDate;
            set { submissionDate = value; OnPropertyChanged(nameof(SubmissionDate)); }
        }

        public string ApplicationStatus
        {
            get => applicationStatus;
            set { applicationStatus = value; OnPropertyChanged(nameof(ApplicationStatus)); }
        }

        public int? IdEmployee
        {
            get => idEmployee;
            set { idEmployee = value; OnPropertyChanged(nameof(IdEmployee)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
