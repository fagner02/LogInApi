using System;
using System.Globalization;
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
            CreateMap<CollaboratorDto, Collaborator>()
                .ForMember(x => x.BirthDate, opt => opt.MapFrom(
                    x => DateTime.ParseExact(x.BirthDate, "mm/dd/yyyy", new CultureInfo("en-US"))))
                .ReverseMap()
                .ForMember(x => x.BirthDate, opt => opt
                    .MapFrom(x => x.BirthDate.ToString("mm/dd/yyyy", new CultureInfo("en-US"))));

            CreateMap<CreateCollaboratorDto, Collaborator>()
                .ForMember(x => x.BirthDate, opt => opt.MapFrom(
                    x => DateTime.ParseExact(x.BirthDate, "mm/dd/yyyy", new CultureInfo("en-US"))))
                .ReverseMap()
                .ForMember(x => x.BirthDate, opt => opt
                    .MapFrom(x => x.BirthDate.ToString("mm/dd/yyyy", new CultureInfo("en-US"))));

            CreateMap<UpdateCollaboratorDto, Collaborator>().ReverseMap();
        }
    }
}