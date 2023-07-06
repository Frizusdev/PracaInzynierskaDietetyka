using System;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public interface IConnectorRepository : IGenericRepository<Connector>
    {
		IEnumerable<Connector> GetByIDandDate(string User_ID, string date);
    }
}

