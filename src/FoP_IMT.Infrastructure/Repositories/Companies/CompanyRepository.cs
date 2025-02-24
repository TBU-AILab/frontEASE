using FoP_IMT.Domain.Entities.Companies;
using FoP_IMT.Domain.Repositories.Companies;
using FoP_IMT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FoP_IMT.Infrastructure.Repositories.Companies
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Company>> LoadWhere(Expression<Func<Company, bool>> predicate)
        {
            var companies = await _context.Companies
                .Where(predicate)
                .ToListAsync() ?? [];

            return companies;
        }

        public async Task<Company?> Load(Guid id)
        {
            var company = await _context.Companies
                .Include(x => x.Image)
                .Include(x => x.Users)
                .Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.ID == id);

            return company;
        }

        public async Task<IList<Company>> LoadAll()
        {
            var companies = await _context.Companies
                .AsNoTracking()
                .Include(x => x.Image)
                .Include(x => x.Users)
                .Include(x => x.Address)
                .Where(x => !x.IsDeleted)
                .ToListAsync() ?? [];

            return companies;
        }

        public async Task<Company> Insert(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            return company;
        }

        public async Task<Company> Update(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task Delete(Company company)
        {
            company.IsDeleted = true;
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.BeginTransactionAsync(cancellationToken);
    }
}
