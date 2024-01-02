using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AptechProject3.Comon;
using AptechProject3.Models;

namespace AptechProject3.Services.ServicesImpl
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Report> Create(Report entity)
        {
            try
            {
                await _unitOfWork.Reports.Add(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while creating report.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Report? deleteReport = await _unitOfWork.Reports.GetById(id);
                if (deleteReport != null)
                {
                    await _unitOfWork.Reports.Delete(deleteReport);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while deleting report.", ex);
            }
        }

        public async Task<IEnumerable<Report>> GetAll()
        {
            try
            {
                return await _unitOfWork.Reports.All();
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving reports.", ex);
            }
        }

        public async Task<Report?> GetById(int id)
        {
            try
            {
                return await _unitOfWork.Reports.GetById(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving report by ID.", ex);
            }
        }

        public async Task<Report> Update(Report entity)
        {
            try
            {
                await _unitOfWork.Reports.Update(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while updating report.", ex);
            }
        }
    }
}
