using Core.Domain.Entities;
using Core.Infrastructure.Repository.Base;
using Core.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public Account GetAccountByUserId(long userId) => _context.Account.AsNoTracking().FirstOrDefault(a => a.UserId == userId);

    public long InsertAccount(User user)
    {
        Account account = new Account()
        {
            UserId = user.Id,
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            Balance = 0,
            User = user
        };

        _context.Account.Add(account);
        return _context.SaveChanges();
    }
}
