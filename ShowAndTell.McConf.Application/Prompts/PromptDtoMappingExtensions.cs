using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using ShowAndTell.McConf.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts
{
    public static class PromptDtoMappingExtensions
    {
        public static PromptDto MapToPromptDto(this Prompt projectFrom, IMapper mapper)
        {
            return mapper.Map<PromptDto>(projectFrom);
        }

        public static List<PromptDto> MapToPromptDtoList(this IEnumerable<Prompt> projectFrom, IMapper mapper)
        {
            return projectFrom.Select(x => x.MapToPromptDto(mapper)).ToList();
        }
    }
}