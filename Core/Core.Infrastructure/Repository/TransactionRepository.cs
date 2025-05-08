using Core.Domain.Entities;
using Core.Infrastructure.Repository.Base;
using Core.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace Core.Infrastructure.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;
    private readonly string _connectionString;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
        _connectionString = _context.Database.GetConnectionString();
    }

    public List<Transaction> GetTransactionsByUserId(long userId, DateTime? startDate, DateTime? endDate)
    {
        //NOTE: Eu poderia ter feito usando o LINQ assim como nos outros repositories porem ontem eu estava estudando
        //Um curso de topicos avançados e um dos pontos era a consulta feita dessa forma e entao eu quis tentar usar isso

        var query = from t in _context.Transaction 
                    where 
                        (t.PayerId == userId)
                        && (startDate == null || t.TransferDate > startDate)
                        && (endDate == null || t.TransferDate < endDate) select t;

        return query.ToList();
    }

    public void PerformTransaction(User payerId, User payeeId, decimal transferValue)
    {
        //NOTE: Assim como o procedimento anterior, eu poderia fazer isso com linq também porem alguns procedimeentos
        //requerem mais cuidado a nivel de banco de dados em termos de performace ou risco de erros, entao optei por usar diretamente
        //a stored procedure pois consigo controlar melhor sua performace e o tratamento de rollback em caso de erros.
        //Poderia ter sido feito com unity of work também caso desejasse controlar as ações pelo .NET.

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new NpgsqlCommand("CALL \"PerformTransaction\"(@payerId, @payeeId, @transferValue, @transferDate)", connection))
            {
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("payerId", payerId.Id);
                command.Parameters.AddWithValue("payeeId", payeeId.Id);
                command.Parameters.AddWithValue("transferValue", transferValue);
                command.Parameters.AddWithValue("TransferDate", DateTime.UtcNow);

                command.ExecuteNonQuery(); 
            }
        }
    }
}
