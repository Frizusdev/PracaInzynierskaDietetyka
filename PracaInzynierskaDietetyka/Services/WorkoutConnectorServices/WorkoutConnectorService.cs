using System;
using PracaInzynierskaDietetyka.DTO.ConnectorDTOS;
using PracaInzynierskaDietetyka.DTO.WorkoutConnectorDTOS;
using PracaInzynierskaDietetyka.Repository;

namespace PracaInzynierskaDietetyka.Services.WorkoutConnectorServices
{
	public class WorkoutConnectorService : IWorkoutConnectorService
	{
        private readonly IWorkoutConnectorRepository _connector;

        public WorkoutConnectorService(IWorkoutConnectorRepository connector)
        {
            _connector = connector;
        }

        public IEnumerable<ConnectorWorkoutDTO> connector(string id, string date)
        {
            return _connector.GetByIDandDate(id, date).Select(x => new ConnectorWorkoutDTO().map(x));
        }

        public async void delete(ConnectorWorkoutDeleteDTO entity)
        {
            var exists = _connector.GetByID(entity.ID);
            if (exists != null && exists.User_ID == entity.User_ID)
            {
                _connector.Delete(new Entity.WorkoutConnector().delete(entity));
            }
        }

        public void Insert(ConnectorWorkoutInsertDTO entity)
        {
            _connector.Insert(new Entity.WorkoutConnector().insert(entity));
        }
    }
}

