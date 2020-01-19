using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.MvcClientApp.Models;

namespace ToDo.MvcClientApp.Services
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDo>> GetAsync();

        Task<ToDo> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<ToDo> AddAsync(ToDo todo);

        Task<ToDo> EditAsync(ToDo todo);
    }
}
