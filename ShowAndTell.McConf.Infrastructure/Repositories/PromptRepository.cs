using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using ShowAndTell.McConf.Domain.Entities;
using ShowAndTell.McConf.Domain.Repositories;
using ShowAndTell.McConf.Infrastructure.Persistence;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace ShowAndTell.McConf.Infrastructure.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class PromptRepository : RepositoryBase<Prompt, Prompt, ApplicationDbContext>, IPromptRepository
    {
        public PromptRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Prompt> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<Prompt>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
        {
            return await FindAllAsync(x => ids.Contains(x.Id), cancellationToken);
        }
    }
}