using System;
using PracaInzynierskaDietetyka.DTO.DishesDTOS;
using PracaInzynierskaDietetyka.DTO.WorkoutDTOS;
using PracaInzynierskaDietetyka.Entity;
using PracaInzynierskaDietetyka.Repository;

namespace PracaInzynierskaDietetyka.Services.WorkoutService
{
	public class WorkoutService : IWorkoutService
	{
        public IWorkoutRepository _workout;

        public WorkoutService(IWorkoutRepository workout)
		{
            _workout = workout;
		}

        public IEnumerable<ExerciseDTO> exercises(string name)
        {
            return _workout.getListByName(name).Select(x => new ExerciseDTO().map(x));
        }

        public IEnumerable<ExerciseDTO> exercisesTop10()
        {
            return _workout.top10().Select(x => new ExerciseDTO().map(x));
        }

        public bool GetByName(string name)
        {
            return _workout.GetByName(name) != null;
        }

        public void Insert(ExerciseDTO entity)
        {
            _workout.Insert(new Entity.Workout().insert(entity));
        }
    }
}

