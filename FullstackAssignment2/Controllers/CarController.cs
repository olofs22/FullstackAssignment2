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
            var car = await _carService.GetById(id);
            return Ok(car);
        }

        [HttpPost] 
        public async Task<ActionResult<ResponseCarDTO>> Create(CreateCarDTO ccdto)
        {
            var createdCar = await _carService.Create(ccdto);
            return CreatedAtAction(nameof(GetById), new { id = createdCar.Id }, createdCar);
        }

        [HttpPut("{id:int}")] 
        public async Task<ActionResult<ResponseCarDTO>> Update(int id, UpdateCarDTO ucdto)
        {
            var updatedCar = await _carService.Update(id, ucdto);

            if (updatedCar == null) return NotFound();

            return Ok(updatedCar);
        }

        [HttpDelete("{id:int}")] 
        public async Task<ActionResult> Delete(int id)
        {
            if (!await _carService.Delete(id))
                return NotFound();
            return NoContent();
        }
    }
}

