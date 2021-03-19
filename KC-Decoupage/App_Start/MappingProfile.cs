using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KC_Decoupage.Dtos;
using KC_Decoupage.Models;
using AutoMapper;


namespace KC_Decoupage.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Post, PostDto>().ForMember(p => p.Id,opt => opt.Ignore());

            Mapper.CreateMap<PostDto, Post>();

        }

    }
}