using BlazorApp.IService;
using BlazorApp.Model;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApp.Pages.Orders
{
    public partial class Orders
    {
        private List<Order> orders = new List<Order>();
        private int pageSize = 5;
        private int pageNumber = 1;
        private int totalOrders;
        private int totalPages;
        private string searchTerm="";
        private bool isloaded;

        protected override async Task OnInitializedAsync()
        {
            await LoadOrders();
        }

        private async Task LoadOrders()
        {
            try
            {
                var response = await _orderService.GetOrders(pageSize, pageNumber, searchTerm);
                orders = response.Data;
                totalOrders = response.TotalRows;
                totalPages = (int)Math.Ceiling((double)totalOrders / pageSize);
                isloaded = true;
            }
            catch (Exception e)
            {
                isloaded = true;
                _toastService.ShowError("Something went wrong. " + e.Message);
            }
        }

        private async Task SearchOrders(ChangeEventArgs e)
        {
            searchTerm = e.Value.ToString();
            pageNumber = 1;
            await LoadOrders();
        }

        private void CreateOrder()
        {
            _navigation.NavigateTo("/orders/create");
        }

        private void EditOrder(int id)
        {
            _navigation.NavigateTo($"/orders/edit/{id}");
        }

        private async Task DeleteOrder(int id, string poNo)
        {
            try
            {
                var isConfirm = await _jsRuntime.InvokeAsync<bool>("customConfirm", "PO. NO: " + poNo + " order will be deleted.");

                if (isConfirm)
                {
                    await _orderService.DeleteOrder(id);
                    await LoadOrders();
                    _toastService.ShowSuccess("Order deleted successfully.");
                }
            }
            catch (Exception e)
            {
                _toastService.ShowError("Something went wrong." + e.Message);
            }

        }


        private async Task NextPage()
        {
            if (CanNavigateToNextPage)
            {
                pageNumber++;
                await LoadOrders();
            }
        }

        private async Task PreviousPage()
        {
            if (CanNavigateToPreviousPage)
            {
                pageNumber--;
                await LoadOrders();
            }
        }

        private async Task NavigateToPage(int page)
        {
            pageNumber = page;
            await LoadOrders();
        }

        private bool CanNavigateToPreviousPage => pageNumber > 1;
        private bool CanNavigateToNextPage => pageNumber < totalPages;
    }
}