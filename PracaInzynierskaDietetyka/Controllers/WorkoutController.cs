using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using PracaInzynierskaDietetyka.DTO.UserDataDTOS;
using PracaInzynierskaDietetyka.DTO.WorkoutConnectorDTOS;
using PracaInzynierskaDietetyka.Models;
using PracaInzynierskaDietetyka.Services.UserDataServices;
using PracaInzynierskaDietetyka.Services.WorkoutConnectorServices;
using PracaInzynierskaDietetyka.Services.WorkoutService;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PracaInzynierskaDietetyka.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _exercise;
        private readonly IWorkoutConnectorService _workout;
        private readonly IUserDataService _user;

        public IConfiguration Configuration { get; }
        public WorkoutController(IConfiguration config, IWorkoutService exercise, IWorkoutConnectorService workout, IUserDataService user)
        {
            Configuration = config;
            _exercise = exercise;
            _workout = workout;
            _user = user;
        }

        public IActionResult WorkOutPage()
        {
            var usergrid = new WorkoutConnector();

            string user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string result = DateOnly.FromDateTime(DateTime.Now).ToString();

            if (User.IsInRole("Dietetyk"))
            {
                usergrid.exercises = _workout.connector(user, result).Select(u => new ConnectorExerciseViewModel().map(u));
                usergrid.userlist = _user.getByIdDietetyk(user).Select(u => new UserMacro().GetDietUserMacro(u));
                usergrid.macro = new UserMacro().GetDietUserMacro(_user.getById(User.FindFirstValue(ClaimTypes.NameIdentifier)));

                return View("Index", usergrid);
            }
            else if (User.IsInRole("Member"))
            {
                usergrid.exercises = _workout.connector(user, result).Select(u => new ConnectorExerciseViewModel().map(u));
                usergrid.macro = new UserMacro().GetDietUserMacro(_user.getById(User.FindFirstValue(ClaimTypes.NameIdentifier)));

                return View("Index", usergrid);
            }
            return View("Index");
        }


        [HttpGet]
        public IActionResult GetWorkoutGrid(string date, string email)
        {
            string user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usergrid = new WorkoutConnector();

            if(email != null)
            {
                var guid = _user.getGuidByEmail(email);
                usergrid.exercises = _workout.connector(guid, date).Select(u => new ConnectorExerciseViewModel().map(u));
                usergrid.macro = new UserMacro().GetDietUserMacro(_user.getById(guid));
            }
            else
            {
                usergrid.exercises = _workout.connector(user, date).Select(u => new ConnectorExerciseViewModel().map(u));
                usergrid.macro = new UserMacro().GetDietUserMacro(_user.getById(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            }
            return PartialView("_PartialWorkout", usergrid);
        }

        [HttpPost]
        public IActionResult AddExercise(string email, string date, string exercise_id, int reps, int times, string pause)
        {
            var usergrid = new ConnectorWorkoutInsertDTO();
            DateOnly.TryParse(date, out DateOnly result);

            if(email != null)
            {
            var guid = _user.getGuidByEmail(email);

            usergrid.User_ID = guid;
            usergrid.Exercise_ID = Convert.ToInt32(exercise_id);
            usergrid.Date = result;
            usergrid.Reps = reps;
            usergrid.Times = times;
            usergrid.Pause_Time = pause;
                try
                {
                _workout.Insert(usergrid);
                }
                catch (Exception e)
                {
                Console.WriteLine($"Generic Exception Handler: {e}");
                }

            }
            else
            {
                string user = User.FindFirstValue(ClaimTypes.NameIdentifier);

                usergrid.User_ID = user;
                usergrid.Exercise_ID = Convert.ToInt32(exercise_id);
                usergrid.Date = result;
                usergrid.Reps = reps;
                usergrid.Times = times;
                usergrid.Pause_Time = pause;
                try
                {
                    _workout.Insert(usergrid);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Generic Exception Handler: {e}");
                }
            }
            return RedirectToAction("GetWorkoutGrid", new { date = date, email = email });
        }

        [HttpGet]
        public IEnumerable<Exercises> GetExercises(string data)
        {
            var exercises = new List<Exercises>();

            exercises = _exercise.exercises(data).Select(u => new Exercises().map(u)).ToList(); 

            return exercises;
        }

        [HttpPost]
        public void AddNewPersonToDietetyk(string email)
        {
            var guid = _user.getGuidByEmail(email);
            var addNewPerson = new AddUserToDietetykDTO();

            try
            {
                addNewPerson.Dietetyk_ID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                addNewPerson.GUID = guid;

                _user.AddPersonToDietetyk(addNewPerson);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Generic Exception Handler: {e}");
            }
        }

        [HttpGet]
        public UserDataModelView UserDataDetails(string email)
        {
            var guid = _user.getGuidByEmail(email);

            return new UserDataModelView().map(_user.mapUserData(guid));
        }

        [HttpPost]
        public void ChangePersonData(string email, string datatype, double data)
        {
            var guid = _user.getGuidByEmail(email);
            var addNewPerson = new ChangeUserMacro();

            try
            {
                switch (datatype)
                {
                    case "kcal":
                        addNewPerson.Kcal = data;
                        addNewPerson.GUID = guid;
                        addNewPerson.Protein = null;
                        addNewPerson.Fat = null;
                        addNewPerson.Carbon = null;

                        _user.ChangeUserMacro(addNewPerson);
                        break;
                    case "prot":
                        addNewPerson.Protein = data;
                        addNewPerson.GUID = guid;
                        addNewPerson.Kcal = null;
                        addNewPerson.Fat = null;
                        addNewPerson.Carbon = null;

                        _user.ChangeUserMacro(addNewPerson);
                        break;
                    case "fat":
                        addNewPerson.Fat = data;
                        addNewPerson.GUID = guid;
                        addNewPerson.Kcal = null;
                        addNewPerson.Protein = null;
                        addNewPerson.Carbon = null;

                        _user.ChangeUserMacro(addNewPerson);
                        break;
                    case "carb":
                        addNewPerson.Carbon = data;
                        addNewPerson.GUID = guid;
                        addNewPerson.Kcal = null;
                        addNewPerson.Protein = null;
                        addNewPerson.Fat = null;

                        _user.ChangeUserMacro(addNewPerson);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Generic Exception Handler: {e}");
            }
        }
    }
}
