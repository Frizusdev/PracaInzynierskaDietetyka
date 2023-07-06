using System;

namespace PracaInzynierskaDietetyka.Models
{
	public class Connector
	{
        public IEnumerable<Dish_TypesModelView> dish_types { get; set; }
        public IEnumerable<DietModelView> diet { get; set; }
        public UserMacro macro { get; set; }
        public IEnumerable<UserMacro> userlist { get; set; }
    }
}

