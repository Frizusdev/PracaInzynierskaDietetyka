using System;
using PracaInzynierskaDietetyka.DTO.Dish_TypesDTOS;

namespace PracaInzynierskaDietetyka.Services.Dish_TypesSerivces
{
	public interface IDish_TypesService
	{
		IEnumerable<Dish_TypeDTO> types();
	}
}

