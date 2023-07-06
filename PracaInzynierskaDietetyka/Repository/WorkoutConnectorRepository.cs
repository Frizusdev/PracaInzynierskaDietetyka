using System;
using Microsoft.EntityFrameworkCore;
using PracaInzynierskaDietetyka.Data;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public class WorkoutConnectorRepository : GenericRepository<WorkoutConnector>, IWorkoutConnectorRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkoutConnectorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<WorkoutConnector> GetByIDandDate(string id, string date)
        {
            return _context.Set<WorkoutConnector>().Where(u => u.User_ID == id && u.Date == DateOnly.Parse(date)).Include(x => x.Workout);
        }
    }
}

