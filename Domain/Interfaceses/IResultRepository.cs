using Domain.Entities;
using Domain.Filters;

namespace Domain.Interfaceses;

public interface IResultRepository
{
    Task UpsertAsync(Result result, CancellationToken ct = default);
    Task<IEnumerable<Result>> GetFilteredAsync(ResultFilter filter, CancellationToken ct = default);
}