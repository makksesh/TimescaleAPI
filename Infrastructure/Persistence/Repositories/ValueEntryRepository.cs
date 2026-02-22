using Domain.Entities;
using Domain.Interfaceses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;


public class ValueEntryRepository : IValueEntryRepository
{
    private readonly AppDbContext _context;

    public ValueEntryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeleteByFileNameAsync(string fileName, CancellationToken ct = default)
    {
        await _context.Values
            .Where(v => v.FileName == fileName)
            .ExecuteDeleteAsync(ct);
    }

    public async Task AddRangeAsync(IEnumerable<ValueEntry> entries,
        CancellationToken ct = default)
    {
        await _context.Values.AddRangeAsync(entries, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<ValueEntry>> GetLast10ByFileNameAsync(
        string fileName, CancellationToken ct = default)
    {
        return await _context.Values
            .Where(v => v.FileName == fileName)
            .OrderByDescending(v => v.Date)
            .Take(10)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}