using System.Data.SqlClient;
using Core.Entities;
using Core.Repository.Settings;

namespace Core.Repository;

public class AccountRepository : BaseRepository
{
    public Account GetAccountByUserId(long userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("GetAccountByUserId", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@paramUserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var account = new Account
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("AccountId")),
                            User = new User() { Id = reader.GetInt32(reader.GetOrdinal("UserId")) },
                            Balance = reader.GetDecimal(reader.GetOrdinal("Balance")),
                            CreationDate = reader.GetDateTime(reader.GetOrdinal("AccountCreationDate"))
                        };

                        return account;
                    }
                    return null;
                }
            }
        }
    }
}
