using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.AdvertaismetDtos;
using Service.Dtos.CategoryDtos;
using Service.Helpers;
using Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostCategoryDto postCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _categoryService.Create(_mapper.Map<Category>(postCategoryDto));
            if (responseObj.StatusCode != StatusCodes.Status200OK && responseObj.StatusCode != StatusCodes.Status201Created) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("Deactive/{id}")]
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _categoryService.Delete((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SupperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _categoryService.DeleteFromDB((int)id);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<GetCategoryDto>>(await _categoryService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            Category category = await _categoryService.GetEntity(e => e.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetCategoryDto>(category));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] PutCategoryDto putCategoryDto)
        {
            if (putCategoryDto.Id == null) return BadRequest();
            Category category = await _categoryService.GetEntity(e => e.Id == putCategoryDto.Id);
            if (category == null)
            {
                return NotFound();
            }
            _mapper.Map(putCategoryDto, category);
            ResponseObj responseObj = await _categoryService.Update(category);
            if (responseObj.StatusCode != StatusCodes.Status200OK) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

