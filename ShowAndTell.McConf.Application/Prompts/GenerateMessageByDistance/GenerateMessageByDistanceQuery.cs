using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using ShowAndTell.McConf.Application.Common.Interfaces;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.GenerateMessageByDistance
{
    public class GenerateMessageByDistanceQuery : IRequest<string>, IQuery
    {
        public decimal Distance { get; set; }

    }
}