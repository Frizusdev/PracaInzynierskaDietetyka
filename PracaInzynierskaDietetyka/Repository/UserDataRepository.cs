using System;
using PracaInzynierskaDietetyka.Data;
using PracaInzynierskaDietetyka.DTO.UserDataDTOS;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Repository
{
	public class UserDataRepository : GenericRepository<Users>, IUserDataRepository
    {
        private readonly ApplicationDbContext _context;

        public UserDataRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void AddPersonToDietetyk(Users addNewPerson)
        {
            _context.Set<Users>().Update(addNewPerson);
            _context.SaveChanges();
        }

        public Users GetByEmail(string email)
        {
            return _context.Set<Users>().Single(x => x.Email == email);
        }

        public Users getUserByID(string id)
        {
            return _context.Set<Users>().Single(x => x.GUID == id);
        }

        public IEnumerable<Users> getWhereDietetykID(string id)
        {
            return _context.Set<Users>().Where(x => x.Dietetyk_ID == id || x.GUID == id);
        }

        public void AddNewUser(Users user)
        {
            try
            {
                _context.Set<Users>().Add(user);
                _context.SaveChanges();
            }
            catch
            {
                throw new InvalidOperationException("Unable to add User");
            }
            
        }
    }
}

