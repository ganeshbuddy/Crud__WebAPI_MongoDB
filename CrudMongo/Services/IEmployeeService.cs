using CrudMongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMongo.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(string id);
        Task<Employee> CreateAsync(Employee emp);
        Task UpdateAsync(string id, Employee emp);
        Task DeleteAsync(string id);
    }
}
