using System;
using PracaInzynierskaDietetyka.DTO.ConnectorDTOS;

namespace PracaInzynierskaDietetyka.Models
{
	public class ConnectorInsertModelView
	{
        public string? User_ID { set; get; }
        public int Dish_ID { set; get; }
        public int DishType_ID { set; get; }
        public int Weight { set; get; }
        public DateOnly Diet_Date { set; get; }
    }
}

