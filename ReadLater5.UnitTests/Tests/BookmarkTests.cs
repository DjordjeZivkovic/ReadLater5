using AutoMapper;
using MediatR;
using MockQueryable.Moq;
using Moq;
using ReadLater5.Application.Inputs.Commands.BookmarkCommands;
using ReadLater5.Application.Inputs.Queries.BookmarkQueries;
using ReadLater5.Application.Interfaces;
using ReadLater5.Application.Mappings;
using ReadLater5.Application.Services.BookmarkService;
using ReadLater5.Domain.Constants;
using ReadLater5.Domain.Dtos;
using ReadLater5.Domain.Models;
using ReadLater5.Domain.ViewModels;
using ReadLater5.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ReadLater5.UnitTests.Tests
{
    public class BookmarkTests
    {
        private readonly IMapper _mapper;
        public BookmarkTests()
        {
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task GetBookmarkById_BookmarkFound_ReturnsBookmarkVM()
        {
            //Arrange.
            var contextMock = new Mock<IDataContext>();
            var bookmarks = new List<Bookmark> 
            { 
                new Bookmark { Id = 1, URL = "www.google.com", ShortDescription = "Test 1" } 
            };

            var mockDbSet = bookmarks.AsQueryable().BuildMockDbSet();
            mockDbSet.FindAsyncMockSetup(bookmarks);

            contextMock.Setup(x => x.Bookmarks).Returns(mockDbSet.Object);

            var queryHandler = new GetBookmark.Handler(contextMock.Object, _mapper);

            //Act.
            var result = await queryHandler.Handle(new BookmarkQuery { Id = 1 }, new CancellationToken());

            //Assert.
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBookmarkById_ThrowsException_NotFound()
        {
            //Arrange.
            var contextMock = new Mock<IDataContext>();
            var bookmarks = new List<Bookmark> 
            { 
                new Bookmark { Id = 1, URL = "www.google.com", ShortDescription = "Test 1" } 
            };

            var mockDbSet = bookmarks.AsQueryable().BuildMockDbSet();
            mockDbSet.FindAsyncMockSetup(bookmarks);

            contextMock.Setup(x => x.Bookmarks).Returns(mockDbSet.Object);

            var queryHandler = new GetBookmark.Handler(contextMock.Object, _mapper);
            //Act.
            var result = await Assert.ThrowsAsync<Exception>(() =>
                queryHandler.Handle(new BookmarkQuery { Id = 2 }, new CancellationToken()));

            //Assert.
            Assert.Equal(Errors.NotFound, result.Message);
        }

        [Fact]
        public async Task GetBookmarks_BookmarksFound_ReturnsBookmarkEnvelope()
        {
            //Arrange.
            var contextMock = new Mock<IDataContext>();
            var bookmarks = new List<Bookmark> { new Bookmark { Id = 1 } };

            var mockDbSet = bookmarks.AsQueryable().BuildMockDbSet();

            contextMock.Setup(x => x.Bookmarks).Returns(mockDbSet.Object);

            var queryHandler = new GetBookmarks.Handler(contextMock.Object, _mapper);

            //Act.
            var result = await queryHandler.Handle(
                new BookmarksQuery { Columns = new List<ColumnDto>() }, new CancellationToken());

            //Assert.
            Assert.True(result.Data.Any());
        }

        [Fact]
        public async Task CreateBookmark_ReturnsUnit()
        {
            //Arrange.
            var contextMock = new Mock<IDataContext>();
            var bookmarks = new List<Bookmark> 
            { 
                new Bookmark { Id = 1, URL = "www.google.com", ShortDescription = "Test 1" } 
            };
            var bookmark = new BookmarkVM { Id = 2, URL = "www.google.com", ShortDescription = "Test 2" };

            var mockDbSet = bookmarks.AsQueryable().BuildMockDbSet();
            mockDbSet.AddMockSetup(bookmarks);

            contextMock.Setup(x => x.Bookmarks).Returns(mockDbSet.Object);
            contextMock.Setup(x => x.SaveChangesAsync(new CancellationToken())).ReturnsAsync(1);

            var queryHandler = new CreateBookmark.Handler(contextMock.Object, _mapper);

            //Act.
            var result = await queryHandler.Handle(new BookmarkCreateCommand { Bookmark = bookmark }, new CancellationToken());

            //Assert.
            Assert.Equal(Unit.Value, result);
        }

        [Fact]
        public async Task UpdateBookmark_ReturnsBookmarkVM()
        {
            //Arrange.
            var contextMock = new Mock<IDataContext>();
            var bookmarks = new List<Bookmark> 
            {
                new Bookmark { Id = 1, URL = "www.google.com", ShortDescription = "Test 1" } 
            };
            var bookmark = new BookmarkVM { Id = 1, URL = "www.google.com", ShortDescription = "Test 3" };

            var mockDbSet = bookmarks.AsQueryable().BuildMockDbSet();
            mockDbSet.FindAsyncMockSetup(bookmarks);

            contextMock.Setup(x => x.Bookmarks).Returns(mockDbSet.Object);
            contextMock.Setup(x => x.SaveChangesAsync(new CancellationToken())).ReturnsAsync(1);

            var queryHandler = new UpdateBookmark.Handler(contextMock.Object, _mapper);

            //Act.
            var result = await queryHandler.Handle(new BookmarkUpdateCommand { Bookmark = bookmark }, new CancellationToken());

            //Assert.
            Assert.IsType<BookmarkVM>(result);
        }

        [Fact]
        public async Task UpdateBookmark_ThrowsException_NotFound()
        {
            //Arrange.
            var contextMock = new Mock<IDataContext>();
            var bookmarks = new List<Bookmark> 
            { 
                new Bookmark { Id = 1, URL = "www.google.com", ShortDescription = "Test 1" } 
            };
            var bookmark = new BookmarkVM { Id = 2, URL = "www.google.com", ShortDescription = "Test 3" };

            var mockDbSet = bookmarks.AsQueryable().BuildMockDbSet();
            mockDbSet.FindAsyncMockSetup(bookmarks);

            contextMock.Setup(x => x.Bookmarks).Returns(mockDbSet.Object);
            contextMock.Setup(x => x.SaveChangesAsync(new CancellationToken())).ReturnsAsync(1);

            var queryHandler = new UpdateBookmark.Handler(contextMock.Object, _mapper);

            //Act.
            var result = await Assert.ThrowsAsync<Exception>(() =>
                queryHandler.Handle(new BookmarkUpdateCommand { Bookmark = bookmark }, new CancellationToken()));

            //Assert.
            Assert.Equal(Errors.NotFound, result.Message);
        }

        [Fact]
        public async Task DeleteBookmark_BookmarkDeleted()
        {
            //Arrange.
            var contextMock = new Mock<IDataContext>();
            var bookmarks = new List<Bookmark> 
            { 
                new Bookmark { Id = 1, URL = "www.google.com", ShortDescription = "Test 1" } 
            };

            var mockDbSet = bookmarks.AsQueryable().BuildMockDbSet();
            mockDbSet.FindAsyncMockSetup(bookmarks);
            mockDbSet.RemoveMockSetup(bookmarks);

            contextMock.Setup(x => x.Bookmarks).Returns(mockDbSet.Object);
            contextMock.Setup(x => x.SaveChangesAsync(new CancellationToken())).ReturnsAsync(1);

            var queryHandler = new DeleteBookmark.Handler(contextMock.Object);

            //Act.
            var result = await queryHandler.Handle(new BookmarkDeleteCommand { Id = 1 }, new CancellationToken());

            //Assert.
            Assert.Equal(Unit.Value, result);
        }

        [Fact]
        public async Task DeleteBookmark_ThrowsException_NotFound()
        {
            //Arrange.
            var contextMock = new Mock<IDataContext>();
            var bookmarks = new List<Bookmark> 
            { 
                new Bookmark { Id = 1, URL = "www.google.com", ShortDescription = "Test 1" } 
            };

            var mockDbSet = bookmarks.AsQueryable().BuildMockDbSet();
            mockDbSet.FindAsyncMockSetup(bookmarks);
            mockDbSet.RemoveMockSetup(bookmarks);

            contextMock.Setup(x => x.Bookmarks).Returns(mockDbSet.Object);
            contextMock.Setup(x => x.SaveChangesAsync(new CancellationToken())).ReturnsAsync(1);

            var queryHandler = new DeleteBookmark.Handler(contextMock.Object);

            //Act.
            var result = await Assert.ThrowsAsync<Exception>(() =>
                queryHandler.Handle(new BookmarkDeleteCommand { Id = 2 }, new CancellationToken()));

            //Assert.
            Assert.Equal(Errors.NotFound, result.Message);
        }
    }
}
