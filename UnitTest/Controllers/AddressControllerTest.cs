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
    public class AddressControllerTest {
        private readonly AddressController _controller;
        private readonly Mock<IAddressService> _addressService;

        public AddressControllerTest() {
            _addressService = new Mock<IAddressService>();
            _controller = new AddressController(_addressService.Object);
        }

        [Fact]
        public void CheckGetAddressesPagedSuccessfulResponse() {
            // Arrange
            IEnumerable<AddressDto> addressDtos =
            new List<AddressDto>() { new AddressDto() };

            // Act
            _addressService.Setup(x =>
             x.GetAllPaged(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<OrderAddressColumn>(),
                It.IsAny<OrderType>()))
                .Returns(
                    Task.FromResult(new Response<AddressDto>(
                            addressDtos.ToPagedList(1, 1),
                            addressDtos
                        )));

            var response = _controller.GetAddressesPaged(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<OrderAddressColumn>(),
                It.IsAny<OrderType>()
            );

            // Assert
            Assert.NotNull(response.Result);
        }

        [Fact]
        public void CheckGetDeactivatedAddressesPagedSuccessfulResponse() {
            // Arrange
            IEnumerable<AddressDto> addressDtos =
            new List<AddressDto>() { new AddressDto() };

            // Act
            _addressService.Setup(x =>
             x.GetAllDeactivatedPaged(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<OrderAddressColumn>(),
                It.IsAny<OrderType>()))
                .Returns(
                    Task.FromResult(new Response<AddressDto>(
                            addressDtos.ToPagedList(1, 1),
                            addressDtos
                        )));

            var response = _controller.GetDeactivatedAddressesPaged(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<OrderAddressColumn>(),
                It.IsAny<OrderType>()
            );

            // Assert
            Assert.NotNull(response.Result);
        }

        [Fact]
        public void CheckGetAddressByIdSuccessfulResponse() {
            // Act
            _addressService.Setup(x =>
             x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new AddressDto()));

            var response = _controller.GetAddress(It.IsAny<Guid>());

            // Assert
            Assert.NotNull(response.Result);
        }

        [Fact]
        public void CheckGetAddressByIdUnsuccessfulResponse() {
            // Act
            _addressService.Setup(x => x.Get(It.IsAny<Guid>()));

            var response = _controller.GetAddress(It.IsAny<Guid>());

            // Assert
            Assert.IsType<NotFoundResult>(response.Result.Result);
        }

        [Fact]
        public void CheckUpdateAddressSuccessfulResponse() {
            // Act
            _addressService.Setup(x =>
             x.Update(It.IsAny<Guid>(), It.IsAny<UpdateAddressDto>()))
                .Returns(Task.FromResult(true));

            var response = _controller.Put(It.IsAny<Guid>(), It.IsAny<UpdateAddressDto>());

            // Assert
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public void CheckUpdateAddressUnsuccessfulResponse() {
            // Act
            _addressService.Setup(x =>
             x.Update(It.IsAny<Guid>(), It.IsAny<UpdateAddressDto>()))
                .Returns(Task.FromResult(false));

            var response = _controller.Put(It.IsAny<Guid>(), It.IsAny<UpdateAddressDto>());

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void CheckDeactivateAddressSuccessfulResponse() {
            // Act
            _addressService.Setup(x =>
             x.Deactivate(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            var response = _controller.Deactivate(It.IsAny<Guid>());

            // Assert
            Assert.IsType<NoContentResult>(response.Result.Result);
        }

        [Fact]
        public void CheckDeactivateAddressUnsuccessfulResponse() {
            // Act
            _addressService.Setup(x =>
             x.Deactivate(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));

            var response = _controller.Deactivate(It.IsAny<Guid>());

            // Assert
            Assert.IsType<NotFoundResult>(response.Result.Result);
        }

        [Fact]
        public void CheckActivateAddressSuccessfulResponse() {
            Guid id = Guid.NewGuid();
            // Act
            _addressService.Setup(x =>
             x.Activate(id))
                .Returns(Task.FromResult(true));

            var response = _controller.Activate(id);

            // Assert
            Assert.IsType<NoContentResult>(response.Result.Result);
        }

        [Fact]
        public void CheckActivateAddressUnsuccessfulResponse() {
            // Act
            _addressService.Setup(x =>
             x.Activate(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));

            var response = _controller.Activate(It.IsAny<Guid>());

            // Assert
            Assert.IsType<NotFoundResult>(response.Result.Result);
        }
    }
}