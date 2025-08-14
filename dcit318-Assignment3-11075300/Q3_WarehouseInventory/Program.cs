using System;
using System.Collections.Generic;
using System.Linq;

namespace Q3_WarehouseInventory
{
    // a) Custom exception
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message) { }
    }

    // b) Product class
    public class Product
    {
        public int Id;
        public string Name;
        public double Price;

        public Product(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"Product ID: {Id}, Name: {Name}, Price: {Price:C}";
        }
    }

    // c) Inventory class with constraint
    public class Inventory<T> where T : Product
    {
        private List<T> items = new();

        public void AddItem(T item) => items.Add(item);

        public T GetItemById(int id)
        {
            var item = items.FirstOrDefault(p => p.Id == id);
            if (item == null)
                throw new ItemNotFoundException($"Item with ID {id} not found.");
            return item;
        }

        public List<T> GetAllItems() => new List<T>(items);

        public bool RemoveItem(int id)
        {
            var item = items.FirstOrDefault(p => p.Id == id);
            if (item == null) return false;
            items.Remove(item);
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var inventory = new Inventory<Product>();

            // Seed products
            inventory.AddItem(new Product(1, "Laptop", 5500.00));
            inventory.AddItem(new Product(2, "Smartphone", 3500.00));
            inventory.AddItem(new Product(3, "Monitor", 1500.00));

            Console.WriteLine("=== Warehouse Inventory ===");
            foreach (var product in inventory.GetAllItems())
            {
                Console.WriteLine(product);
            }

            // Search for a product
            Console.Write("\nEnter product ID to search: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var product = inventory.GetItemById(id);
                    Console.WriteLine("Found: " + product);
                }
                catch (ItemNotFoundException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid ID input.");
            }
        }
    }
}
