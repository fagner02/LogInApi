using System;
namespace LogInApi.Dtos {
    public class CollaboratorDto {
        /// <summary>
        /// string : Collaborator's CPF.
        /// </summary>
        /// <example>
        /// 111.111.111-11
        /// </example>
        public string Cpf { get; set; }
        /// <summary>
        /// string : Collaborator's Full Name.
        /// </summary>
        /// <example>
        /// John Doe
        /// </example>
        public string FullName { get; set; }
        /// <summary>
        /// string : Collaborator's Sex.
        /// </summary>
        /// <example>
        /// M
        /// </example>
        public string Sex { get; set; }
        /// <summary>
        /// string : Collaborator's Phone.
        /// </summary>
        /// <example>
        /// 00 00000000
        /// </example>
        public string Phone { get; set; }
        /// <summary>
        /// Address : Collaborator's Address Object.
        /// </summary>
        public AddressDto Address { get; set; }
        /// <summary>
        /// DateTime : Collaborator's Birthdate.
        /// </summary>
        /// <example>
        /// 12/30/2000
        /// </example>
        public string BirthDate { get; set; }
    }
}