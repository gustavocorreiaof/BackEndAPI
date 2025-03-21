using System.Data.SqlClient;
using Core.Entities;
using Core.Repository.Settings;

public class UserRepository : BaseRepository
{
    public User GetUserByCPF(string userCPF)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("GetUserByCPF", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@paramCPF", userCPF);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            reader["Name"].ToString(),
                            reader["Password"].ToString(),
                            reader["CPF"].ToString(),
                            reader["Email"].ToString()
                        );
                    }
                    return null;
                }
            }
        }
    }
}
