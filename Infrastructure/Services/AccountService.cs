//using Infrastructure.Entities;
//using Infrastructure.Factories;
//using Infrastructure.Models;
//using Infrastructure.Repositoties;

//namespace Infrastructure.Services
//{
//    public class AccountService
//    {
//        private readonly UserRepository _userRepository;
//        private readonly AddressRepository _addressRepository;

//        public AccountService(UserRepository userRepository, AddressRepository addressRepository)
//        {
//            _userRepository = userRepository;
//            _addressRepository = addressRepository;
//        }

//        public async Task<ResponseResult> CreateAccountAsync(AccountCreationModel accountModel)
//        {
//            try
//            {
//                // Check if user already exists
//                var userExists = await _userRepository.AlreadyExistsAsync(x => x.Email == accountModel.Email);
//                if (userExists.StatusCode == StatusCode.EXISTS)
//                    return ResponseFactory.Error("User already exists with this email.");

//                // Create user
//                var userResult = await _userRepository.CreateOneAsync(UserFactory.Create(accountModel));
//                if (userResult.StatusCode != StatusCode.OK)
//                    return userResult;

//                // Create address
//                var addressResult = await _addressRepository.CreateOneAsync(AddressFactory.Create(accountModel));
//                if (addressResult.StatusCode != StatusCode.OK)
//                    return addressResult;

//                return ResponseFactory.Ok("User and address created successfully.");
//            }
//            catch (Exception ex)
//            {
//                return ResponseFactory.Error(ex.Message);
//            }
//        }

//        public async Task<ResponseResult> GetAccountDetailsAsync(int userId)
//        {
//            try
//            {
//                var userResult = await _userRepository.GetOneAsync(userId);
//                if (userResult.StatusCode != StatusCode.OK)
//                    return userResult;

//                var addressResult = await _addressRepository.GetOneAsync(x => x.UserId == userId);
//                if (addressResult.StatusCode != StatusCode.OK)
//                    return addressResult;

//                // Combine user and address info and return as a response
//                var userDetails = new UserDetailsModel
//                {
//                    User = (UserEntity)userResult.ContentResult,
//                    Address = (AddressEntity)addressResult.ContentResult
//                };

//                return ResponseFactory.Ok(userDetails);
//            }
//            catch (Exception ex)
//            {
//                return ResponseFactory.Error(ex.Message);
//            }
//        }
//    }
//}
