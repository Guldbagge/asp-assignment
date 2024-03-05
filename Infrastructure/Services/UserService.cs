

using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositoties;

namespace Infrastructure.Services;

public class UserService(UserRepository repository, AddressService addressService)
{
    private readonly UserRepository _repository = repository;
    private readonly AddressService _addressService = addressService;

    public async Task<ResponseResult> CreateUserAsync(SignUpModel model)
    {
        try
        {
            var exist = await _repository.AlreadyExistsAsync(x => x.Email == model.Email);
            if (exist.StatusCode == StatusCode.EXISTS)
                return exist;

            var result = await _repository.CreateOneAsync(UserFactory.Create(model));
            if (result.StatusCode != StatusCode.EXISTS)
                return result;

            return ResponseFactory.Ok("User was crated succssfully.");



        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> SignInUserAsync(SignInModel model)
    {
        try
        {
            var result = await _repository.GetOneAsync(x => x.Email == model.Email);
            if (result.StatusCode == StatusCode.OK && result.ContentResult != null)
            {
                var userEntity = (UserEntity)result.ContentResult;
                if ((model.Password == userEntity.PasswordHash)) //Test if it works
                    return ResponseFactory.Ok();
            }

            return ResponseFactory.Error("Incorrect email or password.");

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

}


//if (PasswordHasher.ValidateSecurePassword(model.Password, userEntity.Password, userEntity.SecurityKey))