using AutoMapper;
using crm_backend.Data;
using crm_backend.Entities;
using crm_backend.Models;
using Microsoft.EntityFrameworkCore;

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
            List<Customer> customers = _context.Customers.Where(c => c.IsDeleted == false).OrderByDescending(c => c.UpdatedAt).ToList();
            return customers;
        }

        // Obtener con Paginación (GET)
        public Pagination<Customer> GetAllPagination(int pageNumber, int pageSize, string searchName, DateTime? startDate, DateTime? endDate)
        {
            // Obtén los datos totales basados en el rango de fechas
            var query = _context.Customers.AsQueryable();
            query = query.Where(c => c.IsDeleted == false);

            if (startDate.HasValue)
            {
                query = query.Where(c => c.CreatedAt.AddHours(-5) >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(c => c.CreatedAt.AddHours(-5) <= endDate.Value.AddDays(1));
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(c => c.Nombre.Contains(searchName));
            }

            // Total de clientes
            var totalCustomers = query.Count();


            // Total de páginas
            var totalPages = (int)Math.Ceiling(totalCustomers / (double)pageSize);

            // Número de página solicitada no sea mayor que el total de páginas
            pageNumber = Math.Min(pageNumber, totalPages);

            // Indice inicial y final
            var startIndex = (pageNumber - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize, totalCustomers);

            List<Customer> customers = new List<Customer>();

            if (totalCustomers > 0)
            {
                customers = query
                    .OrderByDescending(c => c.UpdatedAt)
                    .Skip(startIndex)
                    .Take(pageSize)
                    .ToList();
            }

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
