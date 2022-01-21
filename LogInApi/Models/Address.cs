using System.Collections.Generic;
using System;

namespace LogInApi.Models {
    public class Address {
        public Address() {
            this.Id = Guid.NewGuid();
        }
        /// <summary>
        /// Guid : Primary key
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// string : Street name
        /// </summary>
        /// <example>
        /// R. Alexandre Dumas
        /// </example>
        public string Street { get; set; }
        /// <summary>
        /// string : House Number
        /// </summary>
        /// <example>
        /// 1223
        /// </example>
        public string Number { get; set; }
        /// <summary>
        /// string : District Name
        /// </summary>
        /// <example>
        /// Centro
        /// </example>
        public string District { get; set; }
        /// <summary>
        /// string : City Name
        /// </summary>
        /// <example>
        /// Quixadá
        /// </example>
        public string City { get; set; }
        /// <summary>
        /// string : State Name
        /// </summary>
        /// <example>
        /// Ceará
        /// </example>
        public string State { get; set; }
        /// <summary>
        /// IEnumerable of Collaborator : List of Collaborators related to this address
        /// </summary>
        public IEnumerable<Collaborator> Collaborators { get; set; }
        /// <summary>
        /// bool : Active state bool
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}