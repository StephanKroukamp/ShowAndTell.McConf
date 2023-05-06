using System.Threading.Tasks;

namespace ShowAndTell.McConf.Application.Common.Interfaces
{
    public interface IOpenAiClient
    { 
        Task<string> GenerateText(string prompt);
    }
}