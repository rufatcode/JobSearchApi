using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.CompanyDtos;
using Service.Dtos.EmploymentTypeDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class EmploymentTypeController : Controller
    {
        private readonly IEmploymentTypeService _employmentTypeService;
        private readonly IMapper _mapper;
        public EmploymentTypeController(IEmploymentTypeService employmentTypeService, IMapper mapper)
        {
            _employmentTypeService = employmentTypeService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostEmploymentTypeDto postEmploymentTypeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _employmentTypeService.Create(_mapper.Map<EmploymentType>(postEmploymentTypeDto));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _employmentTypeService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _employmentTypeService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetEmploymentTypeDto>>(await _employmentTypeService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            EmploymentType employment = await _employmentTypeService.GetEntity(e => e.Id == id);
            if (employment == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetEmploymentTypeDto>(employment));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutEmploymentTypeDto putEmploymentTypeDto)
        {
            if (putEmploymentTypeDto.Id == null) return BadRequest();
            EmploymentType employmentType = await _employmentTypeService.GetEntity(e => e.Id == putEmploymentTypeDto.Id);
            if (employmentType == null)
            {
                return NotFound();
            }
            _mapper.Map(putEmploymentTypeDto, employmentType);
            ResponseObj responseObj = await _employmentTypeService.Update(employmentType);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

