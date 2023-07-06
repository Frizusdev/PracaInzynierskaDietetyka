using System;
using System.ComponentModel.DataAnnotations;
using PracaInzynierskaDietetyka.DTO.DishesDTOS;

namespace PracaInzynierskaDietetyka.Entity
{
    public class Dishes : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public float Kcal { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbon { get; set; }

        public Dishes insert(DishesDTO dish)
        {
            ID = dish.ID;
            Name = dish.Name;
            Description = dish.Description;
            Photo = dish.Photo;
            Kcal = dish.Kcal;
            Protein = dish.Protein;
            Fat = dish.Fat;
            Carbon = dish.Carbon;

            return this;
        }

    }
}

