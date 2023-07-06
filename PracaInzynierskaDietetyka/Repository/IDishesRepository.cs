using System;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public interface IDishesRepository : IGenericRepository<Dishes>
    {
        List<Dishes> GetDishByName(string name);
        Dishes GetByName(string name);
    }
}

