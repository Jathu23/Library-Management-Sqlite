using Library_Api_Sqlite.EntityModals;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Library_Api_Sqlite.Data
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Books (
            Id INTEGER PRIMARY KEY,
            ISBN TEXT NOT NULL,
            Title TEXT NOT NULL,
            Author TEXT NOT NULL,
            Genre TEXT,
            Copies INTEGER NOT NULL,
            AviCopies INTEGER NOT NULL,
            PublishYear INTEGER NOT NULL,
            AddDateTime TEXT NOT NULL,
            Images TEXT,
            RentCount INTEGER NOT NULL
        );

        CREATE TABLE IF NOT EXISTS Users (
            NIC INTEGER PRIMARY KEY,  -- Assuming NIC is a unique ID
            firstName TEXT NOT NULL,
            lastName TEXT NOT NULL,
            fullName TEXT NOT NULL,
            email TEXT NOT NULL UNIQUE,  -- Email should be unique
            password TEXT NOT NULL,
            phoneNumber TEXT NOT NULL,
            joinDate TEXT NOT NULL,  -- Storing DateTime as TEXT
            lastLoginDate TEXT NOT NULL,  -- Storing DateTime as TEXT
            rentCount INTEGER NOT NULL,
            profileImg TEXT  -- Profile image file path
        );
        CREATE TABLE IF NOT EXISTS ADMINS(
        ID INTEGER PRIMARY KEY,
        NAME TEXT NOT NULL,
        EMAIL TEXT NOT NULL UNIQUE,
        USERID TEXT NOT NULL UNIQUE,
        password TEXT NOT NULL,
        profileImg TEXT
        );

        CREATE TABLE IF NOT EXISTS LentRecords (
    Id INTEGER PRIMARY KEY,
    ISBN TEXT NOT NULL,
    UserNIC INTEGER NOT NULL,
    Copies INTEGER NOT NULL,
    LentDate TEXT NOT NULL,
    ReturnDate TEXT NOT NULL
);

            CREATE TABLE IF NOT EXISTS LentHistory (
    Id INTEGER PRIMARY KEY,
    ISBN TEXT NOT NULL,
    UserNIC INTEGER NOT NULL,
    Copies INTEGER NOT NULL,
    LentDate TEXT NOT NULL,
    ReturnDate TEXT NOT NULL
);

                CREATE TABLE IF NOT EXISTS ReturnRecords (
    Id INTEGER PRIMARY KEY,
    UserNIC INTEGER NOT NULL,
    LentId INTEGER NOT NULL,
    ISBN TEXT NOT NULL,
    LentCopies INTEGER NOT NULL,
    ReturnCopies INTEGER NOT NULL,
    ReturnDate TEXT NOT NULL
);
                 CREATE TABLE IF NOT EXISTS ReturnHistory (
    Id INTEGER PRIMARY KEY,
    UserNIC INTEGER NOT NULL,
    LentId INTEGER NOT NULL,
    ISBN TEXT NOT NULL,
    LentCopies INTEGER NOT NULL,
    ReturnCopies INTEGER NOT NULL,
    ReturnDate TEXT NOT NULL
);


        -- Insert sample data for Books if the table is empty
        INSERT OR IGNORE INTO Books (Id, ISBN, Title, Author, Genre, Copies, AviCopies, PublishYear, AddDateTime, Images, RentCount) VALUES
        (11134, '978-0-7432-7356-3', 'The Kite Runner', 'Khaled Hosseini', 'Fiction,Drama', 18, 18, 2003, '2024-09-27', 'bookimages/g (1).jpg,bookimages/g (2).jpg,bookimages/h (4).jpg', 0),
        (21245, '978-0-375-50420-9', 'A Song of Ice and Fire', 'George R.R. Martin', 'Fantasy,Adventure', 25, 25, 1996, '2024-09-27', 'bookimages/g (4).jpg,bookimages/g (3).jpg,bookimages/h (4).jpg', 0),
        (31356, '978-0-06-112241-5', 'Brave New World', 'Aldous Huxley', 'Dystopian,Science Fiction', 14, 14, 1932, '2024-09-27', 'bookimages/g (5).jpg,bookimages/h (4).jpg,bookimages/h (4).jpg', 0),
        (41467, '978-0-618-00224-2', 'The Two Towers', 'J.R.R. Tolkien', 'Fantasy,Adventure', 25, 25, 1954, '2024-09-27', 'bookimages/g (6).jpg,bookimages/g (7).jpg,bookimages/h (4).jpg', 0),
        (51578, '978-1-5011-9702-9', 'The Institute', 'Stephen King', 'Horror,Thriller', 15, 15, 2019, '2024-09-27', 'bookimages/g (8).jpg,bookimages/g (9).jpg,bookimages/h (4).jpg', 0),
        (61689, '978-0-593-07353-0', 'Atomic Habits', 'James Clear', 'Self-help,Non-Fiction', 30, 30, 2018, '2024-09-27', 'bookimages/h (1).jpg,bookimages/g (7),bookimages/h (4).jpg', 0),
        (71790, '978-0-399-56441-3', 'Norwegian Wood', 'Haruki Murakami', 'Fiction,Romance', 9, 9, 1987, '2024-09-27', 'bookimages/h (2).jpg,bookimages/g (1).jpg,bookimages/h (4).jpg', 0),
        (81801, '978-1-4516-8600-5', 'Steve Jobs', 'Walter Isaacson', 'Biography,Non-Fiction', 22, 22, 2011, '2024-09-27', 'bookimages/h (3).jpg,bookimages/g (7),bookimages/h (4).jpg', 0),
        (91912, '978-0-374-53197-2', 'Where the Crawdads Sing', 'Delia Owens', 'Fiction,Mystery', 12, 12, 2018, '2024-09-27', 'bookimages/h (2).jpg,bookimages/g (7),bookimages/h (4).jpg', 0),
        (102023, '978-1-250-30765-9', 'Project Hail Mary', 'Andy Weir', 'Science Fiction,Thriller', 22, 22, 2021, '2024-09-27', 'bookimages/h (3).jpg,bookimages/g (1).jpg,bookimages/h (4).jpg', 0);

        -- Insert sample data for Users if the table is empty
        INSERT OR IGNORE INTO Users (NIC, firstName, lastName, fullName, email, password, phoneNumber, joinDate, lastLoginDate, rentCount, profileImg) VALUES
        (1001, 'John', 'Doe', 'John Doe', 'john.doe@example.com', 'password123', '555-1234', '2024-09-27', '2024-09-27', 0, 'johndoe.jpg'),
        (1002, 'Jane', 'Smith', 'Jane Smith', 'jane.smith@example.com', 'password456', '555-5678', '2024-09-27', '2024-09-27', 0, 'janesmith.jpg');

        
         --Insert sample data for ADMINS if the table is empty
        INSERT OR IGNORE INTO ADMINS(ID, NAME, EMAIL, USERID, password, profileImg) VALUES
          (2301, 'Alice Johnson', 'alice.johnson@example.com', 'alice01', 'password123', 'alice.jpg'),
           (2302, 'Bob Smith', 'bob.smith@example.com', 'bob02', 'password456', 'bob.jpg');


        "
                ;
       

                command.ExecuteNonQuery();
            }
        }

    }
}
