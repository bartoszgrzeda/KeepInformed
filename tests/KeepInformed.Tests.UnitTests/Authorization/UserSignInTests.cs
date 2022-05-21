using KeepInformed.Application.Authorization.Commands.UserSignIn;
using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Common.Encrypter;
using KeepInformed.Common.Jwt;
using KeepInformed.Contracts.Authorization.Commands.UserSignIn;
using KeepInformed.Contracts.Authorization.Exceptions;
using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Infrastructure.Encrypter;
using KeepInformed.Tests.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KeepInformed.Tests.UnitTests.Authorization;

[TestClass]
public class UserSignInTests
{
    private IUserRepository _userRepository;
    private IEncrypter _encrypter;
    private IJwtTokenService _jwtTokenService;

    [TestInitialize]
    public void Initialize()
    {
        _userRepository = new UserRepositoryMock();
        _encrypter = new Encrypter();
        _jwtTokenService = new JwtTokenServiceMock();
    }

    [TestMethod]
    public async Task UserSignInCommandHandler_ShouldWork()
    {
        // ARRANGE
        var email = "email@gmail.com";
        var password = "password";
        var salt = _encrypter.GetSalt();
        var hashedPassword = _encrypter.GetHash(password, salt);
        var user = new User(Guid.NewGuid(), email, hashedPassword, salt);
        user.SetIsEmailConfirmed(true);
        await _userRepository.Add(user);
        var oldLastSignInDate = user.LastSignInDate;

        var command = new UserSignInCommand()
        {
            Email = email,
            Password = password
        };
        var commandHandler = new UserSignInCommandHandler(_userRepository, _encrypter, _jwtTokenService);

        // ACT
        await commandHandler.Handle(command, new CancellationToken());

        // ASSERT
        var signedInUser = await _userRepository.GetByEmail(email);
        Assert.AreNotEqual(oldLastSignInDate, signedInUser.LastSignInDate);
        Assert.IsNotNull(_jwtTokenService.GetFromCache(email));
    }

    [TestMethod]
    public async Task UserSignUpCommandHandler_ProvidedNotExistingUser_ShouldThrow_UserInvalidCredentialsDomainException()
    {
        // ARRANGE
        var email = "email@gmail.com";
        var password = "password";
        var salt = _encrypter.GetSalt();
        var hashedPassword = _encrypter.GetHash(password, salt);
        var user = new User(Guid.NewGuid(), email, hashedPassword, salt);
        var command = new UserSignInCommand()
        {
            Email = email,
            Password = password
        };
        var commandHandler = new UserSignInCommandHandler(_userRepository, _encrypter, _jwtTokenService);

        // ASSERT
        await Assert.ThrowsExceptionAsync<UserInvalidCredentialsDomainException>(() => commandHandler.Handle(command, new CancellationToken()));
    }

    [TestMethod]
    public async Task UserSignUpCommandHandler_ProvidedInvalidPassword_ShouldThrow_UserInvalidCredentialsDomainException()
    {
        // ARRANGE
        var email = "email@gmail.com";
        var password = "password";
        var salt = _encrypter.GetSalt();
        var hashedPassword = _encrypter.GetHash(password, salt);
        var user = new User(Guid.NewGuid(), email, hashedPassword, salt);
        await _userRepository.Add(user);

        var command = new UserSignInCommand()
        {
            Email = email,
            Password = "invalidPassword"
        };
        var commandHandler = new UserSignInCommandHandler(_userRepository, _encrypter, _jwtTokenService);

        // ASSERT
        await Assert.ThrowsExceptionAsync<UserInvalidCredentialsDomainException>(() => commandHandler.Handle(command, new CancellationToken()));
    }

    [TestMethod]
    public async Task UserSignInCommandHandler_ProvidedUserWithNotConfirmedEmail_ShouldThrow_UserEmailNotConfirmedDomainException()
    {
        // ARRANGE
        var email = "email@gmail.com";
        var password = "password";
        var salt = _encrypter.GetSalt();
        var hashedPassword = _encrypter.GetHash(password, salt);
        var user = new User(Guid.NewGuid(), email, hashedPassword, salt);
        await _userRepository.Add(user);

        var command = new UserSignInCommand()
        {
            Email = email,
            Password = password
        };
        var commandHandler = new UserSignInCommandHandler(_userRepository, _encrypter, _jwtTokenService);

        // ASSERT
        await Assert.ThrowsExceptionAsync<UserEmailNotConfirmedDomainException>(() => commandHandler.Handle(command, new CancellationToken()));
    }
}
