using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Domain.Repositories;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.GetPrompts
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetPromptsQueryHandler : IRequestHandler<GetPromptsQuery, List<PromptDto>>
    {
        private readonly IPromptRepository _promptRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetPromptsQueryHandler(IPromptRepository promptRepository, IMapper mapper)
        {
            _promptRepository = promptRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<PromptDto>> Handle(GetPromptsQuery request, CancellationToken cancellationToken)
        {
            var prompts = await _promptRepository.FindAllAsync(cancellationToken);
            return prompts.MapToPromptDtoList(_mapper);
        }
    }
}