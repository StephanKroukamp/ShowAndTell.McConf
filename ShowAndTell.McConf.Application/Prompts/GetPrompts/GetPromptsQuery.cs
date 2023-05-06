using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Application.Common.Interfaces;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.GetPrompts
{
    public class GetPromptsQuery : IRequest<List<PromptDto>>, IQuery
    {
    }
}