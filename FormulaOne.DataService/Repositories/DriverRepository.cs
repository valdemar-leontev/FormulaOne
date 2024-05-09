using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories;

public class DriverRepository : GenericRepository<Driver>, IDriverRepository
{
    public DriverRepository(AppDbContext context, ILogger logger) : base(context, logger) { }

    public override async Task<IEnumerable<Driver>> All()
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
            _logger.LogError(e, "{Repo} All function error", typeof(DriverRepository));
            throw;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(driver => driver.Id == id);

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
            _logger.LogError(e, "{Repo} Delete function error", typeof(DriverRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(Driver entity)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(driver => driver.Id == entity.Id);

            if (result == null)
            {
                return false;
            }

            result.FirstName = entity.FirstName;
            result.Number = entity.Number;
            result.UpdatedDate = DateTime.Now;
            result.LastName = entity.LastName;
            result.BirthDay = entity.BirthDay;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Update function error", typeof(DriverRepository));
            throw;
        }
    }
}