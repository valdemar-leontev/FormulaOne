using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities;
using FormulaOne.Entities.Dtos.Requests;
using FormulaOne.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers;

public class AchievementController : BaseController
{
    public AchievementController(IUnitOfWork unitOfWork, IMapper mapper) 
        : base(unitOfWork, mapper) { }

    [HttpGet]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> GetDriverAchievements(Guid driverId)
    {
        var driverAchievements = await _unitOfWork.Achievements.GetDriverAchievementAsync(driverId);
        
        if (driverAchievements == null)
        {
            return NotFound("Achievements not found");
        }
        
        var result = _mapper.Map<DriverAchievementResponse>(driverAchievements);
        
        return Ok(result);
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddAchievement([FromBody] CreateDriverAchievementRequest achievement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var mappedAchievement = _mapper.Map<Achievement>(achievement);
        
        var result = await _unitOfWork.Achievements.Add(mappedAchievement);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetDriverAchievements), new { driverId = result.DriverId }, mappedAchievement);
    }
    
    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateAchievement([FromBody] CreateDriverAchievementRequest achievement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var mappedAchievement = _mapper.Map<Achievement>(achievement);
        
        var result = await _unitOfWork.Achievements.Update(mappedAchievement);
        await _unitOfWork.CompleteAsync();
        
        return NoContent();
    }
}