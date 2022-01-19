using System;
namespace LogInApi.Models {
    public class Collaborator {
        public string Cpf { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}