using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using Core.Infrastructure.Repository.Base;
using Core.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public User GetById(long id) => _context.User.AsNoTracking().FirstOrDefault(u => u.Id == id);

    public User GetByEmail(string email) =>
        _context.User.FirstOrDefault(u => u.Email == email);

    public User GetByTaxNumber(string taxNumber)
    {
        return _context.User.FirstOrDefault(u => u.TaxNumber == taxNumber);
    }

    public List<User> GetAllUsers() => _context.User.ToList();

    public long Insert(User user)
    {
        _context.User.Add(user);
        _context.SaveChanges();

        return user.Id;
    }

    public long Update(User user)
    {
        _context.User.Update(user);
        _context.SaveChanges();

        return user.Id;
    }

    public void Delete(User user)
    {
        _context.User.Remove(user);
        _context.SaveChanges();
    }

    public decimal GetUserBalance(long userId)
    {
        var balance = _context.User
            .Join(_context.Account,
                  user => user.Id,
                  account => account.UserId,
                  (user, account) => new { user.Id, account.Balance })
            .Where(x => x.Id == userId)
            .Select(x => x.Balance)
            .FirstOrDefault();

        return balance;
    }

    public void AddBalance(long id, decimal value)
    {
        var account = _context.Account.FirstOrDefault(a => a.UserId == id);

        if (account == null)
            throw new ApiException(ApiMsg.EX012);
        else
        {
            account.Balance += value;
            _context.SaveChanges();
        }
    }
}