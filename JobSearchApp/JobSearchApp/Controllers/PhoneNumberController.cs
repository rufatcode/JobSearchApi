using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.JobInformationTypeDtos;
using Service.Dtos.PhoneNumberDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class PhoneNumberController : Controller
    {
        private readonly IPhoneNumberService _phoneNumberService;
        private readonly IMapper _mapper;
        public PhoneNumberController(IPhoneNumberService phoneNumberService, IMapper mapper)
        {
            _phoneNumberService = phoneNumberService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostPhoneNumberDto postPhoneNumber)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _phoneNumberService.Create(_mapper.Map<PhoneNumber>(postPhoneNumber));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _phoneNumberService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _phoneNumberService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetPhoneNumberDto>>(await _phoneNumberService.GetAll(null, "PhoneNumberHeadling")));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            PhoneNumber phoneNumber = await _phoneNumberService.GetEntity(e => e.Id == id, "PhoneNumberHeadling");
            if (phoneNumber == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetPhoneNumberDto>(phoneNumber));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutPhoneNumberDto putPhoneNumberDto)
        {
            if (putPhoneNumberDto.Id == null) return BadRequest();
            PhoneNumber phoneNumber = await _phoneNumberService.GetEntity(e => e.Id == putPhoneNumberDto.Id);
            if (phoneNumber == null)
            {
                return NotFound();
            }
            _mapper.Map(putPhoneNumberDto, phoneNumber);
            ResponseObj responseObj = await _phoneNumberService.Update(phoneNumber);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

