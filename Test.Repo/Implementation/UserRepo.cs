using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Models;
using Test.Repo.Interface;

namespace Test.Repo.Implementation
{
 public   class UserRepo:IUserRepo
    {
        private readonly DBContext _dBContext;

        public DbSet<User> _entity { get; }

        public UserRepo(DBContext context)
        {
            _dBContext = context;
            _entity = _dBContext.Users;
        }
        public async Task<User> Register(User viewmodel)
        {



     

            try
            {
                await _entity.AddAsync(viewmodel);
                SaveChangesasync();
            
                //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
                return viewmodel;
            }
            catch (Exception e)
            {

                return null;
            }

        }
      public  async Task<User> Login(User viemodel)
        {
            var user = await _entity.FirstOrDefaultAsync(model => model.Email.ToLower() == viemodel.Email.ToLower() && model.Password == viemodel.Password);
            if (user != null)
            {
                
                return user;
            }

            return null;


        }
        public void SaveChangesasync()
        {


            _dBContext.SaveChanges();
        }

        public async Task<bool> GetuserByemail(string email)
        {
          return await  _entity.AnyAsync(u => u.Email.Trim().ToLower() == email.ToLower().Trim());
        }
    }
}
