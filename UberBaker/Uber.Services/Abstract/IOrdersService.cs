using System;
using System.Collections.Generic;
using System.Linq;
using Uber.Core;

namespace Uber.Services
{
    public interface IOrdersService
    {
        Order Save(Order order);
        void Delete(int id);
        List<Order> GetAll();
        IQueryable<Order> GetAllByDate(DateTime startDate, DateTime endDate);
    }
}