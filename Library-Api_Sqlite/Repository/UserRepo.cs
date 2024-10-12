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

        public async Task<bool> RemoveUser(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "" +
                    "DELETE FROM Users WHERE NIC = @id ";
                command.Parameters.AddWithValue("@id", id);

                var result = await command.ExecuteNonQueryAsync();

                return result > 0;
            }
        }

        public async Task<User> Getuser(int nic)
        {
            

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT NIC, firstName, lastName, fullName, email, password, phoneNumber, joinDate, lastLoginDate, rentCount, profileImg 
            FROM Users WHERE nic = @nic";
                command.Parameters.AddWithValue("@nic", nic);

                using (var reader = command.ExecuteReader())
                {
                    if (await reader.ReadAsync())
                    {
                        var user = new User
                        {
                            NIC = reader.GetInt32(0),
                            firstName = reader.GetString(1),
                            lastName = reader.GetString(2),
                            fullName = reader.GetString(3),
                            email = reader.GetString(4),
                            password = reader.GetString(5),
                            phoneNumber = reader.GetString(6),
                            joinDate = DateTime.Parse(reader.GetString(7)),
                            lastLoginDate = DateTime.Parse(reader.GetString(8)),
                            rentCount = reader.GetInt32(9),
                            profileimg = reader.GetString(10),
                        };

                        return user;
                    }
                    
                }
            }
            return null;

        }
        public async Task<IEnumerable<User>> GetAll()
        {
            var userList = new List<User>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT NIC, firstName, lastName, fullName, email, password, phoneNumber, joinDate, lastLoginDate, rentCount, profileImg 
            FROM Users";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            NIC = reader.GetInt32(0),
                            firstName = reader.GetString(1),
                            lastName = reader.GetString(2),
                            fullName = reader.GetString(3),
                            email = reader.GetString(4),
                            password = reader.GetString(5),
                            phoneNumber = reader.GetString(6),
                            joinDate = DateTime.Parse(reader.GetString(7)),
                            lastLoginDate = DateTime.Parse(reader.GetString(8)),
                            rentCount = reader.GetInt32(9),
                            profileimg = reader.GetString(10),
                        };
                        userList.Add(user);

                    }
                }
            }
            return userList;
        }

        public async Task<Dictionary<string, string>> Browusers()
        {
            var usersDictionary = new Dictionary<string, string>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync(); 

                using (var command = connection.CreateCommand())
                {
                   
                    command.CommandText = @"SELECT DISTINCT Users.nic, Users.fullname 
                                    FROM Users 
                                    INNER JOIN lentrecords ON lentrecords.UserNIC = Users.nic";

                   
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync()) 
                        {
                            var nic = reader.GetString(0);     
                            var fullname = reader.GetString(1); 

                           
                            if (!usersDictionary.ContainsKey(nic)) 
                            {
                                usersDictionary.Add(nic, fullname);
                            }
                        }
                    }
                }
            }

            return usersDictionary; 
        }



    }
}
