using System;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.DTO.ConnectorDTOS
{
	public class ConnectorDTO
    {
        public int ID { set; get; }
        public string? User_ID { set; get; }
        public int Dish_ID { set; get; }
        public string Dish_Name { set; get; }
        public int DishType_ID { set; get; }
        public string DishType_Name { set; get; }
        public int Weight { set; get; }
        public float Kcal { set; get; }
        public float Protein { set; get; }
        public float Fat { set; get; }
        public float Carbon { set; get; }

        public ConnectorDTO map(Connector connector)
        {
            ID = connector.ID;
            User_ID = connector.User_ID;
            Dish_ID = connector.Dish_ID;
            Dish_Name = connector.Dish.Name;
            DishType_Name = connector.Dish_Types.Dish_Time;
            DishType_ID = connector.DishType_ID;
            Weight = connector.Weight;
            Kcal = connector.Dish.Kcal;
            Protein = connector.Dish.Protein;
            Fat = connector.Dish.Fat;
            Carbon = connector.Dish.Carbon;

            return this;
        }
    }
}

