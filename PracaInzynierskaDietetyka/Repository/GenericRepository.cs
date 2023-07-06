using System;
using Microsoft.EntityFrameworkCore;
using PracaInzynierskaDietetyka.Data;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
	{
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
		{
            _context = context;
		}

        public void Delete(T entity)
        {
            try
            {
                _context.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public IEnumerable<T> Get5()
        {
            return _context.Set<T>().OrderByDescending(x => x.ID).Take(5);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetByID(int id)
        {
            var result = _context.Set<T>().FirstOrDefault(u => u.ID == id);
            _context.ChangeTracker.Clear();
            return result;
            //return _context.Set<T>().SingleOrDefault(u => u.ID == id);
        }

        public void Insert(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }
    }
}

