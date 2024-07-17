using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.PhoneNumberHeadlingDtos;
using Service.Dtos.PositionDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;
        public PositionController(IPositionService positionService, IMapper mapper)
        {
            _positionService = positionService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostPositionDto postPositionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _positionService.Create(_mapper.Map<Position>(postPositionDto));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _positionService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _positionService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetPositionDto>>(await _positionService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            Position position = await _positionService.GetEntity(e => e.Id == id);
            if (position == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetPositionDto>(position));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutPositionDto putPosition)
        {
            if (putPosition.Id == null) return BadRequest();
            Position position = await _positionService.GetEntity(e => e.Id == putPosition.Id);
            if (position == null)
            {
                return NotFound();
            }
            _mapper.Map(putPosition, position);
            ResponseObj responseObj = await _positionService.Update(position);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

