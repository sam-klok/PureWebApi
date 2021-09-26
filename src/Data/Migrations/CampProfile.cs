using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureWebApi.Data.Migrations
{
    public class CampProfile: Profile
    {
        public CampProfile()
        {
            this.CreateMap<CampProfile, Models.CampModel>();
        }
    }
}
