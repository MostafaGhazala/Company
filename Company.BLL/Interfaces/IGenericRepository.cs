using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(int id);
        public void Update(T item);
        public void Delete(T item);
        public Task Add(T item);
    }
}
