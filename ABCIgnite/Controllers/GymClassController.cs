using ABCIgnite.datab;
using ABCIgnite.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCIgnite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly AbcclientDatabaseContext _context;
        private readonly IMapper _mapper;

        public ClassController(AbcclientDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Method 1: GetAll method without AutoMapper (manual mapping)
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ClassDTO>>> GetAllClasses()
        {
            // Fetch data from the database
            var classes = await _context.Classes.ToListAsync();

            // Manually map the entity to DTO
            var classDTOs = classes.Select(c => new ClassDTO
            {
                ClassId = c.ClassId,
                Name = c.Name,
                StartDate = c.StartDate.ToDateTime(new TimeOnly()),  // Convert DateOnly to DateTime
                EndDate = c.EndDate.ToDateTime(new TimeOnly()),      // Convert DateOnly to DateTime
                StartTime = c.StartTime.ToTimeSpan(),                // Convert TimeOnly to TimeSpan
                Duration = c.Duration,
                Capacity = c.Capacity
            }).ToList();

            return Ok(classDTOs);
        }

        // Method 2: GetAll method using AutoMapper
        [HttpGet("GetAllUsingAutoMapper")]
        public async Task<ActionResult<IEnumerable<ClassDTO>>> GetAllClassesUsingAutoMapper()
        {
            // Fetch data from the database
            var classes = await _context.Classes.ToListAsync();

            // Map using AutoMapper
            var classDTOs = _mapper.Map<List<ClassDTO>>(classes);

            return Ok(classDTOs);
        }
    }
}