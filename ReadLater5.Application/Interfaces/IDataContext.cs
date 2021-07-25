using Microsoft.EntityFrameworkCore;
using ReadLater5.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ReadLater5.Application.Interfaces
{
    public interface IDataContext
    {
        DbSet<Bookmark> Bookmarks { get; set; }
        DbSet<Category> Categories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
