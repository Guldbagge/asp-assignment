

using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositoties;

public class AddressRepository(DataContext context) : Repo<AddressEntity>(context)
{
    private readonly DataContext _context = context;
}