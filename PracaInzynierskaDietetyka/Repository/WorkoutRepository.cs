using System;
using PracaInzynierskaDietetyka.Data;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public class WorkoutRepository : GenericRepository<Workout>, IWorkoutRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkoutRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Workout GetByName(string name)
        {
            return _context.Set<Workout>().SingleOrDefault(u => u.Name == name);
        }

        public IEnumerable<Workout> getListByName(string name)
        {
            return _context.Set<Workout>().Where(u => u.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public IEnumerable<Workout> top10()
        {
            return _context.Set<Workout>().Where(x => x.ID <= 10);
        }
    }
}

