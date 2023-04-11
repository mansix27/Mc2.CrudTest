using AutoMapper;
using Azure;
using BlazorWeb.Client.Managers.Master;
using Mc2.CrudTest.Application.Common.Utility;
using Mc2.CrudTest.Application.Services;
using Mc2.CrudTest.Application.Services.Dto;
using Microsoft.AspNetCore.Components;

namespace Mc2.CrudTest.Presentation.Front.Pages.Customer
{
    public partial class CustomerForm
    {
        [Inject] NavigationManager Navigator { get; set; }
        [Inject] ICustomerManager customManager { get; set; }

        protected CustomerDto customerDto = new();

        protected string[] region = { "IR", "US", "Pk" };

        [Parameter]
        public int? Id { get; set; }
        string btnTxt = string.Empty;
        private string warninngMessage = "";
        protected override async Task OnInitializedAsync()
        {
            warninngMessage = "";
            btnTxt = Id == null ? "save" : "update";

        }
        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                var response = await customManager.GetByIdAsync(Id);
                if (response.Success)
                    customerDto = response.Output;
                else
                    warninngMessage = response.Message;
            }
        }

        async void OnSaveSubmitClick()
        {
            warninngMessage = "";
         var isValidEmail = PublicMethod.isValidEmail(customerDto.Email);
          var IsValidBankAccNum = PublicMethod.isValidBankAccountNumber(customerDto.BankAccountNumber);
            if (!isValidEmail)
            {
                warninngMessage = "Email is not valid";
            }
            else if(!IsValidBankAccNum)
            {
                warninngMessage = "Bank Account Number is not valid";
            }
            else
            {
              var result=  await customManager.SaveUpdateAsync(customerDto);
                if (result.Success)
                {
                    warninngMessage = "Operation Succeeded";
                    Navigator.NavigateTo($"/CustomerList");
                }
                else
                    warninngMessage = result.Message;
            }
            StateHasChanged();
        }

    }

}
