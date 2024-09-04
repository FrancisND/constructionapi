using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbWrapper<Company> _companyDbWrapper;

        public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
        {
            _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> GetByCodeAsync(string companyCode)
        {
            return (await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode)))?.FirstOrDefault();
        }

        public async Task<bool> UpdateCompanyAsync(string companyCode, Company company)
        {
            var itemRepo = (await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode)))?.FirstOrDefault();
            if (itemRepo != null)
            {
                company.CompanyCode = companyCode;
                return await _companyDbWrapper.UpdateAsync(company);
            }
            return false;
        }

        public async Task<bool> DeleteCompanyAsync(string companyCode)
        {
            return await _companyDbWrapper.DeleteAsync(t => t.CompanyCode.Equals(companyCode));
        }

        public async Task<bool> SaveCompanyAsync(Company company)
        {
            var itemRepo = (await _companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode)))?.FirstOrDefault();

            if (itemRepo == null)
            {
                return await _companyDbWrapper.InsertAsync(company);
            }
            return await _companyDbWrapper.UpdateAsync(company);
        }
    }
}
