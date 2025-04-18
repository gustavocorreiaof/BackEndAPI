﻿using Core.Domain.Entities;
using Core.Infrastructure.Repository.Base;
using System.Data.SqlClient;

namespace Core.Infrastructure.Repository;

public class TransactionRepository : BaseRepository
{
    public void PerformTransaction(User payerId, User payeeId, decimal transferValue)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("PerformTransaction", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@paramPayerId", payerId.Id);
                command.Parameters.AddWithValue("@paramPayeeId", payeeId.Id);
                command.Parameters.AddWithValue("@paramTransferValue", transferValue);
                command.Parameters.AddWithValue("@paramTransferDate", DateTime.Now);

                var result = command.ExecuteScalar();
            }
        }
    }
}
