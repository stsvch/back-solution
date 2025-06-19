using AutoMapper;
using Back.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Mapping
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<Back.Domain.Entities.News, NewsDto>()
                .ForMember(
                    dst => dst.PhotoPaths,
                    opt => opt.MapFrom(src => src.Photos.Select(p => p.Path))
                );
        }
    }
}
