using System;
using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Infra.Repositories.User;
using NSubstitute;
using System.Threading.Tasks;
using FluentAssertions;
using Application.Models;
using Xunit;
using Domain.Entity;
using Tests.Builders;
using Application.Services.NotificationService;

namespace Tests.Unit.Application.Services
{
  public class UserServiceTests
  {
    private IUserRepository _userRepository;
    private IUserService _userService;
    private INotificationService _notificationService;

    public UserServiceTests()
    {
      _userRepository = Substitute.For<IUserRepository>();
      _notificationService = Substitute.For<INotificationService>();
      _userService = new UserService(_userRepository, _notificationService);
    }

    [Fact]
    public async Task should_save_user()
    {
      //arrange
      var model = new UserRequestModel()
      {
        IsActive = true,
        Login = "grahl",
        Name = "Grahl",
        Password = "12345678"
      };

      //action
      await _userService.Create(model);

      //assert
      await _userRepository
          .Received(1)
          .Create(Arg.Is<User>(d => d.IsActive == true
                                       && d.Login == "grahl"
                                       && d.Name.ToString() == "Grahl"
                                       && d.Password == "12345678"));
    }

    [Fact]
    public async Task should_update_user()
    {
      //arrange
      var userId = Guid.NewGuid();
      var model = new UserRequestModel()
      {
        IsActive = false,
        Login = "grahl",
        Name = "Grahl12",
        Password = "12345678"
      };
      var user = new UserBuilder()
          .WithName("Grahl")
          .ComLogin("grahl")
          .IsActive()
          .ComSenha("123456789")
          .Construct();

      _userRepository
          .GetById(userId)
          .Returns(user);

      //action
      await _userService
          .Update(userId, model);

      //assert
      await _userRepository
          .Received(1)
          .Update(Arg.Is<User>(d => d.IsActive == false
                                                && d.Login == "grahl"
                                                && d.Name.ToString() == "Grahl12"
                                                && d.Password == "12345678"));
    }

    [Fact]
    public async Task should_inativar_user()
    {
      //arrange
      var userId = Guid.NewGuid();
      var user = new UserBuilder()
          .WithName("Grahl")
          .ComLogin("grahl")
          .IsActive()
          .ComSenha("123456")
          .Construct();

      _userRepository
          .GetById(userId)
          .Returns(user);

      //action
      await _userService
          .Delete(userId);

      //assert
      await _userRepository
          .Received(1)
          .Update(Arg.Is<User>(d => d.IsActive == false
                                                  && d.Login == "grahl"
                                                  && d.Name.ToString() == "Grahl"
                                                  && d.Password == "123456"));
    }

    [Fact]
    public async Task should_obter_por_id()
    {
      //arrange
      var userId = Guid.NewGuid();
      var user = new UserBuilder()
          .WithName("Grahl")
          .ComLogin("grahl")
          .IsActive()
          .ComSenha("123456")
          .WithId(userId)
          .Construct();

      _userRepository
          .GetById(userId)
          .Returns(user);

      //action
      var userRetornado = await _userService
          .GetById(userId);

      //assert
      userRetornado.Name.ToString().Should().Be(user.Name.ToString());
      userRetornado.Login.Should().Be(user.Login);
      userRetornado.IsActive.Should().Be(user.IsActive);
      userRetornado.Password.Should().Be(user.Password);
      userRetornado.Id.Should().Be(userId);
    }

    [Fact]
    public async Task should_retornar_todos_os_users()
    {
      //arrange
      var userIdA = Guid.NewGuid();
      var userA = new UserBuilder()
          .WithName("Grahl")
          .ComLogin("grahl")
          .IsActive()
          .ComSenha("123456")
          .WithId(userIdA)
          .Construct();
      var userIdB = Guid.NewGuid();
      var userB = new UserBuilder()
          .WithName("Darlei")
          .ComLogin("darlei")
          .InisActive()
          .ComSenha("12345678")
          .WithId(userIdB)
          .Construct();
      var users = new List<User>();
      users.Add(userA);
      users.Add(userB);

      _userRepository.GetAll().Returns(users.ToList());

      //action
      var usersRetornados = await _userService.GetAll();

      //assert
      usersRetornados.Should().HaveCount(2);

      usersRetornados[0].Name.ToString().Should().Be(userA.Name.ToString());
      usersRetornados[0].Login.Should().Be(userA.Login);
      usersRetornados[0].IsActive.Should().Be(userA.IsActive);
      usersRetornados[0].Password.Should().Be(userA.Password);
      usersRetornados[0].Id.Should().Be(userIdA);

      usersRetornados[1].Name.Should().Be(userB.Name.ToString());
      usersRetornados[1].Login.Should().Be(userB.Login);
      usersRetornados[1].IsActive.Should().Be(userB.IsActive);
      usersRetornados[1].Password.Should().Be(userB.Password);
      usersRetornados[1].Id.Should().Be(userIdB);
    }
  }
}
