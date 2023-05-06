using System;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.GenerateMessageByDistance
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GenerateMessageByDistanceQueryValidator : AbstractValidator<GenerateMessageByDistanceQuery>
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore, Signature = Mode.Merge)]
        public GenerateMessageByDistanceQueryValidator()
        {
            ConfigureValidationRules();
        }

        [IntentManaged(Mode.Fully)]
        private void ConfigureValidationRules()
        {
            RuleFor(v => v.ApiKey)
                .NotNull();

        }
    }
}