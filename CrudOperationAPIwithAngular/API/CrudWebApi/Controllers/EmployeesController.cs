using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudWebApi.Model;
using CrudWebApi.Model.Data;
using AutoMapper;
using CrudWebApi.Dto;
using System.Net;

namespace CrudWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        // GET: api/<PrescrptionController>
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeReadDto>> Get()
        {
            var employees = _employeeRepository.GetAll();

            var employeeDto = _mapper.Map<IEnumerable<EmployeeReadDto>>(employees);

            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employeeDto);

        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        public ActionResult<EmployeeReadDto> GetEmployeeById(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee == null)
            {
                return NotFound(id);
            }
            return (_mapper.Map<EmployeeReadDto>(employee));
        }

        [HttpPost]
        public ActionResult<EmployeeReadDto> CreateEmployee(
            EmployeeCreateDto employeeCreateDto)
        {

            //Mapping to Persist to Data
            var employeeModel = _mapper.Map<TblEmployee>(employeeCreateDto);

            _employeeRepository.Create(employeeModel);
            _employeeRepository.SaveChanges();

            //Mapp from Prescription to its Dtod
            var EmployeeReadDto = _mapper.Map<EmployeeReadDto>(employeeModel);

            return CreatedAtRoute(nameof(GetEmployeeById),
                new { Id = EmployeeReadDto.Id }, EmployeeReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblEmployee(int id, TblEmployee tblEmployee)
        {
            if (id != tblEmployee.Id)
            {
                return BadRequest();
            }

            _employeeRepository.Update(id, tblEmployee);
            
            try
            {
                _employeeRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_employeeRepository.TblEmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            _employeeRepository.Delete(id);

            try
            {
                _employeeRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_employeeRepository.TblEmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }
    }
}
