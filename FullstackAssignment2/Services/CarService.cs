using FullstackAssignment2.Data;
using FullstackAssignment2.DTOs;
using FullstackAssignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackAssignment2.Services
{
    public class CarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ResponseCarDTO>> GetAll()
        {
            var car = await _context.Car.ToListAsync();
            return car.Select(t => new ResponseCarDTO
            {
                Id = t.Id,
                Make = t.Make,
                Model = t.Model,
                Year = t.Year,
            }).ToList();
        }
        public async Task<ResponseCarDTO> GetById(int id) 
        {
            var car = await _context.Car
                .FirstOrDefaultAsync(t => t.Id == id);

            if (car == null) throw new KeyNotFoundException($"Car with Id:{id} was not found");

            return new ResponseCarDTO 
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
            };
        }
        public async Task<ResponseCarDTO> Create(CreateCarDTO ccdto) 
        {
            var car = new Car
            {
                Make = ccdto.Make,
                Model = ccdto.Model,
                Year = ccdto.Year,
            };

            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return await GetById(car.Id);
        }
        public async Task<ResponseCarDTO> Update(int id, UpdateCarDTO ucdto) 
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null) return null;

            car.Make = ucdto.Make;
            car.Model = ucdto.Model;
            car.Year = ucdto.Year;

            await _context.SaveChangesAsync();

            return await GetById(car.Id);
        }
        public async Task<bool> Delete(int id) 
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
                return false;

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

