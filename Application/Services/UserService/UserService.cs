using Application.Models;
using Application.Services.NotificationService;
using Domain.Entity;
using Infra.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
  public class UserService
      : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly INotificationService _notificationService;

    public UserService(IUserRepository userRepository, INotificationService notificationService)
    {
      _userRepository = userRepository;
      _notificationService = notificationService;
    }

    public async Task Create(UserRequestModel request)
    {
      var user = new User(request.Name, request.Login, request.Password, request.IsActive);
      if (user.Invalid)
      {
        _notificationService.AddEntityNotifications(user.ValidationResult);
        return;
      }
      await _userRepository.Create(user);
    }

    public async Task Delete(Guid id)
    {
      var user = await _userRepository.GetById(id);
      if (user is null)
      {
        _notificationService.AddNotification("UserDeleteError", $"O usuário com id {id} não foi encontras");
      }
      user.Deactivate();
      await _userRepository.Update(user);
    }

    public async Task<IList<UserResponseModel>> GetAll()
    {
      var users = await _userRepository.GetAll();
      if (users is null)
      {
        _notificationService.AddNotification("UserGetAllError", "Não há usuários cadastrados");
      }
      return users.Select(d => new UserResponseModel
      {
        IsActive = d.IsActive,
        Id = d.Id,
        Login = d.Login,
        Name = d.Name.ToString(),
        Password = d.Password
      }).ToList();
    }

    public async Task<UserResponseModel> GetById(Guid id)
    {
      var user = await _userRepository.GetById(id);
      if (user is null)
      {
        _notificationService.AddNotification("UserGetByIdError", $"Não foi encontrado usuário com id {id}");
      }
      return new UserResponseModel()
      {
        IsActive = user.IsActive,
        Id = user.Id,
        Login = user.Login,
        Name = user.Name.ToString(),
        Password = user.Password
      };
    }

    public async Task Update(Guid id, UserRequestModel request)
    {
      var user = await _userRepository.GetById(id);
      user.Update(request.Name, request.Login, request.Password, request.IsActive);
      if (user.Invalid)
      {
        _notificationService.AddEntityNotifications(user.ValidationResult);
        return;
      }
      await _userRepository.Update(user);
    }

    public async Task ValidateEntityExistence(Guid entityId)
    {
      await _userRepository.ValidateEntityExistence(entityId);
    }
  }
}
