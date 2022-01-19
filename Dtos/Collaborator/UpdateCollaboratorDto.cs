namespace LogInApi.Dtos {
    public class UpdateCollaboratorDto {
        public string FullName { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public AddressDto Address { get; set; }
    }
}