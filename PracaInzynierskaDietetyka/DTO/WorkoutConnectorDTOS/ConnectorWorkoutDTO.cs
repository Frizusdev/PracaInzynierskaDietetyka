using System;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.DTO.WorkoutConnectorDTOS
{
	public class ConnectorWorkoutDTO
    {
        public int ID { get; set; }
        public string User_ID { get; set; }
        public int Exercise_ID { get; set; }
        public string Exercise_Name { get; set; }
        public string Img_Path { get; set; }
        public DateOnly Date { get; set; }
        public int Reps { get; set; }
        public int Times { get; set; }
        public string Pause_Time { get; set; }

        public ConnectorWorkoutDTO map(WorkoutConnector workout)
        {
            ID = workout.ID;
            Exercise_ID = workout.Work_ID;
            Exercise_Name = workout.Workout.Name;
            Img_Path = workout.Workout.Img_path;
            Date = workout.Date;
            Reps = workout.Reps;
            Times = workout.Times;
            Pause_Time = workout.Pause_Time;

            return this;
        }
    }
}

