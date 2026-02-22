using Domain.Entities;

namespace Domain.Interfaceses;

public interface IValueEntryRepository
{
    Task DeleteByFileNameAsync(string fileName, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<ValueEntry> entries, CancellationToken ct = default);
    Task<IEnumerable<ValueEntry>> GetLast10ByFileNameAsync(string fileName, CancellationToken ct = default);
}