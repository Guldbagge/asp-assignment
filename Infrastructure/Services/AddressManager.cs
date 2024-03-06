
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositoties;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AddressManager(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<AddressEntity> GetAddressAsync(string UserId)
    { 
        var addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == UserId);
        return addressEntity!;
    
    }

    public async Task<bool> CreateAddressAsync(AddressEntity entity)
    {
       _context.Addresses.Add(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAddressAsync(AddressEntity entity)
    {
        var existing = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == entity.UserId);
        if (existing != null)
        {
           _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}



//public class AddressService(AddressRepository repository)
//{
//    private readonly AddressRepository _repository = repository;


//    public async Task<ResponseResult> GetOrCreateAddressAsync(string Addressline_1, string postalCode, string city)
//    {
//        try
//        {
//            var result = await GetAddressAsync(Addressline_1, postalCode, city);
//            if (result.StatusCode == StatusCode.NOT_FOUND)

//                result = await CreateAddressAsync(Addressline_1, postalCode, city);
//            return result;

//        }
//        catch (Exception ex)
//        {
//            return ResponseFactory.Error(ex.Message);
//        }
//    }


//    public async Task<ResponseResult> CreateAddressAsync(string Addressline_1, string postalCode, string city)
//    {
//        try

//        {
//            var exist = await _repository.AlreadyExistsAsync(x => x.Addressline_1 == Addressline_1 && x.PostalCode == postalCode && x.City == city);
//            if (exist == null)
//            {
//                var result = await _repository.CreateOneAsync(AddressFactory.Create(Addressline_1, postalCode, city));

//                if (result.StatusCode == StatusCode.OK)

//                    return ResponseFactory.Ok(AddressFactory.Create((AddressEntity)result.ContentResult!));

//                return result;
//            }

//            return exist;
//        }
//        catch (Exception ex)
//        {
//            return ResponseFactory.Error(ex.Message);
//        }

//    }

//    public async Task<ResponseResult> GetAddressAsync(string Addressline_1, string postalCode, string city)
//    {
//        try
//        {
//            var result = await _repository.GetOneAsync(x => x.Addressline_1 == Addressline_1 && x.PostalCode == postalCode && x.City == city);
//            return result;
//        }
//        catch (Exception ex)
//        {
//            return ResponseFactory.Error(ex.Message);
//        }
//    }

//}