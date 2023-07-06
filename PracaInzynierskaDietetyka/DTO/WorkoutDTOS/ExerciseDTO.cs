using System;
using System.ComponentModel.DataAnnotations;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.DTO.WorkoutDTOS
{
    public class ExerciseDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Img_path { get; set; }

        public ExerciseDTO map(Workout work)
        {
            ID = work.ID;
            Name = work.Name;
            Description = work.Description;
            Img_path = work.Img_path;

            return this;
        }
    }
}

