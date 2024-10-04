using Library_Api_Sqlite.Dto_s.User_Dtos;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.FileSaver;
using Library_Api_Sqlite.Repository;

namespace Library_Api_Sqlite.Services
{
    public class UserServices
    {
        //private readonly UserRepo _userRepo;
        //private readonly SaveToRoot _saveToRoot;

        //public UserServices(UserRepo userRepo, SaveToRoot saveToRoot)
        //{
        //    _userRepo = userRepo;
        //    _saveToRoot = saveToRoot;
        //}

        //public async Task<User> Add(User_Req_Dto Requser)
        //{
        //    var image = new List<IFormFile>
        //    {
        //        Requser.profileimg
        //    };

        //    var imagePath = await _saveToRoot.SaveImages(image, "userimages");

        //    User user = new User
        //    {
        //        NIC = Requser.NIC,
        //        firstName = Requser.firstName,
        //        lastName = Requser.lastName,
        //        fullName = Requser.firstName + Requser.lastName,
        //        email = Requser.email,
        //        password = Requser.password,
        //        phoneNumber = Requser.phoneNumber,
        //        joinDate = DateTime.Now,
        //        lastLoginDate = DateTime.Now,
        //        rentCount = 0,
        //        profileimg = imagePath[0]

        //    };

        //    return await _userRepo.AddUser(user);

        //}
    }
}
