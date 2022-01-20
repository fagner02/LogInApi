using AutoMapper;
using LogInApi.Dtos;
using LogInApi.Models;

namespace LogInApi.Mapping {
    class Mapping : Profile {
        public Mapping() {
            // ADDRESS MAPPING 
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<CreateAddressDto, Address>().ReverseMap();
            CreateMap<UpdateAddressDto, Address>().ReverseMap();

            // COLLABORATOR MAPPING
            CreateMap<CollaboratorDto, Collaborator>().ReverseMap();
            CreateMap<CreateCollaboratorDto, Collaborator>().ReverseMap();
            CreateMap<UpdateCollaboratorDto, Collaborator>().ReverseMap();
        }
    }
}