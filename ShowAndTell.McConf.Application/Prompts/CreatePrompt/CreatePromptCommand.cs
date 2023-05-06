using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Application.Common.Interfaces;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.CreatePrompt
{
    public class CreatePromptCommand : IRequest<Guid>, ICommand
    {
    }
}