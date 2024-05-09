using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories;

public class AchievementRepository : GenericRepository<Achievement>, IAchievementRepository
{
    public AchievementRepository(AppDbContext context, ILogger logger) : base(context, logger) { }

    public async Task<Achievement?> GetDriverAchievementAsync(Guid driverId)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(achievement => achievement.DriverId == driverId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetDriverAchievementAsync function error", typeof(AchievementRepository));
            throw;
        }
    }
    
    public override async Task<IEnumerable<Achievement>> All()
    {
        try
        {
            return await _dbSet.Where(x => x.Status == 1)
                .AsNoTracking()
                .AsSplitQuery() // TODO: delete maybe?
                .OrderBy(x => x.AddedDate)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} All function error", typeof(AchievementRepository));
            throw;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(achievement => achievement.Id == id);

            if (result == null)
            {
                return false;
            }

            result.Status = 0;
            result.UpdatedDate = DateTime.Now;
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Delete function error", typeof(AchievementRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(Achievement entity)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(achievement => achievement.Id == entity.Id);

            if (result == null)
            {
                return false;
            }

            result.UpdatedDate = DateTime.Now;
            result.FastestLap  = entity.FastestLap;
            result.RaceWins = entity.RaceWins;
            result.PolePosition = entity.PolePosition;
            result.WorldChampionship = entity.WorldChampionship;
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Update function error", typeof(AchievementRepository));
            throw;
        }
    }
}