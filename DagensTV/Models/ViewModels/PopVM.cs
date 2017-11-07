using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DagensTV.Models.ViewModels
{
    public class PopVM
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        [Display(Name = "Nyhetspuffar")]
        public Nullable<int> ScheduleId { get; set; }

        public List<ScheduleVM> Schedules = new List<ScheduleVM>();

        public List<PopularContent> Popular = new List<PopularContent>();
    }
}