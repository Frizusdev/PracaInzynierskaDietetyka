using System;
using PracaInzynierskaDietetyka.DTO.UserDataDTOS;

namespace PracaInzynierskaDietetyka.Services.UserDataServices
{
	public interface IUserDataService
	{
        void AddPersonToDietetyk(AddUserToDietetykDTO addNewPerson);
        UserDataDTO getById(string id);
        UserDataDTO mapUserData(string id);
        IEnumerable<UserDataDTO> getByIdDietetyk(string id);
		string getGuidByEmail(string email);
        void ChangeUserMacro(ChangeUserMacro addNewPerson);
        void AddNewPerson(UserDataDTO user);
    }
}

