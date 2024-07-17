using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.PhoneNumberDtos;
using Service.Dtos.PhoneNumberHeadlingDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    public class PhoneNumberHeadlingController : Controller
    {
        private readonly IPhoneNumberHeadlingService _phoneNumberHeadlingService;
        private readonly IMapper _mapper;
        public PhoneNumberHeadlingController(IPhoneNumberHeadlingService phoneNumberHeadlingService, IMapper mapper)
        {
            _phoneNumberHeadlingService = phoneNumberHeadlingService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostPhoneNumberHeadlingDto postPhoneNumberHeadling)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _phoneNumberHeadlingService.Create(_mapper.Map<PhoneNumberHeadling>(postPhoneNumberHeadling));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _phoneNumberHeadlingService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _phoneNumberHeadlingService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetPhoneNumberHeadlingDto>>(await _phoneNumberHeadlingService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            PhoneNumberHeadling phoneNumberHeadling = await _phoneNumberHeadlingService.GetEntity(e => e.Id == id);
            if (phoneNumberHeadling == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetPhoneNumberHeadlingDto>(phoneNumberHeadling));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutPhoneNumberHeadlingDto putPhoneNumberHeadling)
        {
            if (putPhoneNumberHeadling.Id == null) return BadRequest();
            PhoneNumberHeadling phoneNumberHeadling = await _phoneNumberHeadlingService.GetEntity(e => e.Id == putPhoneNumberHeadling.Id);
            if (phoneNumberHeadling == null)
            {
                return NotFound();
            }
            _mapper.Map(putPhoneNumberHeadling, phoneNumberHeadling);
            ResponseObj responseObj = await _phoneNumberHeadlingService.Update(phoneNumberHeadling);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

