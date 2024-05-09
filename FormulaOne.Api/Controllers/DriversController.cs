using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities;
using FormulaOne.Entities.Dtos.Requests;
using FormulaOne.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers;

public class DriversController : BaseController
{
    public DriversController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    [HttpGet]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> GetDriver(Guid driverId)
    {
        var driver = await _unitOfWork.Drivers.GetById(driverId);

        if (driver == null)
        {
            return NotFound();
        }

        var mappedDriver = _mapper.Map<GetDriverResponse>(driver);
        
        return Ok(mappedDriver);
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest driver)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var mappedDriver = _mapper.Map<Driver>(driver);
        
        await _unitOfWork.Drivers.Add(mappedDriver);
        await _unitOfWork.CompleteAsync();
        
        return CreatedAtAction(nameof(GetDriver), new { driverId = mappedDriver.Id }, mappedDriver);
    }
    
    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateDriver([FromBody] Driver driver)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var mappedDriver = _mapper.Map<Driver>(driver);
        
        await _unitOfWork.Drivers.Update(mappedDriver);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
    
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllDriver()
    {
        var drivers = await _unitOfWork.Drivers.All();
                
        return Ok(_mapper.Map<IEnumerable<GetDriverResponse>>(drivers));
    }
    
    [HttpDelete]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> DeleteDriver(Guid driverId)
    {
        var driver = await _unitOfWork.Drivers.GetById(driverId);

        if (driver == null)
        {
            return NotFound();
        }
        
        await  _unitOfWork.Drivers.Delete(driverId);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}