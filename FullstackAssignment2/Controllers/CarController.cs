using FullstackAssignment2.DTOs;
using FullstackAssignment2.Services;
using Microsoft.AspNetCore.Mvc;
namespace FullstackAssignment2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService; 
        public CarController(CarService todoService) 
        {
            _carService = todoService;
        }

        [HttpGet] 
        public async Task<ActionResult<List<ResponseCarDTO>>> GetAll()
        {
            var car = await _carService.GetAll();
            return Ok(car);
        }

        [HttpGet("{id:int}")] 
        public async Task<ActionResult<ResponseCarDTO>> GetById(int id)
        {
            return Ok(await _carService.GetById(id));
        }

        [HttpPost] 
        public async Task<ActionResult<ResponseCarDTO>> Create([FromBody] CreateCarDTO ccdto)
        {
            var createdCar = await _carService.Create(ccdto);
            return CreatedAtAction(nameof(GetById), new { id = createdCar.Id }, createdCar);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseCarDTO>> Update(int id,[FromBody] UpdateCarDTO ucdto)
        {
            return Ok(await _carService.Update(id, ucdto));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _carService.Delete(id);
            return NoContent();
        }
    }
}

