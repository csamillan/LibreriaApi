using LibreriaApi.Dtos;
using Microsoft.Data.SqlClient;

namespace LibreriaApi.Service
{
    public class InventaryService : IInventaryService
    {
        private readonly IConfiguration? _Configuration;

        public InventaryService(IConfiguration? configuration)
        {
            _Configuration = configuration;
        }

        public IList<Inventary> Get()
        {
            var list = new List<Inventary>();

            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = "SELECT a.InventaryId,a.Stock,a.BookId,a.BranchId, " +
                             "b.Name,b.Address,b.City,b.Phone,b.Email, " +
                             "c.Title,c.Autor,c.Price,c.EditorialId, " +
                             "d.Name,d.Address,d.City,d.Phone,d.Email " +
                             "FROM Inventary a " +
                             "LEFT JOIN Branch b ON a.BranchId = b.BranchId " +
                             "LEFT JOIN Book c ON a.BookId = c.BookId " +
                             "LEFT JOIN Editorial d ON c.EditorialId = d.EditorialId ";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var inventary = new Inventary();

                            inventary.Id = reader.GetInt32(0);
                            inventary.Stock = reader.GetDecimal(1);
                            inventary.BookId = reader.GetInt32(2);
                            inventary.BranchId = reader.GetInt32(3);
                            inventary.Branch = new Branch
                            {
                                Id = reader.GetInt32(3),
                                Name = reader.GetString(4),
                                Address = reader.GetString(5),
                                City = reader.GetString(6),
                                Phone = reader.GetString(7),
                                Email = reader.GetString(8),
                            };
                            inventary.Book = new Book
                            {
                                Id = reader.GetInt32(2),
                                Title = reader.GetString(9),
                                Autor = reader.GetString(10),
                                Price = reader.GetDecimal(11),
                                EditorialId = reader.GetInt32(12),
                                Editorial = new Editorial
                                {
                                    Id = reader.GetInt32(12),
                                    Name = reader.GetString(13),
                                    Address = reader.GetString(14),
                                    City = reader.GetString(15),
                                    Phone = reader.GetString(16),
                                    Email = reader.GetString(17)
                                }
                            };
                            list.Add(inventary);
                        }
                    }
                }
            }
            return list;
        }

        public void Save(SaveInventary dto)
        {
            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = $"INSERT INTO Inventary " +
                             $"VALUES ('{(dto.Stock)}','{(dto.BookId)}','{(dto.BranchId)}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void Update(int id, SaveInventary dto)
        {
            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = $"UPDATE Inventary " +
                             $"SET Stock = '{(dto.Stock)}', " +
                             $"BookId = '{(dto.BookId)}',  " +
                             $"BranchId = '{(dto.BranchId)}' " +
                             $"WHERE InventaryId = {(id)}";

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

                string sql = $"DELETE FROM Inventary " +
                             $"WHERE InventaryId = {(id)}";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }

    public interface IInventaryService
    {
        IList<Inventary> Get();

        void Save(SaveInventary dto);

        void Update(int id, SaveInventary dto);

        void Delete(int id);
    }
}
