using System;

namespace ShelterApp.Models
{
    public class Guardian
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? GuardianshipDay { get; set; }
    }
}
