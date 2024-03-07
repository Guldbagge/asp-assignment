//using Infrastructure.Contexts;
//using Infrastructure.Entities;
//using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;

//namespace Infrastructure.Services
//{
//    public class BasicManager
//    {
//        private readonly DataContext _context;

//        public BasicManager(DataContext context)
//        {
//            _context = context;
//        }

//        public async Task<UserEntity> GetUserAsync(string Id)
//        {
//            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
//            return userEntity!;
//        }

//        public async Task<bool> CreateUserAsync(UserEntity entity)
//        {
//            _context.Users.Add(entity);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> UpdateUserAsync(UserEntity entity)
//        {
//            var existing = await _context.Users.FirstOrDefaultAsync(x => x.Id == entity.Id);
//            if (existing != null)
//            {
//                _context.Entry(existing).CurrentValues.SetValues(entity);
//                await _context.SaveChangesAsync();
//                return true;
//            }
//            return false;
//        }
//    }
//}
