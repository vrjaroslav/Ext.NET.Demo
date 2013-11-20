using System.Collections.Generic;
using Uber.Core;

namespace Uber.Services
{
    public interface ICustomersService
    {
        Customer Save(Customer customer);
        void Delete(int id);
        List<Customer> GetAll();
    }
}