using AutoMapper;
using DSI_Assignment.Application.DTO;
using DSI_Assignment.Application.IService;
using DSI_Assignment.Domain.Entities;
using DSI_Assignment.Domain.IRepository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Application.Service
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all items asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result contains a collection of <see cref="ItemDto"/> representing the items.</returns>
        public async Task<IEnumerable<ItemDto>> GetAllAsync()
        {          
            try
            {
                var items = await _itemRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ItemDto>>(items);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching items");
                throw;
            }
        }
    }
}
