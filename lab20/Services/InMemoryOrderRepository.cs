using System;
using System.Collections.Generic;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<int, Order> _orders = new();

    public void Save(Order order)
    {
        Console.WriteLine("Saving order to database (InMemory)...");
        _orders[order.Id] = order;
    }

    public Order GetById(int id)
    {
        return _orders.ContainsKey(id) ? _orders[id] : null;
    }
}