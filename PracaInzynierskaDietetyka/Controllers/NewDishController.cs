using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracaInzynierskaDietetyka.DTO.DishesDTOS;
using PracaInzynierskaDietetyka.DTO.WorkoutDTOS;
using PracaInzynierskaDietetyka.Services.DishesServices;
using PracaInzynierskaDietetyka.Services.WorkoutService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PracaInzynierskaDietetyka.Controllers
{
    public class NewDishController : Controller
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment;
        private readonly IDishesService _dishes;
        private readonly IWorkoutService _exercise;

        public NewDishController(IConfiguration config, IWebHostEnvironment _environment, IDishesService dishes, IWorkoutService exercise)
        {
            Configuration = config;
            Environment = _environment;
            _dishes = dishes;
            _exercise = exercise;
        }

        public IActionResult NewDishPage()
        {
            return View("Index");
        }


        [HttpPost]
        public IActionResult AddNewDish(string name, string desc, string kcal, string protein, string fat, string carbon, string weight, List<IFormFile> postedFiles)
        {
            var dish = new DishesDTO();
            
            name = name.Replace("_", " ");
            desc = desc.Replace("_", " ");
            var g = float.Parse(weight, CultureInfo.InstalledUICulture.NumberFormat);
            string fileNamePath = "";

            if (postedFiles.Count() > 0)
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;

                string path = Path.Combine(this.Environment.WebRootPath, "img/dishimg");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                List<string> uploadedFiles = new List<string>();

                foreach (IFormFile postedFile in postedFiles)
                {
                    try
                    {
                        string fileName = Path.GetFileName(postedFile.FileName);
                        if (Path.Exists(path + "/" + fileName))
                        {
                            fileName = getNextFileName(path, fileName);
                        }

                        using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                        {
                            postedFile.FileName.Replace(" ", "_");
                            if (postedFiles.Count() == 1)
                            {
                                fileNamePath += fileName;
                                postedFile.CopyTo(stream);
                            }
                            else
                            {
                                if (postedFile.FileName == postedFiles.Last().FileName)
                                {
                                    fileNamePath += fileName;
                                    postedFile.CopyTo(stream);
                                }
                                else
                                {
                                    fileNamePath += fileName + ",";
                                    postedFile.CopyTo(stream);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Generic Exception Handler: {e}");
                    }
                }

                dish.Name = name;
                dish.Description = desc;
                dish.Photo = fileNamePath;
                dish.Kcal = float.Parse(kcal, CultureInfo.InvariantCulture.NumberFormat) / g;
                dish.Protein = float.Parse(protein, CultureInfo.InvariantCulture.NumberFormat) / g;
                dish.Fat = float.Parse(fat, CultureInfo.InvariantCulture.NumberFormat) / g;
                dish.Carbon = float.Parse(carbon, CultureInfo.InvariantCulture.NumberFormat) / g;

                _dishes.Insert(dish);
            }
            else
            {
                dish.Name = name;
                dish.Description = desc;
                dish.Photo = null;
                dish.Kcal = float.Parse(kcal, CultureInfo.InvariantCulture.NumberFormat) / g;
                dish.Protein = float.Parse(protein, CultureInfo.InvariantCulture.NumberFormat) / g;
                dish.Fat = float.Parse(fat, CultureInfo.InvariantCulture.NumberFormat) / g;
                dish.Carbon = float.Parse(carbon, CultureInfo.InvariantCulture.NumberFormat) / g;

                _dishes.Insert(dish);
            }
            return RedirectToAction("NewDishPage");
        }

        [HttpPost]
        public IActionResult AddNewExercise(string name, string desc, IFormFile postedFiles)
        {
            var exercise = new ExerciseDTO();
            name = name.Replace("_", " ");
            desc = desc.Replace("_", " ");

            if (postedFiles != null)
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;

                string path = Path.Combine(this.Environment.WebRootPath, "img/workoutimg");
                string fileName = Path.GetFileName(postedFiles.FileName);
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    if (Path.Exists(path + "/" + fileName))
                    {
                        fileName = getNextFileName(path, fileName);
                    }

                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        postedFiles.CopyTo(stream);
                        ViewBag.Message += fileName + ",";
                    }

                    exercise.ID = 0;
                    exercise.Name = name;
                    exercise.Description = desc;
                    exercise.Img_path = fileName;

                    _exercise.Insert(exercise);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Generic Exception Handler: {e}");
                }
            }
            else
            {
                exercise.ID = 0;
                exercise.Name = name;
                exercise.Description = desc;
                exercise.Img_path = null;

                _exercise.Insert(exercise);
            }

            return RedirectToAction("NewDishPage");
        }

        private string getNextFileName(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);

            int i = 0;
            while (Path.Exists(path + "/" + fileName))
            {
                if (i == 0)
                    fileName = fileName.Replace(extension, "(" + ++i + ")" + extension);
                else
                    fileName = fileName.Replace("(" + i + ")" + extension, "(" + ++i + ")" + extension);
            }

            return fileName;
        }

        [HttpGet]
        public bool CheckIfDishNameAlreadyExist(string name)
        {
            return _dishes.GetByName(name);
        }

        [HttpGet]
        public bool CheckIfExerciseNameAlreadyExist(string name)
        {
            return _exercise.GetByName(name);
        }
    }
}

