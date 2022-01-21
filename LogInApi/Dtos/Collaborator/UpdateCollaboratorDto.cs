using System;

namespace LogInApi.Dtos {
    public class UpdateCollaboratorDto {
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
        /// Guid : Collaborator's Address Object Id.
        /// </summary>
        public Guid? AddressId { get; set; }
    }
}