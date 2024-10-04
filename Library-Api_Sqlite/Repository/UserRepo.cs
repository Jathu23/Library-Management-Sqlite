using Library_Api_Sqlite.EntityModals;
using Microsoft.Data.Sqlite;

namespace Library_Api_Sqlite.Repository
{
    public class UserRepo
    {
        private readonly string _connectionString;

        public UserRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> AddUser(User user)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
        INSERT INTO Users (NIC, firstName, lastName, fullName, email, password, phoneNumber, joinDate, lastLoginDate, rentCount, profileImg) 
        VALUES (@NIC, @firstName, @lastName, @fullName, @Email, @Password, @PhoneNumber, @JoinDate, @LastLoginDate, @RentCount, @ProfileImg)";

                command.Parameters.AddWithValue("@NIC", user.NIC);
                command.Parameters.AddWithValue("@firstName", user.firstName);
                command.Parameters.AddWithValue("@lastName", user.lastName);
                command.Parameters.AddWithValue("@fullName", user.fullName);
                command.Parameters.AddWithValue("@Email", user.email);
                command.Parameters.AddWithValue("@Password", user.password);
                command.Parameters.AddWithValue("@PhoneNumber", user.phoneNumber);
                command.Parameters.AddWithValue("@JoinDate", user.joinDate);
                command.Parameters.AddWithValue("@LastLoginDate", user.lastLoginDate);
                command.Parameters.AddWithValue("@RentCount", user.rentCount);
                command.Parameters.AddWithValue("@ProfileImg", user.profileimg);

                await command.ExecuteNonQueryAsync();
            }

            return user;
        }
    }
}
