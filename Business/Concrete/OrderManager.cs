﻿using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void AddNewOrder(Order order)
        {
            _orderDal.Add(order);
        }

        public List<Order> GetAllOrders()
        {
            return _orderDal.GetAll().ToList();
        }
    }
}