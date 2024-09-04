using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;
using NLog;
using System.Web.Http.Description;

namespace WebApi.Controllers
{
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            try
            {
                var items = await _companyService.GetAllCompaniesAsync();
                var result = _mapper.Map<IEnumerable<CompanyDto>>(items);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred - GetAll request");
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync(string companyCode)
        {
            try
            {
                var item = await _companyService.GetCompanyByCodeAsync(companyCode);
                if (item == null) return NotFound();

                var result = _mapper.Map<CompanyDto>(item);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred - Get request");
                return InternalServerError(ex);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync([FromBody] CompanyDto companyDto)
        {
            try
            {
                if (companyDto == null) return BadRequest("Company data is null");

                var company = _mapper.Map<CompanyInfo>(companyDto);
                var result = await _companyService.SaveCompanyAsync(company);
                if (!result) return BadRequest("Could not save company data");

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred - Post request");
                return InternalServerError(ex);
            }
        }



        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> PutAsync(string companyCode, [FromBody] CompanyDto companyDto)
        {
            try
            {
                if (companyDto == null) return BadRequest("Company data is null");

                var company = _mapper.Map<CompanyInfo>(companyDto);
                var result = await _companyService.UpdateCompanyAsync(companyCode, company);
                if (!result) return BadRequest("Could not update company data");

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred - Put request");
                return InternalServerError(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(string companyCode)
        {
            try
            {
                var result = await _companyService.DeleteCompanyAsync(companyCode);
                if (!result) return BadRequest("Could not delete company");

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred - Delete request");
                return InternalServerError(ex);
            }
        }
    }
}