using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Repositories.Companies;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FrontEASE.Infrastructure.Repositories.Companies
{
    public class CompanyRepository(ApplicationDbContext context) : ICompanyRepository
    {
        public async Task<IList<Company>> LoadWhere(Expression<Func<Company, bool>> predicate, CancellationToken cancellationToken)
        {
            var companies = await context.Companies
                .Where(predicate)
                .ToListAsync(cancellationToken) ?? [];

            return companies;
        }

        public async Task<Company?> Load(Guid id, CancellationToken cancellationToken)
        {
            var company = await context.Companies
                .Include(x => x.Image)
                .Include(x => x.Users)
                .Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

            return company;
        }

        public async Task<IList<Company>> LoadAll(CancellationToken cancellationToken)
        {
            var companies = await context.Companies
                .AsNoTracking()
                .Include(x => x.Image)
                .Include(x => x.Users)
                .Include(x => x.Address)
                .Where(x => !x.IsDeleted)
                .ToListAsync(cancellationToken) ?? [];

            return companies;
        }

        public async Task<Company> Insert(Company company, CancellationToken cancellationToken)
        {
            await context.Companies.AddAsync(company, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return company;
        }

        public async Task<Company> Update(Company company, CancellationToken cancellationToken)
        {
            context.Companies.Update(company);
            await context.SaveChangesAsync(cancellationToken);
            return company;
        }

        public async Task Delete(Company company, CancellationToken cancellationToken)
        {
            company.IsDeleted = true;
            context.Companies.Update(company);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => await context.Database.BeginTransactionAsync(cancellationToken);
    }
}
