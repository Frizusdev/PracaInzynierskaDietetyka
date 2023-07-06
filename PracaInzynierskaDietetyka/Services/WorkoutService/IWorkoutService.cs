using System;
using PracaInzynierskaDietetyka.DTO.DishesDTOS;
using PracaInzynierskaDietetyka.DTO.WorkoutDTOS;

namespace PracaInzynierskaDietetyka.Services.WorkoutService
{
	public interface IWorkoutService
	{
        IEnumerable<ExerciseDTO> exercises(string name);
        IEnumerable<ExerciseDTO> exercisesTop10();
        bool GetByName(string name);
        public void Insert(ExerciseDTO entity);
    }
}

