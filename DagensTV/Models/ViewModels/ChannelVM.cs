using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DagensTV.Models.ViewModels
{
    public class ChannelVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public List<ScheduleVM> Schedules { get; set; }
        public List<MyChannels> MyChannels { get; set; }
    }
}