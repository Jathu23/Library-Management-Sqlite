using Library_Api_Sqlite.Dto_s.Book_Dtos;
using Library_Api_Sqlite.Dto_s.User_Dtos;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.FileService;
using Library_Api_Sqlite.Repository;

namespace Library_Api_Sqlite.Services
{
    public class UserServices
    {
        private readonly UserRepo _userRepo;
        private readonly RootOprations _RootOprations;

        public UserServices(UserRepo userRepo, RootOprations rootOprations)
        {
            _userRepo = userRepo;
            _RootOprations = rootOprations;
        }

        public async Task<User> Add(User_Req_Dto Requser)
        {
            List<string> imagePath;

            if (Requser.profileimg == null)
            {
                imagePath = new List<string> { "defaultimg.jpg"};
            }
            else
            {
                var image = new List<IFormFile> { Requser.profileimg };

                imagePath = await _RootOprations.SaveImages(image, "userimages");
            }


            User user = new User
            {
                NIC = Requser.NIC,
                firstName = Requser.firstName,
                lastName = Requser.lastName,
                fullName = Requser.firstName + Requser.lastName,
                email = Requser.email,
                password = Requser.password,
                phoneNumber = Requser.phoneNumber,
                joinDate = DateTime.Now,
                lastLoginDate = DateTime.Now,
                rentCount = 0,
                profileimg = imagePath[0]

            };

            return await _userRepo.AddUser(user);

        }

        public async Task<bool> ValidDateUser(User_Req_Dto user, int nic)
        {
            try
            {
                var getuser = await _userRepo.Getuser(nic);
                if (getuser == null)
                {
                    return false;
                }
                if (getuser.password != user.password)
                {
                    Console.WriteLine("Password mismatch.");
                    return false;
                }
                return await _userRepo.ValidDateUser(getuser.password, nic);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }



        public async Task<bool> UpdateUser(int nic, User_Update_Dto reqUser)
        {
            var oldUser = await _userRepo.Getuser(nic);

            string imgpath;
            if (reqUser.ProfileImage == null)
            {
                imgpath = oldUser.profileimg;
            }
            else
            {
                var ProfileImagepath = await _RootOprations.SaveImages(new List<IFormFile> { reqUser.ProfileImage }, "userimages");
                imgpath = ProfileImagepath[0];
                _RootOprations.DeleteImages(new List<string> { oldUser.profileimg }, "userimages");
            }
            var updatedUser = new UserUpdate
            {
                FirstName = reqUser.FirstName,
                LastName = reqUser.LastName ,
                FullName = reqUser.FirstName + " " + reqUser.LastName,
                Password = reqUser.Password ,
                PhoneNumber = reqUser.PhoneNumber ,
                ProfileImage = imgpath
            };

            return await _userRepo.UpdateUser(updatedUser, nic);
        }


        public async Task<bool> RemoveUser(int id)
        {
            User user = new User();

            if (user.profileimg != "default.jpg" && user.profileimg != null)
            {
                _RootOprations.DeleteImages(new List<string> {user.profileimg}, "userimages");
            }


            return await _userRepo.RemoveUser(id);
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepo.GetAll();
        }

        public async Task<User> Getuser(int nic)
        {
            return await _userRepo.Getuser(nic);
        }




        //public async Task<Dictionary<string, string>> Browusers()
        //{

        //}
    }
}
