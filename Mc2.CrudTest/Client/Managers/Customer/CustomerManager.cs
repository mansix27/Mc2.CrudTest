using Mc2.CrudTest.Application.Services.Dto;
using Mc2.CrudTest.Application.Common.Utility;
using Mc2.CrudTest.Shared.Common;
using Newtonsoft.Json;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorWeb.Client.Managers.Master
{
    public class CustomerManager : ICustomerManager
    {
        private readonly HttpClient _httpClient;
        #region EndPoints
        private string GetAll = "api/Customer/getAll";
        private string Upsert = "api/Customer/Upsert";
        private string DeleteApi = "api/Customer/DeleteCustomer";
        private string GetById = "api/Customer/GetSingleCustomer";
        #endregion
        public CustomerManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async  Task<ResponseModel<CustomerDto>> DeleteAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{DeleteApi}/{id}"); 
            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
             
                return new ResponseModel<CustomerDto>()
                {
                    Success = true,
                    Message = "Success",
                    Output = null
                };
            }
            else
                return new ResponseModel<CustomerDto>()
                {
                    Success = false,
                    Message = "API Response Error",
                    Output = null
                };
           
        }

        public async Task<ResponseModel<List<CustomerDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(GetAll);
            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<ResponseModel<List<CustomerDto>>>(responseAsString);
                return responseModel;
            }
            else
                return new ResponseModel<List<CustomerDto>>()
                {
                    Success = false,
                    Message = "API Response Error",
                    Output = null
                };

        }

        public async Task<ResponseModel<CustomerDto>> SaveUpdateAsync(CustomerDto customerDto)
        {
            var validateMobile= PublicMethod.ValidateMobileNumber(customerDto.PhoneNumber, customerDto.CountryCode);
            if (!validateMobile.IsMobile)
            {
                return new ResponseModel<CustomerDto>()
                {
                    Success = false,
                    Message = "PhoneNumber is Not Valid",
                    Output = null
                };
            }
            else
                customerDto.PhoneNumber = validateMobile.FormattedNumber;

            var response = await _httpClient.PostAsJsonAsync(Upsert, customerDto);
            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<ResponseModel<CustomerDto>>(responseAsString);
                return responseModel;
            }
            else
                return new ResponseModel<CustomerDto>()
                {
                    Success = false,
                    Message = "API Response Error",
                    Output = null
                };
        }


        public async Task<ResponseModel<CustomerDto>> GetByIdAsync(int? id)
        {
              var response = await _httpClient.GetAsync($"{GetById}/{id}"); ;
            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<ResponseModel<CustomerDto>>(responseAsString);
                return responseModel;
            }
            else
                return new ResponseModel<CustomerDto>()
                {
                    Success = false,
                    Message = "API Response Error",
                    Output = null
                };
        }
    }
}
