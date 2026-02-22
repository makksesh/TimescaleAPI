using Domain.Entities;
using Domain.Filters;
using Domain.Interfaceses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ResultRepository : IResultRepository
{
    private readonly AppDbContext _context;

    public ResultRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task UpsertAsync(Result result, CancellationToken ct = default)
    {
        var existing = await _context.Results
            .FirstOrDefaultAsync(r => r.FileName == result.FileName, ct);

        if (existing is null)
            await _context.Results.AddAsync(result, ct);
        else
            _context.Entry(existing).CurrentValues.SetValues(result);

        await _context.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Result>> GetFilteredAsync(
        ResultFilter filter, CancellationToken ct = default)
    {
        var query = _context.Results.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.FileName))
            query = query.Where(r => r.FileName == filter.FileName);

        if (filter.FirstOperationStartFrom.HasValue)
            query = query.Where(r => r.FirstOperationStart >= filter.FirstOperationStartFrom);

        if (filter.FirstOperationStartTo.HasValue)
            query = query.Where(r => r.FirstOperationStart <= filter.FirstOperationStartTo);

        if (filter.AvgValueFrom.HasValue)
            query = query.Where(r => r.AvgValue >= filter.AvgValueFrom);

        if (filter.AvgValueTo.HasValue)
            query = query.Where(r => r.AvgValue <= filter.AvgValueTo);

        if (filter.AvgExecutionTimeFrom.HasValue)
            query = query.Where(r => r.AvgExecutionTime >= filter.AvgExecutionTimeFrom);

        if (filter.AvgExecutionTimeTo.HasValue)
            query = query.Where(r => r.AvgExecutionTime <= filter.AvgExecutionTimeTo);

        return await query.ToListAsync(ct);
    }
}