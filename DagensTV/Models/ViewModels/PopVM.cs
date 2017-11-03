using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DagensTV.Models.ViewModels
{
    public class PopVM
    {

        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string ImgTitle { get; set; }
        public string Icon { get; set; }

        public List<ChannelVM> Channels { get; set; }



    }
}