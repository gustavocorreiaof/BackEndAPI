using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Infrastructure.Repository.Base;
using System.Data.SqlClient;

namespace Core.Infrastructure.Repository;

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
                            reader.GetInt32(reader.GetOrdinal("Id")),
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
                            reader.GetInt32(reader.GetOrdinal("Id")),
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

                command.Parameters.AddWithValue("@paramName", user.Name);
                command.Parameters.AddWithValue("@paramPassword", user.Password);
                command.Parameters.AddWithValue("@paramTaxNumber", user.TaxNumber);
                command.Parameters.AddWithValue("@paramEmail", user.Email);
                command.Parameters.AddWithValue("@paramCreationDate", DateTime.Now);
                command.Parameters.AddWithValue("@paramType", user.Type);

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    int newUserId = Convert.ToInt32(result);
                    Console.WriteLine($"User successfully inserted with ID: {newUserId}");
                }
            }
        }
    }

    public List<User> GetAllUsers()
    {
        var users = new List<User>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("GetAllUsers", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate"))
                        };

                        users.Add(user);
                    }
                }
            }
        }

        return users;
    }

    public void UpdateUser(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("UpdateUserEmail", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //command.Parameters.AddWithValue("@paramUserId", userId);
                //command.Parameters.AddWithValue("@paramNewEmail", newEmail);

                var result = command.ExecuteScalar();
            }
        }
    }
}
