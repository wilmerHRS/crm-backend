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
