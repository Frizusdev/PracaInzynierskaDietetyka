﻿using System;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.DTO.DishesDTOS
{
    public class DishesDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public float Kcal { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbon { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }

        public DishesDTO map(Dishes dish)
        {
            ID = dish.ID;
            Name = dish.Name;
            Kcal = dish.Kcal;
            Protein = dish.Protein;
            Fat = dish.Fat;
            Carbon = dish.Carbon;
            Description = dish.Description;
            Photo = dish.Photo;

            return this;
        }
    }
}
