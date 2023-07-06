using System;
using PracaInzynierskaDietetyka.DTO.UserDataDTOS;

namespace PracaInzynierskaDietetyka.Models
{
	public class UserDataModelView
	{
        public string? First_Name { set; get; }
        public string? Last_Name { set; get; }
        public string? Sex { set; get; }
        public int? Age { set; get; }
        public int? Height { set; get; }
        public int? Weight { set; get; }
        public string? Wish_Weight { set; get; }
        public double? Kcal { set; get; }
        public double? Protein { set; get; }
        public double? Fat { set; get; }
        public double? Carbon { set; get; }
        public string? Extras { set; get; }

        public UserDataModelView map(UserDataDTO users)
        {
            First_Name = users.First_Name;
            Last_Name = users.Last_Name;
            Sex = users.Sex;
            Age = users.Age;
            Height = users.Height;
            Weight = users.Weight;
            Wish_Weight = users.Wish_Weight;
            Kcal = users.Kcal;
            Protein = users.Protein;
            Fat = users.Fat;
            Carbon = users.Carbon;
            Extras = users.Extras;

            return this;
        }
    }
}

