using DagensTV.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DagensTV.Models
{
    public class PopularContent
    {        
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string ImgTitle { get; set; }
        public string Icon { get; set; }
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }

        public virtual Channel Channel { get; set; }
    }


}