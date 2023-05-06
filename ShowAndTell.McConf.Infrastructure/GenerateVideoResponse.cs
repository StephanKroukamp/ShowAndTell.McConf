using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Infrastructure
{
    public class GenerateVideoResponse
    {
        public List<ImageGeneration> Data { get; set; }

        public class ImageGeneration
        {
            public string Url { get; set; }
        }
    }
}
