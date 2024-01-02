using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AptechProject3.Comon;
using AptechProject3.Models;

namespace AptechProject3.Services.ServicesImpl
{
    public class ClientServices : IGenericService<Client>,IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Client> Create(Client entity)
        {
            try
            {
                await _unitOfWork.Clients.Add(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while creating client.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Client? deleteClient = await _unitOfWork.Clients.GetById(id);
                if (deleteClient != null)
                {
                    await _unitOfWork.Clients.Delete(deleteClient);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while deleting client.", ex);
            }
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            try
            {
                return await _unitOfWork.Clients.All();
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving clients.", ex);
            }
        }

        public async Task<Client?> GetById(int id)
        {
            try
            {
                return await _unitOfWork.Clients.GetById(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving client by ID.", ex);
            }
        }

        public async Task<Client> Update(Client entity)
        {
            try
            {
                await _unitOfWork.Clients.Update(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while updating client.", ex);
            }
        }
    }
}
