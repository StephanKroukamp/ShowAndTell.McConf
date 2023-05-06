using ShowAndTell.McConf.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowAndTell.McConf.Application.Common.Interfaces
{
    public interface IDALLEApiClient
    {
        Task<Video> GenerateVideo(Script script);
    }
}
