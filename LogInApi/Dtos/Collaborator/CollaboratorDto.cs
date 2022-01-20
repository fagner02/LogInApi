using System;
namespace LogInApi.Dtos {
    public class CollaboratorDto {
        public string Cpf { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public AddressDto Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}