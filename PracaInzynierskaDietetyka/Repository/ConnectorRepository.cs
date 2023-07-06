using System;
using Microsoft.EntityFrameworkCore;
using PracaInzynierskaDietetyka.Data;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public class ConnectorRepository : GenericRepository<Connector>, IConnectorRepository
    {
        private readonly ApplicationDbContext _context;

        public ConnectorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Connector> GetByIDandDate(string id, string date)
        {
            return _context.Set<Connector>().Where(u => u.User_ID == id && u.Diet_Date == DateOnly.Parse(date)).Include(x => x.Dish).Include(u => u.Dish_Types);
        }
    }
}

