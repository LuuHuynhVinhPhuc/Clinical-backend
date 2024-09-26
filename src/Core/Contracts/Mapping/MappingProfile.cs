using AutoMapper;
using ClinicalBackend.Contracts.DTOs;
using ClinicalBackend.Domain.Entities;

namespace ClinicalBackend.Contracts.Mapping 
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FollowUp, FollowUpDto>().ReverseMap();
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<(List<FollowUp>, PaginationInfoDto), QueryFollowUpsResponseDto>().ReverseMap();
        }
    }
}