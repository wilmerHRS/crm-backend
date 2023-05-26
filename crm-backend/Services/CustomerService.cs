using AutoMapper;
using crm_backend.Data;
using crm_backend.Entities;
using crm_backend.Models;

namespace crm_backend.Services
{
    public class CustomerService : ICustomerService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public CustomerService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener TODOS (GET)
        public List<Customer> GetAll()
        {
            List<Customer> customers = _context.Customers.Where(c => c.IsDeleted == false).ToList();
            return customers;
        }

        // Obtener con Paginación (GET)
        public Pagination<Customer> GetAllPagination(int pageNumber, int pageSize, DateTime? startDate, DateTime? endDate)
        {
            // Total de clientes
            var totalCustomers = _context.Customers.Where(c => c.IsDeleted == false).Count();


            // Total de páginas
            var totalPages = (int)Math.Ceiling(totalCustomers / (double)pageSize);

            // Número de página solicitada no sea mayor que el total de páginas
            pageNumber = Math.Min(pageNumber, totalPages);

            // Indice inicial y final
            var startIndex = (pageNumber - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize, totalCustomers);

            List<Customer> customers = _context.Customers.Where(c => c.IsDeleted == false && (!startDate.HasValue || c.CreatedAt >= startDate.Value) &&
                    (!endDate.HasValue || c.CreatedAt <= endDate.Value))
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();

            var paginationData = new Pagination<Customer>
            {
                TotalItems = totalCustomers,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                NextPage = pageNumber < totalPages ? pageNumber + 1 : (int?)null,
                PreviousPage = pageNumber > 1 ? pageNumber - 1 : (int?)null,
                Data = customers
            };

            return paginationData;
        }

        // Obtener por ID (GET)
        public Customer GetById(int id)
        {
            var customer = getCustomer(id);
            return customer;
        }


        // Crear (POST)
        public Customer Create(CreateRequest customer)
        {
            var customerData = _mapper.Map<Customer>(customer);

            _context.Customers.Add(customerData);
            _context.SaveChanges();

            return customerData;
        }


        // Actualizar (PUT)
        public Customer Update(int id, UpdateRequest customer)
        {
            var customerData = getCustomer(id);

            _mapper.Map(customer, customerData);
            _context.Customers.Update(customerData);
            _context.SaveChanges();
            return customerData;
        }

        // Eliminar (DELETE)
        public Customer Delete(int id)
        {
            var customer = getCustomer(id);
            customer.IsDeleted = true;
            _context.SaveChanges();

            return customer;
        }

        private Customer getCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null || customer.IsDeleted == true) throw new KeyNotFoundException("El Cliente no Existe");
            return customer;
        }
    }
}
