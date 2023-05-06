using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using ShowAndTell.McConf.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace ShowAndTell.McConf.Domain.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface IPromptRepository : IRepository<Prompt, Prompt>
    {

        [IntentManaged(Mode.Fully)]
        Task<Prompt> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<List<Prompt>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);
    }
}