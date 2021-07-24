using AutoMapper;
using MediatR;
using MockQueryable.Moq;
using Moq;
using ReadLater5.Application.Inputs.Commands.CategoryCommands;
using ReadLater5.Application.Inputs.Queries.CategoryQueries;
using ReadLater5.Application.Interfaces;
using ReadLater5.Application.Mappings;
using ReadLater5.Application.Services.CategoryService;
using ReadLater5.Domain.Dtos;
using ReadLater5.Domain.Models;
using ReadLater5.Domain.ViewModels;
using ReadLater5.UnitTest;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ReadLater5.UnitTests.Tests
{
    public class CategoryTests
    {
        private readonly IMapper _mapper;
        public CategoryTests()
        {
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task GetCategories_CategoriesFound_ReturnsCategoryEnvelope()
        {
            //Arrange.
            var contextMock = new Mock<IDataContext>();
            var categories = new List<Category> { new Category { Id = 1 } };

            var mockDbSet = categories.AsQueryable().BuildMockDbSet();

            contextMock.Setup(x => x.Categories).Returns(mockDbSet.Object);

            var queryHandler = new GetCategories.Handler(contextMock.Object, _mapper);

            //Act.
            var result = await queryHandler.Handle(
                new CategoriesQuery { Columns = new List<ColumnDto>() }, new CancellationToken());

            //Assert.
            Assert.True(result.Data.Any());
        }

        [Fact]
        public async Task CreateCategory_ReturnsUnit()
        {
            //Arrange.
            var contextMock = new Mock<IDataContext>();
            var categories = new List<Category> 
            { 
                new Category { Id = 1, Name = "Category 1" } 
            };
            var category = new CategoryVM { Id = 2, Name = "Category 2" };

            var mockDbSet = categories.AsQueryable().BuildMockDbSet();
            mockDbSet.AddMockSetup(categories);

            contextMock.Setup(x => x.Categories).Returns(mockDbSet.Object);
            contextMock.Setup(x => x.SaveChangesAsync(new CancellationToken())).ReturnsAsync(1);

            var queryHandler = new CreateCategory.Handler(contextMock.Object, _mapper);

            //Act.
            var result = await queryHandler.Handle(new CategoryCreateCommand { Category = category }, new CancellationToken());

            //Assert.
            Assert.Equal(Unit.Value, result);
        }
    }
}
