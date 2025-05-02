using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Exceptions;
using Core.Infrastructure.Repository.Base;
using Core.Infrastructure.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Core.Infrastructure.Repository;

public class UserRepository : BaseRepository, IUserRepository
{
    public long Insert(User user)
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

                if (result != null && long.TryParse(result.ToString(), out long newUserId))
                    return newUserId;
                else
                    throw new ApiException("Failed to insert user or retrieve identity.");
            }
        }
    }

    public long Update(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("UpdateUserEmail", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@paramUserId", user.Id);
                command.Parameters.AddWithValue("@paramNewEmail", user.Email);

                var result = command.ExecuteScalar();

                if (result != null && long.TryParse(result.ToString(), out long updatedUserId))
                    return updatedUserId;
                else
                    throw new Exception("Failed to update user or retrieve identity.");
            }
        }
    }

    public void Delete(User user)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("DeleteUser", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@paramUserId", user.Id);

                command.ExecuteNonQuery();  
            }
        }
    }
    
    public User GetById(long userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("GetById", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@paramId", userId);

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
                            (UserType)Enum.Parse(typeof(UserType), reader["Type"].ToString()),
                            DateTime.Parse(reader["CreationDate"].ToString())
                        );
                    }

                    return null!;
                }
            }
        }
    }

    public User GetByTaxNumber(string userTaxNumber)
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
                            (UserType)Enum.Parse(typeof(UserType), reader["Type"].ToString()),
                            DateTime.Parse(reader["CreationDate"].ToString())
                        );
                    }

                    return null!;
                }
            }
        }
    }

    public User GetByEmail(string email)
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
                            (UserType)Enum.Parse(typeof(UserType), reader["Type"].ToString()),
                            DateTime.Parse(reader["CreationDate"].ToString())
                        );
                    }

                    return null!;
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
    
    public void UpdatePassword(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("UpdateUserPassword", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@paramUserId", user.Id);
                command.Parameters.AddWithValue("@paramPassword", user.Password);

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateEmail(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("UpdateUserEmail", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@paramUserId", user.Id);
                command.Parameters.AddWithValue("@paramEmail", user.Email);

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateName(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("UpdateUserName", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@paramUserId", user.Id);
                command.Parameters.AddWithValue("@paramName", user.Name);

                command.ExecuteNonQuery();
            }
        }
    }
}
