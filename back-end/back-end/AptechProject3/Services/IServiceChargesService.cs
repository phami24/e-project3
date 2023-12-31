﻿using AptechProject3.Models;

namespace AptechProject3.Services
{
    public interface IServiceChargesService : IGenericService<ServiceCharges>
    {
        Task<ServiceCharges?> GetByName(string serviceName);
    }
}
