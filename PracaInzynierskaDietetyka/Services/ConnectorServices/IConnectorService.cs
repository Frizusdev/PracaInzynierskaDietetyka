using System;
using PracaInzynierskaDietetyka.DTO.ConnectorDTOS;

namespace PracaInzynierskaDietetyka.Services.ConnectorServices
{
	public interface IConnectorService
    {
        IEnumerable<ConnectorDTO> connector(string id, string date);
        void delete(ConnectorDeleteDTO entity);
        void Insert(ConnectorInsertDTO entity); 
    }
}

