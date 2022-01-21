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
    public class CallaboratorServiceTest {
        private readonly Mock<ICollaboratorRepository> _collaborator;
        private readonly Mock<IMapper> _mapper;
        private readonly CollaboratorService _collaboratorService;

        public CallaboratorServiceTest() {
            _collaborator = new Mock<ICollaboratorRepository>();
            _mapper = new Mock<IMapper>();
            _collaboratorService = new(_collaborator.Object, _mapper.Object);
        }

        [Fact]
        public void CreateNewCollaborator() {
            // Arrange
            CreateCollaboratorDto createCollaboratorDto = new() {
                FullName = "Joao Junior",
                AddressId = Guid.NewGuid(),
                BirthDate = new DateTime(2000, 1, 1),
                Cpf = "578.847.384-57",
                Phone = "123456789",
                Sex = "m"
            };

            Collaborator collaborator = new() {
                FullName = "Joao Junior",
                AddressId = Guid.NewGuid(),
                Address = new() {
                    Street = "Rua teste",
                    Number = "123",
                    City = "Ceará",
                    State = "CE",
                    District = "Centro",
                },
                BirthDate = new DateTime(2000, 1, 1),
                Cpf = "578.847.384-57",
                Phone = "123456789",
                Sex = "m"
            };

            CollaboratorDto collaboratorDto = new() {
                FullName = "Joao Junior",
                Address = new() {
                    Street = "Rua teste",
                    Number = "123",
                    City = "Ceará",
                    State = "CE",
                    District = "Centro",
                },
                BirthDate = new DateTime(2000, 1, 1),
                Cpf = "578.847.384-57",
                Phone = "123456789",
                Sex = "m"
            };

            // Act
            _collaborator.Setup(x => x.Create(It.IsAny<Collaborator>())).Returns(Task.FromResult(collaborator));
            _mapper.Setup(x => x.Map<Collaborator>(It.IsAny<CreateCollaboratorDto>())).Returns(collaborator);
            _mapper.Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>())).Returns(collaboratorDto);

            var result = _collaboratorService.Create(createCollaboratorDto);

            // Assert
            Assert.NotNull(result.Result);
            Assert.Equal(collaborator.Cpf, result.Result.Cpf);
        }

        [Theory]
        [InlineData("254.245.912-64")]
        public void CheckExistentCollaboratorByCpf(string cpf) {
            // Arrange
            IEnumerable<Collaborator> collaborators = new List<Collaborator>() {
                new() {
                    FullName = "Francisco Augusto",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                    IsActive = false
                },
                new() {
                    FullName = "Maria Eliza",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "578.847.384-57",
                    Phone = "123456789",
                }
            };

            IEnumerable<CollaboratorDto> collaboratorDtos = new List<CollaboratorDto>() {
                new() {
                    FullName = "Francisco Augusto",
                    BirthDate = DateTime.Now,
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = DateTime.Now,
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    BirthDate = DateTime.Now,
                    Cpf = "578.847.384-57",
                    Phone = "123456789",
                }
            };

            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns((Expression<Func<Collaborator, bool>> predicate)
                    => Task.FromResult(collaborators.ToList()
                        .Where(x => x.IsActive)
                        .FirstOrDefault(x => x.Cpf == cpf)));

            _mapper
                .Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.Cpf == cpf));

            var result = _collaboratorService.GetByCpf(cpf);

            // Assert
            Assert.NotNull(result.Result);
        }

        [Theory]
        [InlineData("254.245.912-66")]
        public void CheckNullCollaboratorByCpf(string cpf) {
            // Arrange
            IEnumerable<Collaborator> collaborators = new List<Collaborator>() {
                new() {
                    FullName = "Francisco Augusto",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                    IsActive = false
                },
                new() {
                    FullName = "Maria Eliza",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "578.847.384-57",
                    Phone = "123456789",
                }
            };

            IEnumerable<CollaboratorDto> collaboratorDtos = new List<CollaboratorDto>() {
                new() {
                    FullName = "Francisco Augusto",
                    BirthDate = DateTime.Now,
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = DateTime.Now,
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    BirthDate = DateTime.Now,
                    Cpf = "578.847.384-57",
                    Phone = "123456789",
                }
            };

            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns((Expression<Func<Collaborator, bool>> predicate)
                    => Task.FromResult(collaborators.Where(x => x.IsActive).ToList().FirstOrDefault(x => x.Cpf == cpf)));

            _mapper
                .Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.Cpf == cpf));

            var result = _collaboratorService.GetByCpf(cpf);

            // Assert
            Assert.Null(result.Result);
        }

        [Theory]
        [InlineData(1, 2, OrderCollaboratorColumn.Cpf, OrderType.ASC)]
        public void GetAllCollaboratorsPaged(
            int pageNumber,
            int pageSize,
            OrderCollaboratorColumn orderColumn = OrderCollaboratorColumn.FullName,
            OrderType orderType = OrderType.ASC
        ) {
            // Arrange
            IEnumerable<Collaborator> collaborators = new List<Collaborator>() {
                new() {
                    FullName = "Francisco Augusto",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
            };

            IEnumerable<CollaboratorDto> collaboratorDtos = new List<CollaboratorDto>() {
                new() {
                    FullName = "Francisco Augusto",
                    Address = new AddressDto() {
                        Id = Guid.NewGuid(),
                        City = "Ceará",
                        Number = "123",
                        State = "CE",
                        Street = "Rua Teste",
                    },
                    BirthDate = DateTime.Now,
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    Address = new AddressDto() {
                        Id = Guid.NewGuid(),
                        City = "Ceará",
                        Number = "123",
                        State = "CE",
                        Street = "Rua Teste",
                    },
                    BirthDate = DateTime.Now,
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    Address = new AddressDto() {
                        Id = Guid.NewGuid(),
                        City = "Ceará",
                        Number = "123",
                        State = "CE",
                        Street = "Rua Teste",
                    },
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
            };

            // Act
            _collaborator
                .Setup(x => x.GetAllPaged(pageNumber, pageSize, orderColumn, orderType))
                .Returns(Task.FromResult(collaborators.ToPagedList()));

            _mapper
                .Setup(x => x.Map<IEnumerable<CollaboratorDto>>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos);

            var result = _collaboratorService.GetAll();

            // Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result.Count() <= pageSize);
        }

        [Theory]
        [InlineData("254.245.912-64")]
        public void CheckExistentDeactivatedCollaboratorByCpf(string cpf) {
            // Arrange
            IEnumerable<Collaborator> collaborators = new List<Collaborator>() {
                new() {
                    FullName = "Francisco Augusto",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                    IsActive = false,
                },
                new() {
                    FullName = "Maria Eliza",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                    IsActive = false,
                },
                new() {
                    FullName = "Mara Maria",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-38",
                    Phone = "87575857",
                    IsActive = true,
                },
                new() {
                    FullName = "Ana Maria",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                    IsActive = false,
                },
            };

            IEnumerable<CollaboratorDto> collaboratorDtos = new List<CollaboratorDto>() {
                new() {
                    FullName = "Francisco Augusto",
                    BirthDate = DateTime.Now,
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = DateTime.Now,
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Mara Maria",
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-38",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                }
            };

            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns((Expression<Func<Collaborator, bool>> predicate)
                    => Task.FromResult(collaborators.ToList()
                        .Where(x => !x.IsActive)
                        .FirstOrDefault(x => x.Cpf == cpf)));

            _mapper
                .Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.Cpf == cpf));

            var result = _collaboratorService.GetByCpf(cpf);

            // Assert
            Assert.NotNull(result.Result);
        }

        [Theory]
        [InlineData("254.245.912-64")]
        public void CheckNullDeactivatedCollaboratorByCpf(string cpf) {
            // Arrange
            IEnumerable<Collaborator> collaborators = new List<Collaborator>() {
                new() {
                    FullName = "Francisco Augusto",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                    IsActive = false,
                },
                new() {
                    FullName = "Maria Eliza",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                    IsActive = false,
                },
                new() {
                    FullName = "Mara Maria",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-38",
                    Phone = "87575857",
                    IsActive = true,
                },
                new() {
                    FullName = "Ana Maria",
                    AddressId = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                    IsActive = false,
                },
            };

            IEnumerable<CollaboratorDto> collaboratorDtos = new List<CollaboratorDto>() {
                new() {
                    FullName = "Francisco Augusto",
                    BirthDate = DateTime.Now,
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = DateTime.Now,
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Mara Maria",
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-38",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = DateTime.Now,
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                }
            };

            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns((Expression<Func<Collaborator, bool>> predicate)
                    => Task.FromResult(collaborators.ToList()
                        .Where(x => !x.IsActive)
                        .FirstOrDefault(x => x.Cpf == cpf)));

            _mapper
                .Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.Cpf == cpf));

            var result = _collaboratorService.GetByCpf(cpf);

            // Assert
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async void CheckIfCollaboratorWasUpdatedSucsessfully() {
            // Arrange
            UpdateCollaboratorDto updateCollaboratorDto = new() {
                AddressId = Guid.NewGuid(),
                FullName = "Francisco Augusto",
                Phone = "123456789",
            };

            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns(
                    Task.FromResult(new Collaborator() {
                        FullName = "Francisco Augusto",
                        AddressId = Guid.NewGuid(),
                        BirthDate = DateTime.Now,
                        Cpf = "254.245.912-64",
                        Phone = "123456789",
                        IsActive = true,
                    }));

            _collaborator
                .Setup(x => x.Update(It.IsAny<Collaborator>()))
                .Returns(Task.FromResult(true));

            // Assert
            Assert.True(await _collaboratorService.Update(It.IsAny<string>(), updateCollaboratorDto));
        }

        [Fact]
        public async void CheckIfCollaboratorWasNotUpdatedSucsessfully() {
            // Act
            _collaborator
                .Setup(x => x.Update(It.IsAny<Collaborator>()))
                .Returns(Task.FromResult(false));
            // Assert
            Assert.False(await _collaboratorService.Update(It.IsAny<string>(), It.IsAny<UpdateCollaboratorDto>()));
        }

        [Fact]
        public async void CheckIfCollaboratorWasDeletedSucsessfully() {
            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns(
                    Task.FromResult(new Collaborator() {
                        FullName = "Francisco Augusto",
                        AddressId = Guid.NewGuid(),
                        BirthDate = DateTime.Now,
                        Cpf = "254.245.912-64",
                        Phone = "123456789",
                        IsActive = true,
                    }));

            _collaborator
                .Setup(x => x.Update(It.IsAny<Collaborator>()))
                .Returns(Task.FromResult(true));

            // Assert
            Assert.True(await _collaboratorService.Deactivate(It.IsAny<string>()));
        }

        [Fact]
        public async void CheckIfCollaboratorWasNotDeletedSucsessfully() {
            // Act
            _collaborator.Setup(x => x.Delete(It.IsAny<Collaborator>())).Returns(Task.FromResult(false));

            // Assert
            Assert.False(await _collaboratorService.Delete(It.IsAny<string>()));
        }
    }
}