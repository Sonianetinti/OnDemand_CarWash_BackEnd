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
    public class PackageController : ControllerBase
    {
        private readonly IPackage _package;
        public PackageController(IPackage package)
        {
            _package = package;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var packages = await _package.GetAllAsync();
            return Ok(packages);
        }
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetpackageAsync")]
        public async Task<IActionResult> GetpackageAsync(int id)
        {
            var package = await _package.GetAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }
        [HttpPost]
        //[Route("register")]
        public async Task<IActionResult> AddpackageAsync(CarPackageModel addpackage)
        {
            var package = new Models.CarPackageModel()
            {
                
                Name = addpackage.Name,
                Price = addpackage.Price,
                Status = addpackage.Status
            };
            await _package.AddAsync(package);
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
                var package = await _package.DeleteAsync(id);
                if (package == null)
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
        public async Task<IActionResult> UpdatepackageAsync([FromRoute] int id, [FromBody] CarPackageModel updatepackage)
        {
            try
            {
                var package = new Models.CarPackageModel()
                {
                    Name = updatepackage.Name,
                    Price = updatepackage.Price,
                    Status = updatepackage.Status
                };
                package = await _package.UpdateAsync(id,package);
                if (package == null)
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
