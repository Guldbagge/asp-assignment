using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Migrations;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositoties;

public class AccountRepository(DataContext context) : Repo<UserEntity>(context)
{
    private readonly DataContext _context = context;

    public async Task<ResponseResult> UpdateOneAsync(UserEntity entity)
    {
        try
        {
            _context.Set<UserEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return ResponseFactory.Ok(entity);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

}

