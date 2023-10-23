using LibreriaApi.Dtos;
using Microsoft.Data.SqlClient;

namespace LibreriaApi.Service
{
    public class EditorialService : IEditorialService
    {
        private readonly IConfiguration? _Configuration;

        public EditorialService(IConfiguration? configuration)
        {
            _Configuration = configuration;
        }

        public IList<Editorial> Get()
        {
            var list = new List<Editorial>();

            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = "SELECT * FROM Editorial";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var editorial = new Editorial();

                            editorial.Id = reader.GetInt32(0);
                            editorial.Name = reader.GetString(1);
                            editorial.Address = reader.GetString(2);
                            editorial.City = reader.GetString(3);
                            editorial.Phone = reader.GetString(4);
                            editorial.Email = reader.GetString(5);

                            list.Add(editorial);
                        }
                    }
                }
            }
            return list;
        }

        public void Save(SaveEditorial dto)
        {
            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = $"INSERT INTO Editorial " +
                             $"VALUES ('{(dto.Name)}','{(dto.Address)}','{(dto.City)}','{(dto.Phone)}','{(dto.Email)}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void Update(int id, SaveEditorial dto)
        {
            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = $"UPDATE Editorial " +
                             $"SET Name = '{(dto.Name)}', " +
                             $"Address = '{(dto.Address)}',  " +
                             $"City = '{(dto.City)}', " +
                             $"Phone = '{(dto.Phone)}', " +
                             $"Email = '{(dto.Email)}' " +
                             $"WHERE EditorialId = {(id)}";

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

                string sql = $"DELETE FROM Editorial " +
                             $"WHERE EditorialId = {(id)}";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }

    public interface IEditorialService
    {
        IList<Editorial> Get();

        void Save(SaveEditorial dto);

        void Update(int id, SaveEditorial dto);

        void Delete(int id);
    }
}
