using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// =======================================
// a) Marker Interface for Logging
// =======================================
public interface IInventoryEntity
{
    int Id { get; }
}

// =======================================
// a) Immutable Record for Inventory Item
// =======================================
public record InventoryItem(
    int Id,
    string Name,
    int Quantity,
    DateTime DateAdded
) : IInventoryEntity;

// =======================================
// c) Generic Inventory Logger
// =======================================
public class InventoryLogger<T> where T : IInventoryEntity
{
    private readonly List<T> _log = new();
    private readonly string _filePath;

    public InventoryLogger(string filePath)
    {
        _filePath = filePath;
    }

    // Add item to the log
    public void Add(T item)
    {
        _log.Add(item);
        Console.WriteLine($"Item Added: {item}");
    }

    // Get all items in the log
    public List<T> GetAll() => new List<T>(_log);

    // Save log to file
    public void SaveToFile()
    {
        try
        {
            string json = JsonSerializer.Serialize(_log, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
            Console.WriteLine($"Data saved successfully to {_filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }

    // Load log from file
    public void LoadFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("No existing file found. Starting with an empty inventory.");
                return;
            }

            string json = File.ReadAllText(_filePath);
            var items = JsonSerializer.Deserialize<List<T>>(json);

            if (items != null)
            {
                _log.Clear();
                _log.AddRange(items);
                Console.WriteLine("Data loaded successfully from file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
    }
}

// =======================================
// f) InventoryApp Class
// =======================================
public class InventoryApp
{
    private readonly InventoryLogger<InventoryItem> _logger;

    public InventoryApp(string filePath)
    {
        _logger = new InventoryLogger<InventoryItem>(filePath);
    }

    // Add sample data
    public void SeedSampleData()
    {
        _logger.Add(new InventoryItem(1, "Laptop", 10, DateTime.Now));
        _logger.Add(new InventoryItem(2, "Mouse", 50, DateTime.Now));
        _logger.Add(new InventoryItem(3, "Keyboard", 30, DateTime.Now));
        _logger.Add(new InventoryItem(4, "Monitor", 15, DateTime.Now));
        _logger.Add(new InventoryItem(5, "Printer", 8, DateTime.Now));
    }

    public void SaveData() => _logger.SaveToFile();

    public void LoadData() => _logger.LoadFromFile();

    public void PrintAllItems()
    {
        var items = _logger.GetAll();
        if (items.Count == 0)
        {
            Console.WriteLine("No inventory items found.");
            return;
        }

        Console.WriteLine("\n=== Inventory Items ===");
        foreach (var item in items)
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded}");
        }
    }
}

// =======================================
// g) Main Application Flow
// =======================================
public class Program
{
    public static void Main()
    {
        string filePath = "inventory.json";

        // First Session
        var app = new InventoryApp(filePath);
        app.SeedSampleData();
        app.SaveData();

        // Simulate New Session
        Console.WriteLine("\n--- Simulating New Session ---");
        var newApp = new InventoryApp(filePath);
        newApp.LoadData();
        newApp.PrintAllItems();
    }
}
