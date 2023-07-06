using System;
using PracaInzynierskaDietetyka.DTO.DishesDTOS;
using PracaInzynierskaDietetyka.Repository;

namespace PracaInzynierskaDietetyka.Services.DishesServices
{
    public class DishesService : IDishesService
    {
        public DishesService(IDishesRepository dishes)
        {
            Dishes = dishes;
        }

        public IDishesRepository Dishes { get; }

        public IEnumerable<DishesDTO> dishes(string name)
        {
            return Dishes.GetDishByName(name).Select(u => new DishesDTO().map(u));
        }

        public bool GetByName(string name)
        {
            return Dishes.GetByName(name) != null;
        }

        public void Insert(DishesDTO entity)
        {
            Dishes.Insert(new Entity.Dishes().insert(entity));
        }
    }
}

