using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.CompanyContactDtos;
using Service.Dtos.CompanyDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostCompanyDto postCompanyDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _companyService.Create(_mapper.Map<Company>(postCompanyDto));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _companyService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _companyService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetCompanyDto>>(await _companyService.GetAll(null,"CompanyContacts.PhoneNumber.PhoneNumberHeadling")));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            Company company = await _companyService.GetEntity(e => e.Id == id, "CompanyContacts.PhoneNumber.PhoneNumberHeadling");
            if (company == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetCompanyDto>(company));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutCompanyDto putCompanyDto)
        {
            if (putCompanyDto.Id == null) return BadRequest();
            Company company = await _companyService.GetEntity(e => e.Id == putCompanyDto.Id);
            if (company == null)
            {
                return NotFound();
            }
            _mapper.Map(putCompanyDto, company);
            ResponseObj responseObj = await _companyService.Update(company);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

