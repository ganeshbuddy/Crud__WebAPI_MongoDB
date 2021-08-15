using CrudMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMongo.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;
        private readonly EmployeeDatabaseSettings _settings;
        public EmployeeService(IOptions<EmployeeDatabaseSettings> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);

            _employees = database.GetCollection<Employee>(_settings.EmployeesCollectionName);

        }

        async Task<Employee> IEmployeeService.CreateAsync(Employee emp)
        {
            await _employees.InsertOneAsync(emp);
            return emp;
        }

        async Task IEmployeeService.DeleteAsync(string id)
        {
            await _employees.DeleteOneAsync(c => c.id == id);
        }

        async Task<List<Employee>> IEmployeeService.GetAllAsync()
        {
            return await _employees.Find(c => true).ToListAsync();
        }

        async Task<Employee> IEmployeeService.GetByIdAsync(string id)
        {
            return await _employees.Find<Employee>(c => c.id == id).FirstOrDefaultAsync();
        }

        async Task IEmployeeService.UpdateAsync(string id, Employee emp)
        {
            await _employees.ReplaceOneAsync(c => c.id == id, emp);
        }
    }
}
