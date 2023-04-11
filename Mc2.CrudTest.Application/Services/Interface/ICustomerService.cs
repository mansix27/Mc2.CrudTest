using Mc2.CrudTest.Application.Common.Model;
using Mc2.CrudTest.Application.Services.Dto;

namespace Mc2.CrudTest.Application.Services.Interface
{
    public interface ICustomerService
    {
        Task<ResponseModel> GetCustomersAsync();
        Task<ResponseModel> GetCustomerByIdAsync(int Id);
        Task<ResponseModel> UpsertAsync(CustomerDto appSetting);
        Task<ResponseModel> DeleteAsync(int id);
    }
}
