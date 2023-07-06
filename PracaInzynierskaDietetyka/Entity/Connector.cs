using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PracaInzynierskaDietetyka.DTO.ConnectorDTOS;

namespace PracaInzynierskaDietetyka.Entity
{
    public class Connector : IEntity
    {
        [Key]
        public int ID { set; get; }
        public string? User_ID { set; get; }
        [ForeignKey("Dish")]
        public int Dish_ID { set; get; }
        [ForeignKey("Dish_Types")]
        public int DishType_ID { set; get; }
        public int Weight { set; get; }
        public DateOnly Diet_Date { set; get; }

        public virtual Dishes Dish { set; get; }
        public virtual Dish_Types Dish_Types { set; get; }


        public Connector insert(ConnectorInsertDTO connectorDTO)
        {
            User_ID = connectorDTO.User_ID;
            Dish_ID = connectorDTO.Dish_ID;
            DishType_ID = connectorDTO.DishType_ID;
            Weight = connectorDTO.Weight;
            Diet_Date = connectorDTO.Diet_Date;

            return this;
        }

        public Connector delete(ConnectorDeleteDTO connectorDeleteDTO)
        {
            ID = connectorDeleteDTO.ID;

            return this;
        }
    }
}

