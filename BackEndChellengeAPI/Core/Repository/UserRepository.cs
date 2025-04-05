using System.Data.SqlClient;
using Core.Entities;
using Core.Repository.Settings;

public class UserRepository : BaseRepository
{
    public User GetUserByTaxNumber(string userTaxNumber)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("GetUserByTaxNumber", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@paramTaxNumber", userTaxNumber);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            reader["Name"].ToString(),
                            reader["Password"].ToString(),
                            reader["TaxNumber"].ToString(),
                            reader["Email"].ToString(),
                            (UserType)Enum.Parse(typeof(UserType), reader["Type"].ToString())
                        );
                    }
                    return null;
                }
            }
        }
    }

    public User GetUserByEmail(string email)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("GetUserByEmail", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@paramEmail", email);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            reader["Name"].ToString(),
                            reader["Password"].ToString(),
                            reader["TaxNumber"].ToString(),
                            reader["Email"].ToString(),
                            (UserType)Enum.Parse(typeof(UserType), reader["Type"].ToString())
                        );
                    }
                    return null;
                }
            }
        }
    }

    public void InsertUser(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("InsertUser", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Adiciona os parâmetros à procedure
                command.Parameters.AddWithValue("@paramName", user.Name);
                command.Parameters.AddWithValue("@paramPassword", user.Password);
                command.Parameters.AddWithValue("@paramTaxNumber", user.TaxNumber);
                command.Parameters.AddWithValue("@paramEmail", user.Email);
                command.Parameters.AddWithValue("@paramCreationDate", DateTime.Now);
                command.Parameters.AddWithValue("@paramType", user.Type);

                // Executa o comando
                var result = command.ExecuteScalar();  // Retorna o Id gerado
                if (result != null)
                {
                    int newUserId = Convert.ToInt32(result);
                    Console.WriteLine($"User successfully inserted with ID: {newUserId}");
                }
            }
        }
    }
}
