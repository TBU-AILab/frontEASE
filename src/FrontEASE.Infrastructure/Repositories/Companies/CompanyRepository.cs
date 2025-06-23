using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Repositories.Companies;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FrontEASE.Infrastructure.Repositories.Companies
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Company>> LoadWhere(Expression<Func<Company, bool>> predicate, CancellationToken cancellationToken)
        {
            var companies = await _context.Companies
                .Where(predicate)
                .ToListAsync(cancellationToken) ?? [];

            return companies;
        }

        public async Task<Company?> Load(Guid id, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .Include(x => x.Image)
                .Include(x => x.Users)
                .Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

            return company;
        }

        public async Task<IList<Company>> LoadAll(CancellationToken cancellationToken)
        {
            var companies = await _context.Companies
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
            await _context.Companies.AddAsync(company, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return company;
        }

        public async Task<Company> Update(Company company, CancellationToken cancellationToken)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync(cancellationToken);
            return company;
        }

        public async Task Delete(Company company, CancellationToken cancellationToken)
        {
            company.IsDeleted = true;
            _context.Companies.Update(company);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => await _context.Database.BeginTransactionAsync(cancellationToken);
    }
}
