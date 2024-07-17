using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.AdvertaismetDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class AdvertaismetController : Controller
    {
        private readonly IAdvertaismetService _advertaismetService;
        private readonly IMapper _mapper;
        public AdvertaismetController(IAdvertaismetService advertaismetService,IMapper mapper)
        {
            _advertaismetService = advertaismetService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostAdvertaismetDto postAdvertaismetDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _advertaismetService.Create(_mapper.Map<Advertaismet>(postAdvertaismetDto));
            if (responseObj.StatusCode != StatusCodes.Status200OK&&responseObj.StatusCode!=StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _advertaismetService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _advertaismetService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetAdvertaismetDto>>(await _advertaismetService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            Advertaismet advertaismet = await _advertaismetService.GetEntity(e => e.Id == id);
            if (advertaismet == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetAdvertaismetDto>(advertaismet));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery]PutAdvertaismetDto putAdvertaismetDto)
        {
            if (putAdvertaismetDto.Id == null) return BadRequest();
            Advertaismet advertaismet = await _advertaismetService.GetEntity(e => e.Id == putAdvertaismetDto.Id);
            if (advertaismet == null)
            {
                return NotFound();
            }
            _mapper.Map(putAdvertaismetDto, advertaismet);
            ResponseObj responseObj = await _advertaismetService.Update(advertaismet);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

