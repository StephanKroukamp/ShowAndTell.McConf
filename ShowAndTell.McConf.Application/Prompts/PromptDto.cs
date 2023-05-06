using System;
using System.Collections.Generic;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using ShowAndTell.McConf.Application.Common.Mappings;
using ShowAndTell.McConf.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts
{

    public class PromptDto : IMapFrom<Prompt>
    {
        public PromptDto()
        {
        }

        public static PromptDto Create(
            Guid id)
        {
            return new PromptDto
            {
                Id = id,
            };
        }

        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Prompt, PromptDto>();
        }
    }
}