using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.CityDtos;
using Service.Dtos.CompanyContactDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class CompanyContactController : Controller
    {
        private readonly ICompanyContactService _companyContactService;
        private readonly IMapper _mapper;
        public CompanyContactController(ICompanyContactService companyContactService, IMapper mapper)
        {
            _companyContactService = companyContactService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostCompanyContactDto postCompanyContactDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _companyContactService.Create(_mapper.Map<CompanyContact>(postCompanyContactDto));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _companyContactService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _companyContactService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetCompanyContactDto>>(await _companyContactService.GetAll(null, "Company", "PhoneNumber.PhoneNumberHeadling")));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            CompanyContact companyContact = await _companyContactService.GetEntity(e => e.Id == id, "Company", "PhoneNumber.PhoneNumberHeadling");
            if (companyContact == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetCompanyContactDto>(companyContact));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutCompanyContactDto putCompanyContactDto)
        {
            if (putCompanyContactDto.Id == null) return BadRequest();
            CompanyContact companyContact = await _companyContactService.GetEntity(e => e.Id == putCompanyContactDto.Id);
            if (companyContact == null)
            {
                return NotFound();
            }
            _mapper.Map(putCompanyContactDto, companyContact);
            ResponseObj responseObj = await _companyContactService.Update(companyContact);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

