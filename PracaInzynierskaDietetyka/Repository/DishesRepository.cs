using System;
using PracaInzynierskaDietetyka.Data;
using PracaInzynierskaDietetyka.Entity;
using PracaInzynierskaDietetyka.Repository;

namespace PracaInzynierskaDietetyka.Repository
{
	public class DishesRepository : GenericRepository<Dishes>, IDishesRepository
	{
        private readonly ApplicationDbContext _context;

        public DishesRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }

        public Dishes GetByName(string name)
        {
            return _context.Set<Dishes>().SingleOrDefault(u => u.Name == name);
        }

        public List<Dishes> GetDishByName(string name)
        {
            return _context.Set<Dishes>().Where(u => u.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}

