using System;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "1.0")]

namespace ShowAndTell.McConf.Application.Prompts.GenerateMessage
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GenerateMessageQueryValidator : AbstractValidator<GenerateMessageQuery>
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore, Signature = Mode.Merge)]
        public GenerateMessageQueryValidator()
        {
            ConfigureValidationRules();
        }

        [IntentManaged(Mode.Fully)]
        private void ConfigureValidationRules()
        {
            RuleFor(v => v.PrompText)
                .NotNull();

        }
    }
}