
using Mc2.CrudTest.Application.Common.Interface;
using Mc2.CrudTest.Application.Common.Model;

using AutoMapper;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mc2.CrudTest.Application.Services.Interface;
using Mc2.CrudTest.Application.Services.Dto;
using Mc2.CrudTest.Application.Common.Error;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;

namespace Mc2.CrudTest.Application.Services
{
    public class CustomerService : ICustomerService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerService> logger;
        private readonly IMapper mapper;
        private readonly IErrorMessageLog errorMessageLog;
        #endregion
        #region Ctor
        public CustomerService(IUnitOfWork unitOfWork, ILogger<CustomerService> logger, IMapper mapper
            , IErrorMessageLog errorMessageLog
            )
        {
            _unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
            this.errorMessageLog = errorMessageLog;
        }
        #endregion

        #region Customer

        #region Command
        public async Task<ResponseModel> UpsertAsync(CustomerDto CustomerDto)
        {
            
            var Customer = mapper.Map<Customer>(CustomerDto);
           
            try
            {
                if (Customer.Id > 0)
                {
                    _unitOfWork.Repository<Customer>().Update(Customer);

                }
                 
                else
                  await _unitOfWork.Repository<Customer>().AddAsync(Customer);

                await _unitOfWork.SaveAsync();
                return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, mapper.Map<CustomerDto>(Customer));
            }
            catch (Exception ex)
            {
                Log(nameof(UpsertAsync), ex.Message);
                logger?.LogError(ex.ToString());
                return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
            }

        }
        public async Task<ResponseModel> DeleteAsync(int id)
        {
            try
            {
                var CustomerObj = await _unitOfWork.Repository<Customer>().Get(id);
              
                if (CustomerObj != null)
                {
                    
                        CustomerObj.IsDeleted = true;
                        _unitOfWork.Repository<Customer>().Update(CustomerObj);
                        await _unitOfWork.SaveAsync();

                        return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, CustomerObj);


                  
                }
                else
                    return ResponseModel.FailureResponse("Not Found");
            }
            catch (Exception ex)
            {
                Log(nameof(DeleteAsync), ex.Message);
                logger?.LogError(ex.ToString());
                return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
            }
        }
        #endregion

        #region Queries
        public async Task<ResponseModel> GetCustomersAsync()
        {
            try
            {
                var Customers = await _unitOfWork.Repository<Customer>()
               .TableNoTracking
               .OrderBy(t => t.Id)
               .ToListAsync();
               
                var CustomersVm = mapper.Map<List<CustomerDto>>(Customers.Where(c => c.IsDeleted == false).ToList<Customer>());
                return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, CustomersVm);
            }
            catch (Exception ex)
            {
                Log(nameof(GetCustomersAsync), ex.Message);
                logger?.LogError(ex.ToString());
                return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
            }
           
        }
        public async Task<ResponseModel> GetCustomerByIdAsync(int Id)
        {
            try
            {
                var Customer = await _unitOfWork.Repository<Customer>().Get(Id);
                if (Customer != null)
                {
                    var CustomerVm = mapper.Map<CustomerDto>(Customer);
                    return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, CustomerVm);
                }
                else
                    return ResponseModel.FailureResponse("Not Found");
            }
            catch (Exception ex)
            {
                Log(nameof(DeleteAsync), ex.Message);
                logger?.LogError(ex.ToString());
                return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
            }
        }

        #endregion

        #endregion

        #region Error
        private void Log(string method, string msg)
        {
            errorMessageLog.LogError("Application", "CustomerService", method, msg);
        }


        #endregion
    }
}
