﻿using Lodging.DataAccess;
using Lodging.DataAccess.Repositories;
using Lodging.Models;
using Lodging.WebAPI.Controllers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
    public class RentalControllerTest
    {
        private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
        private static readonly DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;
        private readonly RentalController _controller;
        private readonly UnitOfWork _unitOfWork;

        public RentalControllerTest()
        {
            var contextMock = new Mock<LodgingContext>(_options);
            var loggerMock = new Mock<ILogger<RentalController>>();
            var repositoryMock = new Mock<Repository<RentalModel>>(new LodgingContext(_options));
            var unitOfWorkMock = new Mock<UnitOfWork>(contextMock.Object);

            repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
            repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.FromResult(1));
            repositoryMock.Setup(m => m.InsertAsync(It.IsAny<RentalModel>())).Returns(Task.FromResult<RentalModel>(null));
            repositoryMock.Setup(m => m.SelectAsync()).Returns(Task.FromResult<IEnumerable<RentalModel>>(null));
            repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
            repositoryMock.Setup(m => m.SelectAsync(1)).Returns(Task.FromResult<RentalModel>(null));
            repositoryMock.Setup(m => m.Update(It.IsAny<RentalModel>()));
            unitOfWorkMock.Setup(m => m.Rental).Returns(repositoryMock.Object);

            _unitOfWork = unitOfWorkMock.Object;
            _controller = new RentalController(_unitOfWork);
        }

        [Fact]
        public async void Test_Controller_Delete()
        {
            var resultFail = await _controller.DeleteAsync(0);
            var resultPass = await _controller.DeleteAsync(1);

            Assert.NotNull(resultFail);
            Assert.NotNull(resultPass);
        }

        [Fact]
        public async void Test_Controller_Get()
        {
            var resultMany = await _controller.GetAsync();
            var resultFail = await _controller.GetAsync(0);
            var resultOne = await _controller.GetAsync(1);

            Assert.NotNull(resultMany);
            Assert.NotNull(resultFail);
            Assert.NotNull(resultOne);
        }

        [Fact]
        public async void Test_Controller_Post()
        {
            var resultPass = await _controller.PostAsync(new RentalModel());

            Assert.NotNull(resultPass);
        }

        [Fact]
        public async void Test_Controller_Put()
        {
            var resultPass = await _controller.PutAsync(new RentalModel());

            Assert.NotNull(resultPass);
        }
    }
}