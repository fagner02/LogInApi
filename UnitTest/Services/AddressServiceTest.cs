using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using LogInApi.Dtos;
using LogInApi.Repositories;
using Moq;
using Xunit;
using LogInApi.Models;
using AutoMapper;
using LogInApi.Services;
using System.Linq.Expressions;
using System.Linq;
using X.PagedList;
using LogInApi.Enums;

namespace UnitTest {
    public class AddressServiceTest {
        private readonly Mock<IAddressRepository> _address;
        private readonly Mock<IMapper> _mapper;
        private readonly AddressService _addressService;

        public AddressServiceTest() {
            _address = new Mock<IAddressRepository>();
            _mapper = new Mock<IMapper>();
            _addressService = new(_mapper.Object, _address.Object);
        }

        [Fact]
        public void CreateNewAddress() {
            // Arrange
            CreateAddressDto createAddressDto = new() {
                Street = "Street",
                Number = "1111",
                City = "City",
                State = "State",
            };

            Address collaborator = new() {
                Street = "Street",
                Number = "1111",
                City = "City",
                State = "State",
            };

            AddressDto collaboratorDto = new() {
                Street = "Street",
                Number = "1111",
                City = "City",
                State = "State",
            };

            // Act
            _address.Setup(x => x.Create(It.IsAny<Address>())).Returns(Task.FromResult(collaborator));
            _mapper.Setup(x => x.Map<Address>(It.IsAny<CreateAddressDto>())).Returns(collaborator);
            _mapper.Setup(x => x.Map<AddressDto>(It.IsAny<Address>())).Returns(collaboratorDto);

            var result = _addressService.Create(createAddressDto);

            // Assert
            Assert.True(result.IsCompletedSuccessfully);
        }

        [Fact]
        public void CheckExistentActiveAddressById() {
            // Arrange
            Guid id = new("0B8E3DEE-E055-4088-8469-CBE7740D9391");
            IEnumerable<Address> collaborators = new List<Address>() {
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                    IsActive = false
                },
                new() {
                    Id = id,
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                }
            };

            IEnumerable<AddressDto> collaboratorDtos = new List<AddressDto>() {
                new() {
                    Id = id,
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                }
            };

            // Act
            _address
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(collaborators.ToList()
                        .Where(x => x.IsActive)
                        .FirstOrDefault(x => x.Id == id)));

            _mapper
                .Setup(x => x.Map<AddressDto>(It.IsAny<Address>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.Id == id));

            var result = _addressService.Get(id);

            // Assert
            Assert.NotNull(result.Result);
        }

        [Fact]
        public void CheckNullActiveAddressById() {
            // Arrange
            Guid id = new("0B8E3DEE-E055-4088-8469-CBE7740D9391");
            IEnumerable<Address> collaborators = new List<Address>() {
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                    IsActive = false
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                }
            };

            IEnumerable<AddressDto> collaboratorDtos = new List<AddressDto>() {
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                }
            };
            // Act
            _address
                .Setup(x => x.Get(id))
                .Returns((Task.FromResult(collaborators.ToList()
                        .Where(x => x.IsActive)
                        .FirstOrDefault(x => x.Id == id))));

            _mapper
                .Setup(x => x.Map<AddressDto>(It.IsAny<Address>()))
                .Returns(It.IsAny<AddressDto>());

            var result = _addressService.Get(id);

            // Assert
            Assert.Null(result.Result);
        }

        [Fact]
        public void CheckExistentDeactivatedAddressById() {
            // Arrange
            Guid id = new("0B8E3DEE-E055-4088-8469-CBE7740D9391");
            IEnumerable<Address> collaborators = new List<Address>() {
                new() {
                    Id = id,
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                    IsActive = false
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                }
            };

            IEnumerable<AddressDto> collaboratorDtos = new List<AddressDto>() {
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Id = id,
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                }
            };

            // Act
            _address
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(collaborators.ToList()
                        .Where(x => !x.IsActive)
                        .FirstOrDefault(x => x.Id == id)));

            _mapper
                .Setup(x => x.Map<AddressDto>(It.IsAny<Address>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.Id == id));

            var result = _addressService.Get(id);

            // Assert
            Assert.NotNull(result.Result);
        }

        [Fact]
        public void CheckNullDeactivatedAddressById() {
            // Arrange
            Guid id = Guid.NewGuid();
            IEnumerable<Address> collaborators = new List<Address>() {
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                    IsActive = false
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                }
            };

            IEnumerable<AddressDto> collaboratorDtos = new List<AddressDto>() {
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                }
            };

            // Act
            _address
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(collaborators
                    .Where(x => !x.IsActive).ToList().FirstOrDefault(x => x.Id == id)));

            _mapper
                .Setup(x => x.Map<AddressDto>(It.IsAny<Address>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.Id == id));

            var result = _addressService.Get(id);

            // Assert
            Assert.Null(result.Result);
        }

        [Theory]
        [InlineData(1, 2, OrderAddressColumn.Id, OrderType.ASC, OrderAddressColumn.Street, "")]
        public void GetAllAddresssPaged(
            int pageNumber,
            int pageSize,
            OrderAddressColumn orderColumn,
            OrderType orderType,
            OrderAddressColumn searchColumn,
            string searchValue
        ) {
            // Arrange
            IEnumerable<Address> collaborators = new List<Address>() {
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                    IsActive = false
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                }
            };

            IEnumerable<AddressDto> collaboratorDtos = new List<AddressDto>() {
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                },
                new() {
                    Street = "Street",
                    Number = "1111",
                    City = "City",
                    State = "State",
                }
            };

            // Act
            _address
                .Setup(x => x.GetAllPaged(
                    pageNumber,
                    pageSize,
                    orderColumn,
                    orderType,
                    searchColumn,
                    searchValue))
                .Returns(Task.FromResult(collaborators.ToPagedList()));

            _mapper
                .Setup(x => x.Map<IEnumerable<AddressDto>>(It.IsAny<Address>()))
                .Returns(collaboratorDtos);

            var result = _addressService.GetAllPaged(
                pageNumber,
                pageSize,
                orderColumn,
                orderType,
                searchColumn,
                searchValue);

            // Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result.PageNumber == pageNumber);
        }

        [Fact]
        public async void CheckIfAddressWasUpdatedSuccessfully() {
            // Arrange
            UpdateAddressDto updateAddressDto = new() {
                Street = "Street",
                Number = "1111",
                City = "City",
                State = "State",
            };

            // Act
            _address
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(
                    Task.FromResult(new Address() {
                        Street = "Street",
                        Number = "1111",
                        City = "City",
                        State = "State",
                        IsActive = true,
                    }));

            _address
                .Setup(x => x.Update(It.IsAny<Address>()))
                .Returns(Task.FromResult(true));

            // Assert
            Assert.True(await _addressService.Update(It.IsAny<Guid>(), updateAddressDto));
        }

        [Fact]
        public async void CheckIfAddressWasNotUpdatedSuccessfully() {
            // Act
            _address
                .Setup(x => x.Update(It.IsAny<Address>()))
                .Returns(Task.FromResult(false));
            // Assert
            Assert.False(await _addressService.Update(It.IsAny<Guid>(), It.IsAny<UpdateAddressDto>()));
        }

        [Fact]
        public async void CheckIfAddressWasDeletedSuccessfully() {
            // Act
            _address
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(
                    Task.FromResult(new Address() {
                        Street = "Street",
                        Number = "1111",
                        City = "City",
                        State = "State",
                        IsActive = true,
                    }));

            _address
                .Setup(x => x.Update(It.IsAny<Address>()))
                .Returns(Task.FromResult(true));

            // Assert
            Assert.True(await _addressService.Deactivate(It.IsAny<Guid>()));
        }

        [Fact]
        public async void CheckIfAddressWasNotDeletedSuccessfully() {
            // Act
            _address.Setup(x => x.Update(It.IsAny<Address>())).Returns(Task.FromResult(false));

            // Assert
            Assert.False(await _addressService.Deactivate(It.IsAny<Guid>()));
        }
    }
}