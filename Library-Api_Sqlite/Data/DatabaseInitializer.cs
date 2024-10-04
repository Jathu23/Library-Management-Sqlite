﻿using Microsoft.Data.Sqlite;

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
            Id INTEGER PRIMARY KEY ,
            ISBN TEXT NOT NULL,
            Title TEXT NOT NULL,
            Author TEXT NOT NULL,
            Genre TEXT,  -- Assuming this is stored as a comma-separated string
            Copies INTEGER NOT NULL,
            AviCopies INTEGER NOT NULL,
            PublishYear INTEGER NOT NULL,
            AddDateTime TEXT NOT NULL,  -- Use TEXT to store DateTime as a string
            Images TEXT,  -- Assuming this is also stored as a comma-separated string
            RentCount INTEGER NOT NULL
             );

                

             -- Insert sample data if the table is empty
                INSERT OR IGNORE INTO Books (Id, ISBN, Title, Author, Genre, Copies, AviCopies, PublishYear, AddDateTime, Images, RentCount) VALUES
                (11134, '978-0-7432-7356-3', 'The Kite Runner', 'Khaled Hosseini', 'Fiction,Drama', 18, 12, 2003, '2024-09-27', 'kiterunner1.jpg,kiterunner2.jpg', 20),
                (21245, '978-0-375-50420-9', 'A Song of Ice and Fire', 'George R.R. Martin', 'Fantasy,Adventure', 25, 18, 1996, '2024-09-27', 'asoiaf1.jpg,asoiaf2.jpg', 35),
                (31356, '978-0-06-112241-5', 'Brave New World', 'Aldous Huxley', 'Dystopian,Science Fiction', 14, 10, 1932, '2024-09-27', 'bravenewworld1.jpg', 28),
                (41467, '978-0-618-00224-2', 'The Two Towers', 'J.R.R. Tolkien', 'Fantasy,Adventure', 25, 15, 1954, '2024-09-27', 'twotowers1.jpg,twotowers2.jpg', 22),
                (51578, '978-1-5011-9702-9', 'The Institute', 'Stephen King', 'Horror,Thriller', 20, 15, 2019, '2024-09-27', 'institute1.jpg,institute2.jpg', 5),
                (61689, '978-0-593-07353-0', 'Atomic Habits', 'James Clear', 'Self-help,Non-Fiction', 30, 25, 2018, '2024-09-27', 'atomichabits1.jpg', 12),
                (71790, '978-0-399-56441-3', 'Norwegian Wood', 'Haruki Murakami', 'Fiction,Romance', 12, 9, 1987, '2024-09-27', 'norwegianwood1.jpg,norwegianwood2.jpg', 16),
                (81801, '978-1-4516-8600-5', 'Steve Jobs', 'Walter Isaacson', 'Biography,Non-Fiction', 22, 18, 2011, '2024-09-27', 'stevejobs1.jpg,stevejobs2.jpg', 24),
                (91912, '978-0-374-53197-2', 'Where the Crawdads Sing', 'Delia Owens', 'Fiction,Mystery', 18, 12, 2018, '2024-09-27', 'crawdads1.jpg', 9),
                (102023, '978-1-250-30765-9', 'Project Hail Mary', 'Andy Weir', 'Science Fiction,Thriller', 28, 22, 2021, '2024-09-27', 'projecthailmary1.jpg,projecthailmary2.jpg', 3);
            ";   
             command.ExecuteNonQuery();
          }

        }
    }
}
