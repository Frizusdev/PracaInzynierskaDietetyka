using System;
using PracaInzynierskaDietetyka.DTO.Dish_TypesDTOS;
using PracaInzynierskaDietetyka.Repository;

namespace PracaInzynierskaDietetyka.Services.Dish_TypesSerivces
{
	public class Dish_TypesService : IDish_TypesService
    {
        private readonly IDish_TypesRepository _dishes;

        public Dish_TypesService(IDish_TypesRepository dishes)
        {
            _dishes = dishes;
        }

        public IEnumerable<Dish_TypeDTO> types()
        {
            return _dishes.GetAll().Select(u => new Dish_TypeDTO().map(u));
        }
    }
}

