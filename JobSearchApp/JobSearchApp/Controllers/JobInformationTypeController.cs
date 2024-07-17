using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.JobInformationDtos;
using Service.Dtos.JobInformationTypeDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class JobInformationTypeController : Controller
    {
        private readonly IJobInformationTypeService _jobInformationTypeService;
        private readonly IMapper _mapper;
        public JobInformationTypeController(IJobInformationTypeService jobInformationTypeService, IMapper mapper)
        {
            _jobInformationTypeService = jobInformationTypeService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostJobInformationTypeDto postJobInformationTypeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _jobInformationTypeService.Create(_mapper.Map<JobInformationType>(postJobInformationTypeDto));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _jobInformationTypeService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _jobInformationTypeService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetJobInformationTypeDto>>(await _jobInformationTypeService.GetAll(null, "JobInformations")));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            JobInformationType jobInformationType = await _jobInformationTypeService.GetEntity(e => e.Id == id, "JobInformations");
            if (jobInformationType == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetJobInformationTypeDto>(jobInformationType));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutJobInformationTypeDto putJobInformationTypeDto)
        {
            if (putJobInformationTypeDto.Id == null) return BadRequest();
            JobInformationType jobInformationType = await _jobInformationTypeService.GetEntity(e => e.Id == putJobInformationTypeDto.Id);
            if (jobInformationType == null)
            {
                return NotFound();
            }
            _mapper.Map(putJobInformationTypeDto, jobInformationType);
            ResponseObj responseObj = await _jobInformationTypeService.Update(jobInformationType);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

