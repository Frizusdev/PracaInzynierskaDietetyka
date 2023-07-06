using System;
using System.ComponentModel.DataAnnotations;

namespace PracaInzynierskaDietetyka.Entity
{
    public class Dish_Types
    {
        [Key]
        public int DishType_ID { get; set; }
        public string? Dish_Time { get; set; }
    }
}

