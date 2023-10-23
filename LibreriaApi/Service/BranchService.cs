using LibreriaApi.Dtos;
using Microsoft.Data.SqlClient;

namespace LibreriaApi.Service
{
    public class BranchService : IBranchService
    {
        private readonly IConfiguration? _Configuration;

        public BranchService(IConfiguration? configuration)
        {
            _Configuration = configuration;
        }

        public IList<Branch> Get()
        {
            var list = new List<Branch>();

            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = "select * from Branch";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var branch = new Branch();

                            branch.Id = reader.GetInt32(0);
                            branch.Name = reader.GetString(1);
                            branch.Address = reader.GetString(2);
                            branch.City = reader.GetString(3);
                            branch.Phone = reader.GetString(4);
                            branch.Email = reader.GetString(5);

                            list.Add(branch);
                        }
                    }
                }
            }
            return list;
        }

        public void Save(SaveBranch dto)
        {
            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = $"INSERT INTO Branch " +
                             $"VALUES ('{(dto.Name)}','{(dto.Address)}','{(dto.City)}','{(dto.Phone)}','{(dto.Email)}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void Update(int id, SaveBranch dto)
        {
            using (SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("LibreriaDb")))
            {
                connection.Open();

                string sql = $"UPDATE Branch " +
                             $"SET Name = '{(dto.Name)}', " +
                             $"Address = '{(dto.Address)}',  " +
                             $"City = '{(dto.City)}', " +
                             $"Phone = '{(dto.Phone)}', " +
                             $"Email = '{(dto.Email)}' " +
                             $"WHERE BranchId = {(id)}";

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

                string sql = $"DELETE FROM Branch " +
                             $"WHERE BranchId = {(id)}";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }

    public interface IBranchService
    {
        IList<Branch> Get();

        void Save(SaveBranch dto);

        void Update(int id, SaveBranch dto);

        void Delete(int id);

    }
}
