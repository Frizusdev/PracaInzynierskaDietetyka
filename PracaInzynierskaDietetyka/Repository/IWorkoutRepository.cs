using System;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public interface IWorkoutRepository : IGenericRepository<Workout>
	{
		IEnumerable<Workout> top10();
        Workout GetByName(string name);
        IEnumerable<Workout> getListByName(string name);
    }
}

