using System;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PracaInzynierskaDietetyka.DTO.ConnectorDTOS;
using PracaInzynierskaDietetyka.DTO.UserDataDTOS;
using PracaInzynierskaDietetyka.Models;
using PracaInzynierskaDietetyka.Services.ConnectorServices;
using PracaInzynierskaDietetyka.Services.Dish_TypesSerivces;
using PracaInzynierskaDietetyka.Services.DishesServices;
using PracaInzynierskaDietetyka.Services.UserDataServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PracaInzynierskaDietetyka.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConnectorService _connector;
    private readonly IDish_TypesService _dish_Types;
    private readonly IDishesService _dishes;
    private readonly IUserDataService _user;

    public HomeController(ILogger<HomeController> logger, IConnectorService connector, IDish_TypesService dish_Types, IDishesService dishes, IUserDataService user)
    {
        _logger = logger;
        _connector = connector;
        _dish_Types = dish_Types;
        _dishes = dishes;
        _user = user;
    }

    public IActionResult Index()
    {
        var today = DateTime.Now.ToString("dd/MM/yyyy");
        if (User.IsInRole("Member"))
        {
            var connectorModel = _connector.connector(User.FindFirstValue(ClaimTypes.NameIdentifier), today).Select(u => new DietModelView().get(u));
            var con = new Connector();
            con.diet = connectorModel;
            con.dish_types = _dish_Types.types().Select(u => new Dish_TypesModelView().map(u));
            //con.macro = _user.getById(User.FindFirstValue(ClaimTypes.NameIdentifier)).Select(u => new UserMacro().GetMacro(u));
            con.macro = new UserMacro().GetDietUserMacro(_user.getById(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return View(con);
        }
        else if (User.IsInRole("Dietetyk"))
        {
            var connectorModel = _connector.connector(User.FindFirstValue(ClaimTypes.NameIdentifier), today).Select(u => new DietModelView().get(u));
            var con = new Connector();
            con.diet = connectorModel;
            con.dish_types = _dish_Types.types().Select(u => new Dish_TypesModelView().map(u));
            //con.macro = _user.getById(User.FindFirstValue(ClaimTypes.NameIdentifier)).Select(u => new UserMacro().GetDietUserMacro(u));
            con.macro = new UserMacro().GetDietUserMacro(_user.getById(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            con.userlist = _user.getByIdDietetyk(User.FindFirstValue(ClaimTypes.NameIdentifier)).Select(u => new UserMacro().GetDietUserMacro(u));
            
            return View(con);
        }
        else return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult getDiet(string date)
    {
        if (date != null)
        {
            var today = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            var connectorModel = _connector.connector(User.FindFirstValue(ClaimTypes.NameIdentifier), today).Select(u => new DietModelView().get(u));

            var con = new Connector();
            con.diet = connectorModel;
            con.dish_types = _dish_Types.types().Select(u => new Dish_TypesModelView().map(u));
            return PartialView("_DietGrid", con);
        }
        else return RedirectToAction("Error");
    }

    [HttpPost]
    public IActionResult InsertDiet(string date, int dish_typeID, int dishID, int weight)
    {
        var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var inserter = new ConnectorInsertDTO();
        var Date = DateOnly.Parse(date);

        inserter.User_ID = userID;
        inserter.DishType_ID = dish_typeID;
        inserter.Dish_ID = dishID;
        inserter.Weight = weight;
        inserter.Diet_Date = Date;

        _connector.Insert(inserter);

        return RedirectToAction("getDiet", new {date = Date.ToString() });
    }

    [HttpPost]
    public IActionResult DeleteDish(string date, int dish_typeID, int dishID, int conID)
    {
        var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var deleter = new ConnectorDeleteDTO();
        var Date = DateOnly.Parse(date);

        deleter.ID = conID;
        deleter.User_ID = userID;

        _connector.delete(deleter);

        return RedirectToAction("getDiet", new { date = Date.ToString() });
    }

    [HttpGet]
    public IEnumerable<DishModelView> getDishes(string name)
    {
        var model = new List<DishModelView>();

        model = _dishes.dishes(name).Select(u => new DishModelView().map(u)).ToList();

        return model;
    }


    [HttpGet]
    public IActionResult getUserDiet(string date, string email)
    {
        if (date != null)
        {
            var guid = _user.getGuidByEmail(email);
            var today = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            var connectorModel = _connector.connector(guid, today).Select(u => new DietModelView().get(u));

            var con = new Connector();
            con.diet = connectorModel;
            //con.macro = _user.getById(guid).Select(u => new UserMacro().GetDietUserMacro(u));
            con.macro = new UserMacro().GetDietUserMacro(_user.getById(guid));
            con.dish_types = _dish_Types.types().Select(u => new Dish_TypesModelView().map(u));
            return PartialView("_DietGrid", con);
        }
        else return RedirectToAction("Error");
    }
    

    [HttpPost]
    public IActionResult InsertDietDietetyk(string date, int dish_typeID, int dishID, int weight, string email)
    {
        var userID = _user.getGuidByEmail(email);
        var inserter = new ConnectorInsertDTO();
        var Date = DateOnly.Parse(date);

        inserter.User_ID = userID;
        inserter.DishType_ID = dish_typeID;
        inserter.Dish_ID = dishID;
        inserter.Weight = weight;
        inserter.Diet_Date = Date;

        _connector.Insert(inserter);

        return RedirectToAction("getUserDiet", new { date = Date.ToString(), email = email });
    }

    [HttpPost]
    public IActionResult DeleteDietDietetyk(string date, int dish_typeID, int dishID, int conID, string email)
    {
        var userID = _user.getGuidByEmail(email);
        var deleter = new ConnectorDeleteDTO();
        var Date = DateOnly.Parse(date);

        deleter.ID = conID;
        deleter.User_ID = userID;

        _connector.delete(deleter);

        return RedirectToAction("getUserDiet", new { date = Date.ToString(), email = email });
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

