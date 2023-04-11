using Mc2.CrudTest.Application.Services.Dto;
using Mc2.CrudTest.Shared.Common;

namespace BlazorWeb.Client.Managers.Master
{
    public interface ICustomerManager
    {
        Task<ResponseModel<List<CustomerDto>>> GetAllAsync();
        Task<ResponseModel<CustomerDto>> SaveUpdateAsync(CustomerDto customerDto);
        Task<ResponseModel<CustomerDto>> DeleteAsync(int id);
        Task<ResponseModel<CustomerDto>> GetByIdAsync(int? id);
    }
}
