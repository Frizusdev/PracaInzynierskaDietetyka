using System;
using PracaInzynierskaDietetyka.DTO.UserDataDTOS;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Models
{
	public class UserMacro
    {
        public string? First_Name { set; get; }
        public string? Last_Name { set; get; }
        public string? Email { set; get; }
        public double? Kcal { set; get; }
        public double? Protein { set; get; }
        public double? Fat { set; get; }
        public double? Carbon { set; get; }

        public UserMacro GetMacro(UserDataDTO users)
        {
            Kcal = users.Kcal;
            Protein = users.Protein;
            Fat = users.Fat;
            Carbon = users.Carbon;

            return this;
        }

        public UserMacro GetDietUserMacro(UserDataDTO users)
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
    }
}

