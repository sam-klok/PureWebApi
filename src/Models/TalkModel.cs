using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PureWebApi.Models
{
    public class TalkModel
    {
        public int TalkId { get; set; }
        public string Title { get; set; }

        [Required]
        [MinLength(50)]

        public string Abstract { get; set; }
        public int Level { get; set; }

        public SpeakerModel Speaker{ get; set; }
    }
}
