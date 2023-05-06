using ShowAndTell.McConf.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Infrastructure
{
    public class VideoResponse
    {
        public string DateCreated { get; set; }
        public List<Video> Data { get; set; }
    }
}
