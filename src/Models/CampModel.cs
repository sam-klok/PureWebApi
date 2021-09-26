﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureWebApi.Models
{
    public class CampModel
    {
        public string Name { get; set; }
        public string Moniker { get; set; }
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        public int Length { get; set; } = 1;
    }
}
