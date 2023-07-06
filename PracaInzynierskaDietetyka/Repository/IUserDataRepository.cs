using System;
using PracaInzynierskaDietetyka.DTO.UserDataDTOS;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public interface IUserDataRepository : IGenericRepository<Users>
	{
		Users getUserByID(string id);
		IEnumerable<Users> getWhereDietetykID(string id);
		Users GetByEmail(string email);
        void AddPersonToDietetyk(Users addNewPerson);
		void AddNewUser(Users user);
    }
}

