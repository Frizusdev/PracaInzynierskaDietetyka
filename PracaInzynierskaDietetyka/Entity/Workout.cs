using System;
using System.ComponentModel.DataAnnotations;
using PracaInzynierskaDietetyka.DTO.WorkoutDTOS;

namespace PracaInzynierskaDietetyka.Entity
{
    public class Workout : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Img_path { get; set; }

        public Workout insert(ExerciseDTO exercise)
        {
            ID = exercise.ID;
            Name = exercise.Name;
            Description = exercise.Description;
            Img_path = exercise.Img_path;

            return this;
        }
    }
}

