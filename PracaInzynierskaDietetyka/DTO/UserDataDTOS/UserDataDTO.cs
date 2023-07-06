using System;
using PracaInzynierskaDietetyka.Entity;
using PracaInzynierskaDietetyka.Models;

namespace PracaInzynierskaDietetyka.DTO.UserDataDTOS
{
	public class UserDataDTO
    {
        public int ID { set; get; }
        public string? GUID { set; get; }
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
        public string? Dietetyk_ID { set; get; }
        public string? Email { set; get; }

        public UserDataDTO map(Users users)
        {
            First_Name = users.First_Name;
            Last_Name = users.Last_Name;
            Email = users.Email;
            Kcal = users.Kcal;
            Protein = users.Protein;
            Fat = users.Fat;
            Carbon = users.Carbon;

            return this;
        }

        public UserDataDTO mapguid(Users users)
        {
            GUID = users.GUID;

            return this;
        }

        public UserDataDTO mapUserData(Users users)
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

