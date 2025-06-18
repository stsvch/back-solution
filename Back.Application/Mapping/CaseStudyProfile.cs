using AutoMapper;
using Back.Application.DTOs;
using Back.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Mapping
{
    public class CaseStudyProfile : Profile
    {
        public CaseStudyProfile()
        {
            CreateMap<CaseStudy, CaseStudyDto>()
                .ForMember(
                    d => d.PhotoPaths,
                    opt => opt.MapFrom(src => src.Photos.Select(p => p.Path))
                );
        }
    }
}
