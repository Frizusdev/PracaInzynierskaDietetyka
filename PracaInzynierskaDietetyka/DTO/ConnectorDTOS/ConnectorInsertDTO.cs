using System;
namespace PracaInzynierskaDietetyka.DTO.ConnectorDTOS
{
	public class ConnectorInsertDTO
    {
        public string? User_ID { set; get; }
        public int Dish_ID { set; get; }
        public int DishType_ID { set; get; }
        public int Weight { set; get; }
        public DateOnly Diet_Date { set; get; }
    }
}

