using System;
using PracaInzynierskaDietetyka.DTO.ConnectorDTOS;
using PracaInzynierskaDietetyka.Models;
using PracaInzynierskaDietetyka.Repository;

namespace PracaInzynierskaDietetyka.Services.ConnectorServices
{
	public class ConnectorService : IConnectorService
	{
        private readonly IConnectorRepository _connector;

        public ConnectorService(IConnectorRepository connector)
		{
            _connector = connector;
		}

        public IEnumerable<ConnectorDTO> connector(string id, string date)
        {
            return _connector.GetByIDandDate(id, date).Select(x => new ConnectorDTO().map(x));
        }

        public async void delete(ConnectorDeleteDTO entity)
        {
            var exists = _connector.GetByID(entity.ID);
            if (exists != null && exists.User_ID == entity.User_ID)
            {
                _connector.Delete(new Entity.Connector().delete(entity));
            }
        }

        public void Insert(ConnectorInsertDTO entity)
        {
            _connector.Insert(new Entity.Connector().insert(entity));
        }
    }
}

