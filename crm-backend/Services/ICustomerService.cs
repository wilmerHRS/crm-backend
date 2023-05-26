using crm_backend.Entities;
using crm_backend.Models;
using System.Collections.Generic;

namespace crm_backend.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        Pagination<Customer> GetAllPagination(int pageNumber, int pageSize, DateTime? startDate, DateTime? endDate);
        Customer GetById(int id);
        Customer Create(CreateRequest customer);
        Customer Update(int id, UpdateRequest customer);
        Customer Delete(int id);
    }
}
