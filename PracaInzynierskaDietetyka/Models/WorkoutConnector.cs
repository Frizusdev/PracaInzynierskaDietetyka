using System;
namespace PracaInzynierskaDietetyka.Models
{
	public class WorkoutConnector
	{
		public IEnumerable<ConnectorExerciseViewModel> exercises { get; set; }
        public IEnumerable<UserMacro> userlist { get; set; }
        public UserMacro macro { get; set; }
    }
}

