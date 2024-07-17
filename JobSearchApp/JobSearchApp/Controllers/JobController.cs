using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.EmploymentTypeDtos;
using Service.Dtos.JobDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostJobDto postJobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _jobService.Create(_mapper.Map<Job>(postJobDto));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _jobService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _jobService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetJobDto>>(await _jobService.GetAll(null, "EmploymentType", "City", "Position", "Advertaismet", "Category", "Company.CompanyContacts.PhoneNumber.PhoneNumberHeadling", "JobInformations.JobInformationType")));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            Job job = await _jobService.GetEntity(e => e.Id == id, "EmploymentType", "City", "Position", "Advertaismet", "Category", "Company.CompanyContacts.PhoneNumber.PhoneNumberHeadling", "JobInformations.JobInformationType");
            if (job == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetJobDto>(job));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutJobDto putJobDto)
        {
            if (putJobDto.Id == null) return BadRequest();
            Job job = await _jobService.GetEntity(e => e.Id == putJobDto.Id);
            if (job == null)
            {
                return NotFound();
            }
            _mapper.Map(putJobDto, job);
            ResponseObj responseObj = await _jobService.Update(job);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

