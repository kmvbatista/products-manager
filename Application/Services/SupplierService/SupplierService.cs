using Application.Models.SupplierModels;
using Application.Services.NotificationService;
using Domain.DomainNotifications;
using Domain.Entity;
using Infra.Repositories.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
  public class SupplierService : ISupplierService
  {
    private readonly ISupplierRepository _supplierRepository;
    private readonly INotificationService _notificationService;

    public SupplierService(ISupplierRepository supplierRepository, INotificationService notificationService)
    {
      _supplierRepository = supplierRepository;
      _notificationService = notificationService;
    }

    public async Task Create(SupplierRequestModel request)
    {
      var supplier = new Supplier(request.CorporateName, request.cpnj, request.TradingName,
          request.Email, request.Telephone, request.Address);
      if (supplier.Invalid)
      {
        _notificationService.AddEntityNotifications(supplier.ValidationResult);
        return;
      }
      await _supplierRepository.Create(supplier);
    }

    public async Task Delete(Guid id)
    {
      await ValidateEntityExistence(id);
      if (_notificationService.HasNotifications())
        return;
      await _supplierRepository.Delete(id);
    }

    public async Task<bool> ExistsById(Guid id)
    {
      return await _supplierRepository.ExistsById(id);
    }

    public async Task<IList<SupplierResponseModel>> GetAll()
    {
      var suppliers = await _supplierRepository.GetAll();
      if (suppliers is null)
      {
        _notificationService.AddNotification("SupplierGetAllError", "Não há fornecedores cadastrados");
        return null;
      }
      return suppliers.Select(supplier => new SupplierResponseModel
      {
        Id = supplier.Id,
        cpnj = supplier.cpnj,
        Email = supplier.Email.ToString(),
        Address = supplier.Address,
        TradingName = supplier.TradingName,
        CorporateName = supplier.CorporateName,
        Telephone = supplier.Telephone.ToString()
      }).ToList();
    }

    public async Task<IList<Supplier>> GetAllRaw()
    {
      return await _supplierRepository.GetAll();
    }

    public async Task<SupplierResponseModel> GetById(Guid id)
    {
      var supplier = await _supplierRepository.GetById(id);
      if (supplier is null)
      {
        _notificationService.AddNotification("SupplierGetByIdError", $"O fornecedor com id ${id} não foi encontrado");
        return null;
      }
      return new SupplierResponseModel()
      {
        Id = supplier.Id,
        cpnj = supplier.cpnj,
        Email = supplier.Email.ToString(),
        Address = supplier.Address,
        TradingName = supplier.TradingName,
        CorporateName = supplier.CorporateName,
        Telephone = supplier.Telephone.ToString()
      };
    }

    public async Task Update(Guid id, SupplierRequestModel request)
    {
      var supplier = await _supplierRepository.GetById(id);
      if (supplier is null)
      {
        _notificationService.AddNotification("SupplierUpdateError", "Fornecedor não encontrado");
        return;
      }
      supplier.Update(request.CorporateName, request.cpnj, request.TradingName, request.Email, request.Telephone, request.Address);
      if (supplier.Invalid)
      {
        _notificationService.AddEntityNotifications(supplier.ValidationResult);
      }
      await _supplierRepository.Update(supplier);
    }

    public async Task ValidateEntityExistence(Guid entityId)
    {
      bool supplierExists = await _supplierRepository.ValidateEntityExistence(entityId);
      if (!supplierExists)
      {
        _notificationService.AddNotification("SupplierInexistentError", $"O fornecedor com o id {entityId}");
      }
    }
  }
}
