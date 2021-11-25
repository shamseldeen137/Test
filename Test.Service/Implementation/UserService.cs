using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.DTO;
using Test.Data.Models;
using Test.Repo.Interface;
using Test.Service.Interface;

namespace Test.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _IUserRepo;
        private readonly IMapper _Imapper;

        public UserService(IUserRepo IUserRepository,IMapper imapper)
        {
            this._IUserRepo = IUserRepository;
            this._Imapper = imapper;
        }

        public async Task<bool> GetuserByemail(string email)
        {
           return await _IUserRepo.GetuserByemail(email);
        }

        public async Task<UserDTO> Login(UserDTO viemodel)
        {
           var user = _Imapper.Map<User>(viemodel);
            user.Password = EncodePasswordToBase64(viemodel.Password);
            var result =await  _IUserRepo.Login(user);
            var loggeduser = _Imapper.Map<UserDTO>(result);
            return loggeduser;
        }

        public async Task<UserDTO> Register(UserDTO viemodel)
        {
            var user = _Imapper.Map<User>(viemodel);
            user.Password = EncodePasswordToBase64(viemodel.Password);
          var result= await _IUserRepo.Register(user);
            var loggeduser = _Imapper.Map<UserDTO>(result);
            return loggeduser;
        }
        string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
