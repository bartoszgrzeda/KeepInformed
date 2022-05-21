using KeepInformed.Application.Authorization.Commands.UserSignUp;
using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Common.Encrypter;
using KeepInformed.Common.EventBus;
using KeepInformed.Contracts.Authorization.Commands.UserSignUp;
using KeepInformed.Contracts.Authorization.Exceptions;
using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Infrastructure.Encrypter;
using KeepInformed.Tests.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KeepInformed.Tests.UnitTests.Authorization;

[TestClass]
public class UserSignUpTests
{
    private IUserRepository _userRepository;
    private IEncrypter _encrypter;
    private IEventBus _eventBus;

    [TestInitialize]
    public void Initialize()
    {
        _userRepository = new UserRepositoryMock();
        _encrypter = new Encrypter();
        _eventBus = new EventBusMock();
    }

    [TestMethod]
    public async Task UserSignUpCommandHandler_ShouldWork()
    {
        // ARRANGE
        var email = "email@gmail.com";
        var command = new UserSignUpCommand()
        {
            Email = email,
            Password = "password"
        };
        var commandHandler = new UserSignUpCommandHandler(_userRepository, _encrypter, _eventBus);

        // ACT
        await commandHandler.Handle(command, new CancellationToken());

        // ASSERT
        Assert.AreEqual(1, _userRepository.GetAll().Count());

        var user = _userRepository.GetByEmail(email);
        Assert.IsNotNull(user);
    }

    [TestMethod]
    public async Task UserSignUpCommandHandler_ProvidedExistingUser_ShouldThrow_UserWithProvidedEmailAlreadyExistsDomainException()
    {
        // ARRANGE
        var email = "email@gmail.com";
        var user = new User(Guid.NewGuid(), email, "password", "salt");
        await _userRepository.Add(user);

        var command = new UserSignUpCommand()
        {
            Email = email,
            Password = "password"
        };
        var commandHandler = new UserSignUpCommandHandler(_userRepository, _encrypter, _eventBus);

        // ASSERT
        await Assert.ThrowsExceptionAsync<UserWithProvidedEmailAlreadyExistsDomainException>(() => commandHandler.Handle(command, new CancellationToken()));
    }
}
