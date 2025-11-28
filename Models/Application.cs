using System;

namespace ShelterApp.Models
{
    public class Application
    {
        public int Id { get; set; }
        public int IdGuardian { get; set; }
        public int IdAnimal { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string ApplicationStatus { get; set; }
        public int? IdEmployee { get; set; }
    }
}
