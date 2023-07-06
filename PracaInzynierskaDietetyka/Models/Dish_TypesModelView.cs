using System;
using PracaInzynierskaDietetyka.DTO.Dish_TypesDTOS;

namespace PracaInzynierskaDietetyka.Models
{
	public class Dish_TypesModelView
	{
        public int DishType_ID { get; set; }
        public string? Dish_Time { get; set; }

        public Dish_TypesModelView map(Dish_TypeDTO _Types)
        {
            DishType_ID = _Types.DishType_ID;
            Dish_Time = _Types.Dish_Time;

            return this;
        }
    }
}

