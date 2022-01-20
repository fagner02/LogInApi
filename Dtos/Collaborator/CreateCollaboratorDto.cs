using System;
namespace LogInApi.Dtos {
    public class CreateCollaboratorDto {
        public string Cpf { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public Guid? AddressId { get; set; }
        public DateTime BirthDate { get; set; }
    }
}