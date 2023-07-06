using System;
using PracaInzynierskaDietetyka.DTO.ConnectorDTOS;

namespace PracaInzynierskaDietetyka.Models
{
    public class DietModelView
    {
        public int ID { get; set; }
        public int Dish_ID { set; get; }
        public string Dish_Name { set; get; }
        public int DishType_ID { set; get; }
        public string DishType_Name { set; get; }
        public int Weight { set; get; }
        public float Kcal { set; get; }
        public float Protein { set; get; }
        public float Fat { set; get; }
        public float Carbon { set; get; }

        public DietModelView get(ConnectorDTO DTO)
		{
            ID = DTO.ID;
            Dish_ID = DTO.Dish_ID;
            Dish_Name = DTO.Dish_Name;
            DishType_ID = DTO.DishType_ID;
            DishType_Name = DTO.DishType_Name;
            Weight = DTO.Weight;
            Kcal = DTO.Kcal;
            Protein = DTO.Protein;
            Fat = DTO.Fat;
            Carbon = DTO.Carbon;

            return this;
		}
	}
}

