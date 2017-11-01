using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DagensTV.Models.ViewModels
{
    public class ScheduleVM
    {
        public int Id { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
        public Nullable<DateTime> EndTime { get; set; }
        public Nullable<int> Duration { get; set; }        
        public string Resume { get; set; }
        public Nullable<int> ChannelId { get; set; }
        public Nullable<int> ShowId { get; set; }
        public string ChannelName { get; set; }
        public string ShowName { get; set; }

        public string CategoryName { get; set; }
        public string CategoryTag { get; set; }
        public string MovieGenre { get; set; }
        public string ImdbRating { get; set; }
        public string StarImage { get; set; }
    }
}