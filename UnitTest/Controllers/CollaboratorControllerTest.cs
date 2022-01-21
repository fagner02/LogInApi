using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using LogInApi.Services;
using LogInApi.Dtos;
using LogInApi.Models;
using LogInApi.Enums;
using LogInApi.Controllers;
using Moq;
using Xunit;
using X.PagedList;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest {
    public class CollaboratorControllerTest {
        private readonly CollaboratorController _controller;
        private readonly Mock<ICollaboratorService> _collaboratorService;

        public CollaboratorControllerTest() {
            _collaboratorService = new Mock<ICollaboratorService>();
            _controller = new CollaboratorController(_collaboratorService.Object);
        }

        [Fact]
        public void CheckGetCollaboratorsPagedSuccessfulResponse() {
            // Arrange
            IEnumerable<CollaboratorDto> collaboratorDtos =
            new List<CollaboratorDto>() { new CollaboratorDto() };

            // Act
            _collaboratorService.Setup(x =>
             x.GetAllPaged(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<OrderCollaboratorColumn>(),
                It.IsAny<OrderType>()))
                .Returns(
                    Task.FromResult(new Response<CollaboratorDto>(
                            collaboratorDtos.ToPagedList(1, 1),
                            collaboratorDtos
                        )));

            var response = _controller.GetCollaboratorsPaged(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<OrderCollaboratorColumn>(),
                It.IsAny<OrderType>()
            );

            // Assert
            Assert.NotNull(response.Result);
        }

        [Fact]
        public void CheckGetCollaboratorsPagedUnsuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x =>
             x.GetAllPaged(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<OrderCollaboratorColumn>(),
                It.IsAny<OrderType>()))
                .Returns(It.IsAny<Task<Response<CollaboratorDto>>>());

            var response = _controller.GetCollaboratorsPaged(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<OrderCollaboratorColumn>(),
                It.IsAny<OrderType>()
            );

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void CheckGetDeactivatedCollaboratorsPagedSuccessfulResponse() {
            // Arrange
            IEnumerable<CollaboratorDto> collaboratorDtos =
            new List<CollaboratorDto>() { new CollaboratorDto() };

            // Act
            _collaboratorService.Setup(x =>
             x.GetAllDeactivatedPaged(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<OrderCollaboratorColumn>(),
                It.IsAny<OrderType>()))
                .Returns(
                    Task.FromResult(new Response<CollaboratorDto>(
                            collaboratorDtos.ToPagedList(1, 1),
                            collaboratorDtos
                        )));

            var response = _controller.GetDeactivatedCollaboratorsPaged(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<OrderCollaboratorColumn>(),
                It.IsAny<OrderType>()
            );

            // Assert
            Assert.NotNull(response.Result);
        }

        [Fact]
        public void CheckGetCollaboratorByCpfSuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x =>
             x.GetByCpf(It.IsAny<string>()))
                .Returns(Task.FromResult(new CollaboratorDto()));

            var response = _controller.Get(It.IsAny<string>());

            // Assert
            Assert.NotNull(response.Result);
        }

        [Fact]
        public void CheckGetCollaboratorByCpfUnsuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x => x.GetByCpf(It.IsAny<string>()));

            var response = _controller.Get(It.IsAny<string>());

            // Assert
            Assert.IsType<NotFoundResult>(response.Result.Result);
        }

        [Fact]
        public void CheckGetCollaboratorByNameSuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x =>
             x.GetByName(It.IsAny<string>()))
                .Returns(Task.FromResult(new CollaboratorDto()));

            var response = _controller.Get(It.IsAny<string>());

            // Assert
            Assert.NotNull(response.Result);
        }

        [Fact]
        public void CheckGetCollaboratorByNameUnsuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x =>
             x.GetByCpf(It.IsAny<string>()));

            var response = _controller.Get(It.IsAny<string>());

            // Assert
            Assert.IsType<NotFoundResult>(response.Result.Result);
        }

        [Fact]
        public void CheckUpdateCollaboratorSuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x =>
             x.Update(It.IsAny<string>(), It.IsAny<UpdateCollaboratorDto>()))
                .Returns(Task.FromResult(true));

            var response = _controller.Put(It.IsAny<string>(), It.IsAny<UpdateCollaboratorDto>());

            // Assert
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public void CheckUpdateCollaboratorUnsuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x =>
             x.Update(It.IsAny<string>(), It.IsAny<UpdateCollaboratorDto>()))
                .Returns(Task.FromResult(false));

            var response = _controller.Put(It.IsAny<string>(), It.IsAny<UpdateCollaboratorDto>());

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void CheckDeactivateCollaboratorSuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x =>
             x.Deactivate(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var response = _controller.Deactivate(It.IsAny<string>());

            // Assert
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public void CheckDeactivateCollaboratorUnsuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x =>
             x.Deactivate(It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            var response = _controller.Deactivate(It.IsAny<string>());

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void CheckActivateCollaboratorSuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x =>
             x.Activate(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var response = _controller.Activate(It.IsAny<string>());

            // Assert
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public void CheckActivateCollaboratorUnsuccessfulResponse() {
            // Act
            _collaboratorService.Setup(x =>
             x.Activate(It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            var response = _controller.Activate(It.IsAny<string>());

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }
    }
}