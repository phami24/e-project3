﻿using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.Models;

namespace AptechProject3.Repository.Impl
{
    public class ClientServiceRepository : GenericRepository<ClientService>, IClientServiceRepository
    {
        public ClientServiceRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}