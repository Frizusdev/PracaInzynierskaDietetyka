using System;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.DTO.Dish_TypesDTOS
{
	public class Dish_TypeDTO
	{
        public int DishType_ID { get; set; }
        public string? Dish_Time { get; set; }

		public Dish_TypeDTO map(Dish_Types _Types)
		{
			DishType_ID = _Types.DishType_ID;
			Dish_Time = _Types.Dish_Time;

			return this;
		}
	}
}

