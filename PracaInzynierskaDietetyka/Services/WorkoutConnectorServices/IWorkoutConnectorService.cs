using System;
using PracaInzynierskaDietetyka.DTO.WorkoutConnectorDTOS;

namespace PracaInzynierskaDietetyka.Services.WorkoutConnectorServices
{
	public interface IWorkoutConnectorService
	{
        IEnumerable<ConnectorWorkoutDTO> connector(string id, string date);
        void delete(ConnectorWorkoutDeleteDTO entity);
        void Insert(ConnectorWorkoutInsertDTO entity);
    }
}

