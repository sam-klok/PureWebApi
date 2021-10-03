using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PureWebApi.Models;
using PureWebApiCore.Data;


namespace PureWebApi.Data
{
    public class CampProfile: Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>()
                //.ForMember(c=>c.Talks, o=>o.MapFrom(m=>m.Talks))
                .ForMember(c => c.VenueName, o => o.MapFrom(m => m.Location.VenueName));

            //this.CreateMap<Camp, Models.CampModel>();
            this.CreateMap<Models.CampModel, Camp>(); //I'm surprised that mapper require me to create two maps, and it doesn't auto-revers map

            this.CreateMap<Talk, Models.TalkModel>()
                .ReverseMap()
                .ForMember(t => t.Camp, opt => opt.Ignore()) // ignore map back camp, so we don't update it
                .ForMember(t => t.Speaker, opt => opt.Ignore());

            this.CreateMap<Speaker, Models.SpeakerModel>().ReverseMap();
            //this.CreateMap<Models.SpeakerModel, Speaker>();
        }
    }
}
