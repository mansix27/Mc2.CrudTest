using BlazorWeb.Client.Managers.Master;
using Mc2.CrudTest.Application.Services.Dto;
using Mc2.CrudTest.Application.Services.Interface;
using Mc2.CrudTest.Shared.Common;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Front.Pages.Customer
{
    [Route("/Customer/CustomerList")]
    public partial class CustomerList
    {
        [Inject] NavigationManager Navigator { get; set; }
        [Inject] ICustomerManager customManager { get; set; }

        protected CustomerDto customerDto = new();
        protected List<CustomerDto> customerDtoList = new();
        private string warninngMessage = "";
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadCustomers();
            }
            catch (Exception ex)
            {
                warninngMessage = ex.Message;
            }
        }

        protected async Task LoadCustomers()
        {
            try
            {
                var response = await customManager.GetAllAsync();
                if (response.Success)
                    customerDtoList = response.Output;
                else
                    warninngMessage = response.Message;

                StateHasChanged();
            }
            catch (Exception ex)
            {
                warninngMessage = ex.Message;
            }

        }

        void CreateCustomer()
        {
            Navigator.NavigateTo("/CustomerForm");
        }
        async void OnDeleteClick(CustomerDto customerDto)
        {
            var response = await customManager.DeleteAsync(customerDto.Id);
            if (response.Success)
            {
                await LoadCustomers();
                warninngMessage = "Operation Succeeded";
                //StateHasChanged();
            }
            else
                warninngMessage = "Operation Faild";
        }
        async void OnEditClick(CustomerDto customerDto)
        {
            Navigator.NavigateTo($"/CustomerForm/{customerDto.Id}");
        }

    }
}
