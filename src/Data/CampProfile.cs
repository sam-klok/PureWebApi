using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PureWebApiCore.Data;


namespace PureWebApi.Data
{
    public class CampProfile: Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, Models.CampModel>();
            this.CreateMap<Models.CampModel, Camp>(); //I'm surprised that mapper require me to create two maps, and it doesn't auto-revers map

            this.CreateMap<Talk, Models.TalkModel>();  // it's important to add if you want to map Talk
            this.CreateMap<Speaker, Models.SpeakerModel>();
        }
    }
}
