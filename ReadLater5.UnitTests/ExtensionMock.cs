using Microsoft.EntityFrameworkCore;
using Moq;
using ReadLater5.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReadLater5.UnitTest
{
    public static class ExtensionMock
    {
        public static void FindAsyncMockSetup<T>(this Mock<DbSet<T>> mock, IEnumerable<T> data) where T : BaseModel => 
            mock.Setup(set => set.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((object[] input) => data.SingleOrDefault(x => x.Id == (int)input.First()));

        public static void AddMockSetup<T>(this Mock<DbSet<T>> mock, List<T> data) where T : class => 
            mock.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => data.Add(s));

        public static void RemoveMockSetup<T>(this Mock<DbSet<T>> mock, List<T> data) where T : class =>
            mock.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>((s) => data.Remove(s));
    }
}
