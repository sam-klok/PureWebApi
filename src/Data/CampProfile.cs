using AutoMapper;
using PureWebApiCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureWebApi.Data
{
    public class CampProfile: Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, Models.CampModel>();
            this.CreateMap<Talk, Models.TalkModel>();  // it's important to add if you want to map Talk
            this.CreateMap<Speaker, Models.SpeakerModel>();
        }
    }
}
