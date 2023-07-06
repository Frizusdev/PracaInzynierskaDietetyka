using System;
using PracaInzynierskaDietetyka.DTO.DishesDTOS;

namespace PracaInzynierskaDietetyka.Services.DishesServices
{
	public interface IDishesService 
	{
		IEnumerable<DishesDTO> dishes(string name);
        bool GetByName(string name);
        public void Insert(DishesDTO entity);
    }
}

