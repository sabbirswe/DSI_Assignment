using AutoMapper;
using DSI_Assignment.Application.DTO;
using DSI_Assignment.Application.IService;
using DSI_Assignment.Domain.IRepository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Application.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all suppliers asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result contains a collection of <see cref="SupplierDto"/> representing the suppliers.</returns>
        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            try
            {
                var suppliers = await _supplierRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching suppliers");
                throw;
            }
        }
    }
}
