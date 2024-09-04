using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            try
            {
                var items = await _employeeService.GetAllEmployeesAsync();
                var result = _mapper.Map<IEnumerable<EmployeeDto>>(items);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred - GetAll request");
                return InternalServerError(ex);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{employeeCode}")]
        public async Task<IHttpActionResult> GetAsync(string employeeCode)
        {
            try
            {
                var item = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
                if (item == null) return NotFound();

                var result = _mapper.Map<EmployeeDto>(item);
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
        public async Task<IHttpActionResult> PostAsync([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                if (employeeDto == null) return BadRequest("Employee data is null");

                var employee = _mapper.Map<EmployeeInfo>(employeeDto);
                var result = await _employeeService.SaveEmployeeAsync(employee);
                if (!result) return BadRequest("Could not save employee data");

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred - Post request");
                return InternalServerError(ex);
            }
        }
    }
}