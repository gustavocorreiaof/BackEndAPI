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
}
