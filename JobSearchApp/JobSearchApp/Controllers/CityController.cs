using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.AdvertaismetDtos;
using Service.Dtos.CityDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostCityDto postCityDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _cityService.Create(_mapper.Map<City>(postCityDto));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _cityService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _cityService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetCityDto>>(await _cityService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            City city = await _cityService.GetEntity(e => e.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetCityDto>(city));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutCityDto putCityDto)
        {
            if (putCityDto.Id == null) return BadRequest();
            City city = await _cityService.GetEntity(e => e.Id == putCityDto.Id);
            if (city == null)
            {
                return NotFound();
            }
            _mapper.Map(putCityDto, city);
            ResponseObj responseObj = await _cityService.Update(city);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

