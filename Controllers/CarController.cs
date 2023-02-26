using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandCarWashSystem.Models;
using OnDemandCarWashSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICar _car;
        public CarController(ICar car)
        {
            _car = car;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _car.GetAllAsync();
            return Ok(cars);
        }
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetCarAsync")]
        public async Task<IActionResult> GetCarAsync(int id)
        {
            var cars = await _car.GetAsync(id);
            if (cars == null)
            {
                return NotFound();
            }
            return Ok(cars);
        }
        [HttpPost]
        
        
        public async Task<IActionResult> AddCarAsync(CarModel addcar)
        {
            var car = new Models.CarModel()
            {

                Model = addcar.Model,
                
                Status = addcar.Status
            };
            await _car.AddAsync(car);
            return Ok();
        }
        #region
        //delete method
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var car = await _car.DeleteAsync(id);
                if (car == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            { }
            return Ok();
        }
        #endregion
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCarAsync([FromRoute] int id, [FromBody] CarModel updatecar)
        {
            try
            {
                var car = new Models.CarModel()
                {
                    Model = updatecar.Model,
                   
                    Status = updatecar.Status
                };
                car = await _car.UpdateAsync(id, car);
                if (car == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            { }
            return Ok();
        }
    }
}
