using BlazorApp.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApp.Pages.OrderInfo
{
    public partial class OrderInfo
    {

        [Parameter]
        public int? Id { get; set; }

        private List<Supplier> suppliers = new List<Supplier>();
        private List<Item> items = new List<Item>();
        private Order order = new Order();
        private OrderDetail orderDetail = new OrderDetail();
        private bool isloaded;
        private bool invalidItem;
        private bool invalidQty;
        private bool invalidRate;
        private Item selecteditem;
        private int? selectedOrderDetailId;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (Id.HasValue)
                {
                    order = await _orderService.GetOrder(Id.Value);
                }
                else
                {
                    order.RefID = await _orderService.GenerateOrderRefNo();
                    order.PoDate = DateTime.Today;
                }

                suppliers = await _supplierService.GetSuppliers();
                items = await _itemService.GetItems();

                isloaded = true;
            }
            catch (Exception e)
            {
                isloaded = true;
                _toastService.ShowError("Something went wrong." + e.Message);
            }


        }
        private async Task<IEnumerable<Item>> SearchItems(string searchText)
        {
            return await Task.FromResult(items.Where(x => x.ItemName.ToLower().Contains(searchText.ToLower())).ToList());
        }


        private void AddItem()
        {
            try 
            {
                if (orderDetail.OrderDetailId == 0)
                {
                    if (selecteditem != null)
                    {
                        orderDetail.ItemId = selecteditem.ItemId;
                        orderDetail.ItemName = selecteditem.ItemName;
                    }
                    if (!ValidateOrderDetail())
                    {
                        return;
                    }
                    orderDetail.OrderDetailId = new Random().Next();
                    order.OrderDetails.Add(orderDetail);
                    _toastService.ShowSuccess("Item added successfully.");
                }
                else
                {
                    if (selecteditem == null)
                    {
                        orderDetail.ItemId = 0;
                    }
                    if (!ValidateOrderDetail())
                    {
                        return;
                    }
                    var existingOrderDetail = order.OrderDetails.FirstOrDefault(x => x.OrderDetailId == orderDetail.OrderDetailId);
                    if (existingOrderDetail != null)
                    {
                        existingOrderDetail = orderDetail;
                        existingOrderDetail.ItemId = selecteditem.ItemId;
                        existingOrderDetail.ItemName = selecteditem.ItemName;
                    }
                    else
                    {
                        order.OrderDetails.Add(orderDetail);
                    }
                    selectedOrderDetailId = null;
                    _toastService.ShowSuccess("Item updated successfully.");
                }
                order.IsOrderDetailsModified = true;
                selecteditem = null;
                orderDetail = new OrderDetail();
            }
            catch (Exception e)
            {
                _toastService.ShowError("Something went wrong." + e.Message);
            }
        }

        private void EditItem(OrderDetail item)
        {
            orderDetail = item;
            selectedOrderDetailId= orderDetail.OrderDetailId;
            selecteditem = new Item { ItemId = (int)item.ItemId, ItemName = item.ItemName };
        }

        private async Task RemoveItem(OrderDetail item)
        {
            var isConfirm = await _jsRuntime.InvokeAsync<bool>("customConfirm", item.ItemName + " item will be deleted.");

            if (isConfirm)
            {
                order.OrderDetails.Remove(item);
                order.IsOrderDetailsModified = true;
                _toastService.ShowSuccess("Item deleted successfully.");
            }
        }

        private async Task SaveOrder()
        {
            try
            {
                if (order.OrderDetails.Count == 0)
                {
                    await _jsRuntime.InvokeVoidAsync("Swal.fire", "No items added", "Please add items to the list.","info");
                    return;
                }
                if (Id.HasValue)
                {
                    await _orderService.UpdateOrder(Id.Value, order);
                    _toastService.ShowSuccess("Order updated successfully.");
                }
                else
                {
                    await _orderService.CreateOrder(order);
                    _toastService.ShowSuccess("Order saved successfully.");
                }

                _navigation.NavigateTo("/orders");
            }
            catch (Exception e)
            {
                _toastService.ShowError("Something went wrong." + e.Message);
            }


        }

        private void Close()
        {
            _navigation.NavigateTo("/orders");
        }


        private bool ValidateOrderDetail()
        {
            var isValid = true;
            invalidRate = false;
            invalidQty = false;
            invalidItem = false;
            if (orderDetail.Rate == null)
            {
                invalidRate = true;
                isValid = false;
            }
            if (orderDetail.Qty == null)
            {
                invalidQty = true;
                isValid = false;
            }
            if (orderDetail.ItemId < 1)
            {
                invalidItem = true;
                isValid = false;
            }
            return isValid;
        }

    }
}