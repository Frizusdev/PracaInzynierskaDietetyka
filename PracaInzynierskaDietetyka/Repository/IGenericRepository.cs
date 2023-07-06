using System;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public interface IGenericRepository<T> where T : class
	{
		IEnumerable<T> GetAll();
		IEnumerable<T> Get5();
		public T GetByID(int id);
		public void Insert(T entity);
		public void Delete(T entity);
    }
}

