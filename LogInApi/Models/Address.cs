using System.Collections.Generic;
using System;

namespace LogInApi.Models
{
    public class Address
    {
        public Address()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public IEnumerable<Collaborator> Collaborators { get; set; }
        public bool IsActive { get; set; } = true;
    }
}