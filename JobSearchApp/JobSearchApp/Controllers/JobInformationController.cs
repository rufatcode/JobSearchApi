using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.EmploymentTypeDtos;
using Service.Dtos.JobInformationDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class JobInformationController : Controller
    {
        private readonly IJobInformationService _jobInformationService;
        private readonly IMapper _mapper;
        public JobInformationController(IJobInformationService jobInformationService, IMapper mapper)
        {
            _jobInformationService = jobInformationService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostJobInformationDto postJobInformationDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _jobInformationService.Create(_mapper.Map<JobInformation>(postJobInformationDto));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _jobInformationService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _jobInformationService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetJobInformationDto>>(await _jobInformationService.GetAll(null, "JobInformationType")));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            JobInformation jobInformation = await _jobInformationService.GetEntity(e => e.Id == id, "JobInformationType");
            if (jobInformation == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetJobInformationDto>(jobInformation));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutJobInformationDto putJobInformationDto)
        {
            if (putJobInformationDto.Id == null) return BadRequest();
            JobInformation jobInformation = await _jobInformationService.GetEntity(e => e.Id == putJobInformationDto.Id);
            if (jobInformation == null)
            {
                return NotFound();
            }
            _mapper.Map(putJobInformationDto, jobInformation);
            ResponseObj responseObj = await _jobInformationService.Update(jobInformation);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

