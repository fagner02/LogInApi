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
        public void CreateNewCollaboratorSuccsessfully() {
            // Arrange
            CreateCollaboratorDto createCollaboratorDto = new() {
                FullName = "Joao Junior",
                AddressId = Guid.NewGuid(),
                BirthDate = "10/30/2000",
                Cpf = "578.847.384-57",
                Phone = "123456789",
                Sex = "I"
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
                Sex = "I"
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
                BirthDate = "10/30/2000",
                Cpf = "578.847.384-57",
                Phone = "123456789",
                Sex = "I"
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

        [Fact]
        public void CreateNewCollaboratorWithInvalidSexException() {
            // Arrange
            CreateCollaboratorDto createCollaboratorDto = new() {
                FullName = "Joao Junior",
                AddressId = Guid.NewGuid(),
                BirthDate = "10/30/2000",
                Cpf = "578.847.384-57",
                Phone = "123456789",
                Sex = "j"
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
                Sex = "j"
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
                BirthDate = "10/30/2000",
                Cpf = "578.847.384-57",
                Phone = "123456789",
                Sex = "j"
            };

            // Act
            _collaborator.Setup(x => x.Create(It.IsAny<Collaborator>())).Returns(Task.FromResult(collaborator));
            _mapper.Setup(x => x.Map<Collaborator>(It.IsAny<CreateCollaboratorDto>())).Returns(collaborator);
            _mapper.Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>())).Returns(collaboratorDto);

            // Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => {
                var result = await _collaboratorService.Create(createCollaboratorDto);
            });

            Assert.Equal("Invalid Sex character", exception.Result.Message);
        }

        [Fact]
        public void CreateNewCollaboratorWithInvalidCpfException() {
            // Arrange
            CreateCollaboratorDto createCollaboratorDto = new() {
                FullName = "Joao Junior",
                AddressId = Guid.NewGuid(),
                BirthDate = "10/30/2000",
                Cpf = "578.847.384-59",
                Phone = "123456789",
                Sex = "I"
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
                Cpf = "578.847.384-59",
                Phone = "123456789",
                Sex = "I"
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
                BirthDate = "10/30/2000",
                Cpf = "578.847.384-59",
                Phone = "123456789",
                Sex = "I"
            };

            // Act
            _collaborator.Setup(x => x.Create(It.IsAny<Collaborator>())).Returns(Task.FromResult(collaborator));
            _mapper.Setup(x => x.Map<Collaborator>(It.IsAny<CreateCollaboratorDto>())).Returns(collaborator);
            _mapper.Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>())).Returns(collaboratorDto);

            // Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => {
                var result = await _collaboratorService.Create(createCollaboratorDto);
            });

            Assert.Equal("Invalid CPF.", exception.Result.Message);
        }

        [Fact]
        public void CreateNewCollaboratorWithInvalidPhoneException() {
            // Arrange
            CreateCollaboratorDto createCollaboratorDto = new() {
                FullName = "Joao Junior",
                AddressId = Guid.NewGuid(),
                BirthDate = "10/30/2000",
                Cpf = "578.847.384-57",
                Phone = "88 8 8888 8888",
                Sex = "I"
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
                Phone = "88 8 8888 8888",
                Sex = "I"
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
                BirthDate = "10/30/2000",
                Cpf = "578.847.384-57",
                Phone = "88 8 8888 8888",
                Sex = "I"
            };

            // Act
            _collaborator.Setup(x => x.Create(It.IsAny<Collaborator>())).Returns(Task.FromResult(collaborator));
            _mapper.Setup(x => x.Map<Collaborator>(It.IsAny<CreateCollaboratorDto>())).Returns(collaborator);
            _mapper.Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>())).Returns(collaboratorDto);

            // Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => {
                var result = await _collaboratorService.Create(createCollaboratorDto);
            });

            Assert.Equal("Invalid Phone. Check invalid symbols and whitespaces.", exception.Result.Message);
        }

        [Fact]
        public void CreateNewCollaboratorWithInvalidAgeException() {
            // Arrange
            CreateCollaboratorDto createCollaboratorDto = new() {
                FullName = "Joao Junior",
                AddressId = Guid.NewGuid(),
                BirthDate = "10/30/2010",
                Cpf = "578.847.384-57",
                Phone = "123456789",
                Sex = "I"
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
                BirthDate = new DateTime(2010, 10, 30),
                Cpf = "578.847.384-57",
                Phone = "123456789",
                Sex = "I"
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
                BirthDate = "10/30/2010",
                Cpf = "578.847.384-57",
                Phone = "123456789",
                Sex = "I"
            };

            // Act
            _collaborator.Setup(x => x.Create(It.IsAny<Collaborator>())).Returns(Task.FromResult(collaborator));
            _mapper.Setup(x => x.Map<Collaborator>(It.IsAny<CreateCollaboratorDto>())).Returns(collaborator);
            _mapper.Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>())).Returns(collaboratorDto);

            // Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => {
                var result = await _collaboratorService.Create(createCollaboratorDto);
            });

            Assert.Equal("Collaborator must be 18 years old or older.", exception.Result.Message);
        }

        [Theory]
        [InlineData("254.245.912-64")]
        public void CheckExistentActiveCollaboratorByCpf(string cpf) {
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
                    IsActive = false,
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
                    BirthDate = "10/30/2000",
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = "10/30/2000",
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = "10/30/2000",
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    BirthDate = "10/30/2000",
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
        public void CheckNullActiveCollaboratorByCpf(string cpf) {
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
                    BirthDate = "10/30/2000",
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = "10/30/2000",
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = "10/30/2000",
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    BirthDate = "10/30/2000",
                    Cpf = "578.847.384-57",
                    Phone = "123456789",
                }
            };

            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns((Expression<Func<Collaborator, bool>> predicate)
                    => Task.FromResult(collaborators
                    .Where(x => x.IsActive).ToList().FirstOrDefault(x => x.Cpf == cpf)));

            _mapper
                .Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.Cpf == cpf));

            var result = _collaboratorService.GetByCpf(cpf);

            // Assert
            Assert.Null(result.Result);
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
                    BirthDate = "10/30/2000",
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = "10/30/2000",
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = "10/30/2000",
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    BirthDate = "10/30/2000",
                    Cpf = "578.847.384-57",
                    Phone = "123456789",
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
        [InlineData("254.245.912-66")]
        public void CheckNullDeactivatedCollaboratorByCpf(string cpf) {
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
                    BirthDate = "10/30/2000",
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = "10/30/2000",
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = "10/30/2000",
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    BirthDate = "10/30/2000",
                    Cpf = "578.847.384-57",
                    Phone = "123456789",
                }
            };

            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns((Expression<Func<Collaborator, bool>> predicate)
                    => Task.FromResult(collaborators
                    .Where(x => !x.IsActive).ToList().FirstOrDefault(x => x.Cpf == cpf)));

            _mapper
                .Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.Cpf == cpf));

            var result = _collaboratorService.GetByCpf(cpf);

            // Assert
            Assert.Null(result.Result);
        }

        [Theory]
        [InlineData(OrderCollaboratorColumn.FullName, "", 1, 2, OrderCollaboratorColumn.Cpf, OrderType.ASC)]
        public void GetAllCollaboratorsPaged(
            OrderCollaboratorColumn searchColumn,
            string search,
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
                    BirthDate = "10/30/2000",
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
                   BirthDate = "10/30/2000",
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
                    BirthDate = "10/30/2000",
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
            };

            // Act
            _collaborator
                .Setup(x => x.GetAllPaged(
                pageNumber,
                pageSize,
                orderColumn,
                orderType,
                searchColumn,
                search))
                .Returns(Task.FromResult(collaborators.ToPagedList()));

            _mapper
                .Setup(x => x.Map<IEnumerable<CollaboratorDto>>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos);

            var result = _collaboratorService.GetAllPaged(
                pageNumber,
                pageSize,
                orderColumn,
                orderType,
                searchColumn,
                search);

            // Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result.PageNumber == pageNumber);
        }

        [Fact]
        public async void CheckIfCollaboratorWasUpdatedSuccessfully() {
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
        public async void CheckIfCollaboratorWasNotUpdatedSuccessfully() {
            // Act
            _collaborator
                .Setup(x => x.Update(It.IsAny<Collaborator>()))
                .Returns(Task.FromResult(false));
            // Assert
            Assert.False(await _collaboratorService.Update(It.IsAny<string>(), It.IsAny<UpdateCollaboratorDto>()));
        }

        [Fact]
        public async void CheckIfCollaboratorWasDeletedSuccessfully() {
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
        public async void CheckIfCollaboratorWasNotDeletedSuccessfully() {
            // Act
            _collaborator.Setup(x => x.Update(It.IsAny<Collaborator>())).Returns(Task.FromResult(false));

            // Assert
            Assert.False(await _collaboratorService.Deactivate(It.IsAny<string>()));
        }

        [Theory]
        [InlineData("Francisco Augusto")]
        public void CheckExistentActiveCollaboratorByName(string fullName) {
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
                    IsActive = false
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
                    BirthDate = "10/30/2000",
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = "10/30/2000",
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = "10/30/2000",
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    BirthDate = "10/30/2000",
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
                        .FirstOrDefault(x => x.FullName == fullName)));

            _mapper
                .Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.FullName == fullName));

            var result = _collaboratorService.GetByName(fullName);

            // Assert
            Assert.NotNull(result.Result);
        }

        [Theory]
        [InlineData("Jose Augusto")]
        public void CheckNullActiveCollaboratorByName(string fullName) {
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
                    BirthDate = "10/30/2000",
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = "10/30/2000",
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = "10/30/2000",
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    BirthDate = "10/30/2000",
                    Cpf = "578.847.384-57",
                    Phone = "123456789",
                }
            };

            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns((Expression<Func<Collaborator, bool>> predicate)
                    => Task.FromResult(collaborators
                    .Where(x => x.IsActive).ToList().FirstOrDefault(x => x.FullName == fullName)));

            _mapper
                .Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.FullName == fullName));

            var result = _collaboratorService.GetByName(fullName);

            // Assert
            Assert.Null(result.Result);
        }

        [Theory]
        [InlineData("Francisco Augusto")]
        public void CheckExistentDeactivatedCollaboratorByName(string fullName) {
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
                    BirthDate = "10/30/2000",
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = "10/30/2000",
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = "10/30/2000",
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    BirthDate = "10/30/2000",
                    Cpf = "578.847.384-57",
                    Phone = "123456789",
                }
            };

            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns((Expression<Func<Collaborator, bool>> predicate)
                    => Task.FromResult(collaborators.ToList()
                        .Where(x => !x.IsActive)
                        .FirstOrDefault(x => x.FullName == fullName)));

            _mapper
                .Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.FullName == fullName));

            var result = _collaboratorService.GetByName(fullName);

            // Assert
            Assert.NotNull(result.Result);
        }

        [Theory]
        [InlineData("Jose Augusto")]
        public void CheckNullDeactivatedCollaboratorByName(string fullName) {
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
                    BirthDate = "10/30/2000",
                    Cpf = "254.245.912-64",
                    Phone = "123456789",
                },
                new() {
                    FullName = "Maria Eliza",
                    BirthDate = "10/30/2000",
                    Cpf = "932.307.820-46",
                    Phone = "0808708009",
                },
                new() {
                    FullName = "Ana Maria",
                    BirthDate = "10/30/2000",
                    Cpf = "312.153.739-37",
                    Phone = "87575857",
                },
                new() {
                    FullName = "Igor Silva",
                    BirthDate = "10/30/2000",
                    Cpf = "578.847.384-57",
                    Phone = "123456789",
                }
            };

            // Act
            _collaborator
                .Setup(x => x.Get(It.IsAny<Expression<Func<Collaborator, bool>>>()))
                .Returns((Expression<Func<Collaborator, bool>> predicate)
                    => Task.FromResult(collaborators
                    .Where(x => !x.IsActive).ToList().FirstOrDefault(x => x.FullName == fullName)));

            _mapper
                .Setup(x => x.Map<CollaboratorDto>(It.IsAny<Collaborator>()))
                .Returns(collaboratorDtos.FirstOrDefault(x => x.FullName == fullName));

            var result = _collaboratorService.GetByName(fullName);

            // Assert
            Assert.Null(result.Result);
        }

    }
}