using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using PureWebApi.Models;
using PureWebApiCore.Data;

namespace PureWebApi.Data.Migrations
{
    public class CampProfile: Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>()
                //.ForMember(c=>c.Talks, o=>o.MapFrom(m=>m.Talks))
                .ForMember(c => c.VenueName,o => o.MapFrom(m => m.Location.VenueName));

        }
    }
}
