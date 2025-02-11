using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<Sale?> GetByIdAsync(Guid saleId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid saleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// For listing with pagination, etc.
        /// </summary>
        IQueryable<Sale> Query();
    }
}
