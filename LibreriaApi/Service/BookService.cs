using LibreriaApi.Dtos;
using Microsoft.Data.SqlClient;

namespace LibreriaApi.Service
{
    public class BookService : IBookService
    {
        private readonly IConfiguration? _Configuration;

        public BookService(IConfiguration? configuration)
        {
            _Configuration = configuration;
        }

        public IList<Book> Get()
        {
            var list = new List<Book>();

            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = "SELECT a.BookId,a.Title,a.Autor,a.Price,a.EditorialId,b.Name,b.Address,b.City,b.Phone,b.Email " +
                             "FROM Book a " +
                             "LEFT JOIN Editorial b ON a.EditorialId = b.EditorialId;";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Book = new Book();

                            Book.Id = reader.GetInt32(0);
                            Book.Title = reader.GetString(1);
                            Book.Autor = reader.GetString(2);
                            Book.Price = reader.GetDecimal(3);
                            Book.EditorialId = reader.GetInt32(4);
                            Book.Editorial = new Editorial 
                            {
                                Id = reader.GetInt32(4),
                                Name = reader.GetString(5),
                                Address = reader.GetString(6),
                                City = reader.GetString(7),
                                Phone = reader.GetString(8),
                                Email = reader.GetString(9)
                            };

                            list.Add(Book);
                        }
                    }
                }
            }
            return list;
        }

        public void Save(SaveBook dto)
        {
            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = $"INSERT INTO Book " +
                             $"VALUES ('{(dto.Title)}','{(dto.Autor)}','{(dto.Price)}','{(dto.EditorialId)}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void Update(int id, SaveBook dto)
        {
            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = $"UPDATE Book " +
                             $"SET Title = '{(dto.Title)}', " +
                             $"Autor = '{(dto.Autor)}',  " +
                             $"Price = '{(dto.Price)}', " +
                             $"EditorialId = '{(dto.EditorialId)}' " +
                             $"WHERE BookId = {(id)}";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = $"DELETE FROM Book " +
                             $"WHERE BookId = {(id)}";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }

    public interface IBookService
    {
        IList<Book> Get();

        void Save(SaveBook dto);

        void Update(int id, SaveBook dto);

        void Delete(int id);
    }
}
