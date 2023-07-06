using System;
using System.ComponentModel.DataAnnotations;
using PracaInzynierskaDietetyka.DTO.UserDataDTOS;

namespace PracaInzynierskaDietetyka.Entity
{
    public class Users : IEntity
    {
        [Key]
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

        public Users AddNewUser(UserDataDTO user)
        {
            GUID = user.GUID;
            First_Name = user.First_Name;
            Last_Name = user.Last_Name;
            Sex = user.Sex;
            Age = user.Age;
            Height = user.Height;
            Weight = user.Weight;
            Wish_Weight = user.Wish_Weight;
            Kcal = user.Kcal;
            Protein = user.Protein;
            Fat = user.Fat;
            Carbon = user.Carbon;
            Extras = user.Extras;
            Dietetyk_ID = user.Dietetyk_ID;
            Email = user.Email;

            return this;
        }
    }
}

