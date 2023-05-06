using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Domain.Repositories;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.GetPromptById
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetPromptByIdQueryHandler : IRequestHandler<GetPromptByIdQuery, PromptDto>
    {
        private readonly IPromptRepository _promptRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetPromptByIdQueryHandler(IPromptRepository promptRepository, IMapper mapper)
        {
            _promptRepository = promptRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<PromptDto> Handle(GetPromptByIdQuery request, CancellationToken cancellationToken)
        {
            var prompt = await _promptRepository.FindByIdAsync(request.Id, cancellationToken);
            return prompt.MapToPromptDto(_mapper);
        }
    }
}