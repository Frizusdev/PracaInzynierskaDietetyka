using System;
using PracaInzynierskaDietetyka.DTO.WorkoutConnectorDTOS;

namespace PracaInzynierskaDietetyka.Models
{
	public class ConnectorExerciseViewModel
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

        public ConnectorExerciseViewModel map(ConnectorWorkoutDTO workout)
        {
            ID = workout.ID;
            Exercise_ID = workout.Exercise_ID;
            Exercise_Name = workout.Exercise_Name;
            Img_Path = workout.Img_Path;
            Date = workout.Date;
            Reps = workout.Reps;
            Times = workout.Times;
            Pause_Time = workout.Pause_Time;

            return this;
        }
    }
}

