using FluentAssertions;
using Tests.Builders;
using System;
using Xunit;

namespace Tests.Unit.Domain.Entity
{
  public class UserTest
  {
    [Fact]
    public void should_deactivate_user()
    {
      //arrange
      var user = new UserBuilder().IsActive().ComLogin("kennedy").ComSenha("123456789").WithName("Kennedy").Construct();
      //action
      user.Deactivate();
      //assert
      user.IsActive.Should().BeFalse();
    }

    [Fact]
    public void should_update_user()
    {
      //arrange
      var userId = Guid.NewGuid();
      var user = new UserBuilder()
          .WithName("Grahl")
          .ComLogin("grahl")
          .ComSenha("123456")
          .WithId(userId)
          .IsActive()
          .Construct();

      //action
      user.Update("Darlei", "darlei", "1234567", false);

      //assert
      user.IsActive.Should().BeFalse();
      user.Login.Should().Be("darlei");
      user.Password.Should().Be("1234567");
      user.Id.Should().Be(userId);
    }
  }
}
