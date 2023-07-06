using System;
namespace PracaInzynierskaDietetyka.DTO.WorkoutConnectorDTOS
{
	public class ConnectorWorkoutInsertDTO
	{
        public string User_ID { get; set; }
        public int Exercise_ID { get; set; }
        public DateOnly Date { get; set; }
        public int Reps { get; set; }
        public int Times { get; set; }
        public string Pause_Time { get; set; }
    }
}

