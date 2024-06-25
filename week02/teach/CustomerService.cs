using System;
using System.Collections.Generic;

public class CustomerService
{
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Add customer to non-full queue
        // Expected Result: Customer added to the queue
        Console.WriteLine("Test 1");
        var cs1 = new CustomerService(2);
        cs1.AddNewCustomer("John Doe", "12345", "Problem 1");
        Console.WriteLine(cs1);
        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add customer to full queue
        // Expected Result: Error message displayed
        Console.WriteLine("Test 2");
        var cs2 = new CustomerService(1);
        cs2.AddNewCustomer("Jane Doe", "67890", "Problem 2");
        cs2.AddNewCustomer("John Doe", "12345", "Problem 1");
        Console.WriteLine(cs2);
        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Serve customer from non-empty queue
        // Expected Result: Customer served and displayed
        Console.WriteLine("Test 3");
        var cs3 = new CustomerService(2);
        cs3.AddNewCustomer("Jane Doe", "67890", "Problem 2");
        cs3.ServeCustomer();
        Console.WriteLine(cs3);
        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Serve customer from empty queue
        // Expected Result: Error message displayed
        Console.WriteLine("Test 4");
        var cs4 = new CustomerService(1);
        try
        {
            cs4.ServeCustomer();
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }
        Console.WriteLine(cs4);
        // Defect(s) Found: None

        Console.WriteLine("=================");
    }

    private readonly Queue<Customer> _queue;
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        _maxSize = maxSize > 0 ? maxSize : 10;
        _queue = new Queue<Customer>();
    }

    /// /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// 

    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        public string Name { get; }
        public string AccountId { get; }
        public string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId}) : {Problem}";
        }
    }

    /// Add a new customer to the queue.
    /// 
    public void AddNewCustomer(string name, string accountId, string problem)
    {
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        var customer = new Customer(name, accountId, problem);
        _queue.Enqueue(customer);
    }

    /// 
    /// Dequeue the next customer and display the information.
    /// 
    public void ServeCustomer()
    {
        if (_queue.Count == 0)
        {
            throw new IndexOutOfRangeException("No customers in queue.");
        }

        var customer = _queue.Dequeue();
        Console.WriteLine(customer);
    }

    /// 
    /// Provide a string representation of the customer service queue object.
    /// 
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + String.Join(", ", _queue) + "]";
    }

}