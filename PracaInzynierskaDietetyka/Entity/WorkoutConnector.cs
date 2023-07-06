using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PracaInzynierskaDietetyka.DTO.ConnectorDTOS;
using PracaInzynierskaDietetyka.DTO.WorkoutConnectorDTOS;

namespace PracaInzynierskaDietetyka.Entity
{
	public class WorkoutConnector : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string User_ID { get; set; }
        [ForeignKey("Workout")]
        public int Work_ID { get; set; }
        public DateOnly Date { get; set; }
        public int Reps { get; set; }
        public int Times { get; set; }
        public string Pause_Time { get; set; }

        public virtual Workout Workout { get; set; }

        public WorkoutConnector insert(ConnectorWorkoutInsertDTO dto)
        {
            User_ID = dto.User_ID;
            Work_ID = dto.Exercise_ID;
            Date = dto.Date;
            Reps = dto.Reps;
            Times = dto.Times;
            Pause_Time = dto.Pause_Time;

            return this;
        }

        public WorkoutConnector delete(ConnectorWorkoutDeleteDTO connectorDeleteDTO)
        {
            ID = connectorDeleteDTO.ID;

            return this;
        }
    }
}

