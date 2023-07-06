using System;
using Microsoft.EntityFrameworkCore;
using PracaInzynierskaDietetyka.DTO.UserDataDTOS;
using PracaInzynierskaDietetyka.Entity;
using PracaInzynierskaDietetyka.Repository;

namespace PracaInzynierskaDietetyka.Services.UserDataServices
{
	public class UserDataService : IUserDataService
    {
        public readonly IUserDataRepository _users;

        public UserDataService(IUserDataRepository users)
        {
            _users = users;
        }

        public void AddPersonToDietetyk(AddUserToDietetykDTO addNewPerson)
        {
            var x = _users.getUserByID(addNewPerson.GUID);
            if(x != null)
            {
                x.Dietetyk_ID = addNewPerson.Dietetyk_ID;
                _users.AddPersonToDietetyk(x);
            }
        }

        public UserDataDTO getById(string id)
        {
            //return _users.getUserByID(id).Select(x => new UserDataDTO().map(x));
            return new UserDataDTO().map(_users.getUserByID(id));
        }

        public UserDataDTO mapUserData(string id)
        {
            return new UserDataDTO().mapUserData(_users.getUserByID(id));
        }

        public IEnumerable<UserDataDTO> getByIdDietetyk(string id)
        {
            return _users.getWhereDietetykID(id).Select(x => new UserDataDTO().map(x));
        }

        public string getGuidByEmail(string email)
        {
            return _users.GetByEmail(email).GUID;
        }

        public void ChangeUserMacro(ChangeUserMacro macro)
        {
            var x = _users.getUserByID(macro.GUID);
            if (x != null)
            {
                if(macro.Kcal != null)
                {
                    x.Kcal = macro.Kcal;
                    _users.AddPersonToDietetyk(x);
                }
                else if (macro.Protein != null)
                {
                    x.Protein = macro.Protein;
                    _users.AddPersonToDietetyk(x);
                }
                else if (macro.Fat != null)
                {
                    x.Fat = macro.Fat;
                    _users.AddPersonToDietetyk(x);
                }
                else if (macro.Carbon != null)
                {
                    x.Carbon = macro.Carbon;
                    _users.AddPersonToDietetyk(x);
                }
            }
        }

        public void AddNewPerson(UserDataDTO user)
        {
            _users.AddNewUser(new Users().AddNewUser(user));
        }
    }
}

