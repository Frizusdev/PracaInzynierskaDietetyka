using System;
using PracaInzynierskaDietetyka.Data;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public class Dish_TypesRepository : IDish_TypesRepository
    {
        private readonly ApplicationDbContext _context;

        public Dish_TypesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(Dish_Types entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dish_Types> Get5()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dish_Types> GetAll()
        {
            return _context.Set<Dish_Types>().ToList();
        }

        public Dish_Types GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Dish_Types entity)
        {
            throw new NotImplementedException();
        }
    }
}

