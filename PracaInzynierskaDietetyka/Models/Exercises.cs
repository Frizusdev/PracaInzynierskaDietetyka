using System;
using PracaInzynierskaDietetyka.DTO.WorkoutDTOS;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Models
{
	public class Exercises
	{
        public int ID { get; set; }
        public string? Name { get; set; }

        public Exercises map(ExerciseDTO work)
        {
            ID = work.ID;
            Name = work.Name;

            return this;
        }
    }
}

