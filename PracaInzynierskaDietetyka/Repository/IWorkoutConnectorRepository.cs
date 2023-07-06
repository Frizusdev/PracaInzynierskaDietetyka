using System;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public interface IWorkoutConnectorRepository : IGenericRepository<WorkoutConnector>
	{
        IEnumerable<WorkoutConnector> GetByIDandDate(string id, string date);
    }
}

