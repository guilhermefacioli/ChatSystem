using ChatSystem.Common;
using ChatSystem.Filter;
using ChatSystem.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.DatabaseAccess.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly ChatContext _context;

        public UserRepository(ChatContext context)
        {
            _context = context;
        }

        private IQueryable<User> Users => _context.Users.AsQueryable();

        public async Task<ApplicationResult> Create(User user)
        {
            user.DateCreated = DateTime.UtcNow;
            user.DateModified = DateTime.UtcNow;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ApplicationResult();
        }

        public async Task<ApplicationResult> Update(User user)
        {
            var item = await _context.Users.FindAsync(user.Id);

            item.Id = user.Id;
            item.Name = user.Name;
            item.Email = user.Email;
            item.DateModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ApplicationResult();
        }

        public async Task<ApplicationResult> Delete(Guid id)
        {
            var item = await _context.Users.FindAsync(id);

            item.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();


            return new ApplicationResult();
        }

        public async Task<ApplicationResult<User>> Get(Guid id)
        {
            var item = await Users.Where(x => x.Id == id).ToListAsync();

            var result = new ApplicationResult<User>
            {
                Result = item.FirstOrDefault()
            };
            if (!item.Any())
            {
                result.Errors.Add($"No items found for the id: {id}");
            }

            return result;
        }

        public async Task<ApplicationResult<CollectionResult<User>>> GetAll(Filters filter, PagingOptions pagingOptions)
        {
            var query = Users;

            if (filter.SearchText != null)
            {
                query = query.Where(x => x.Name == filter.SearchText);
            }

            var items = await query.Skip(pagingOptions.Offset).Take(pagingOptions.Limit).ToListAsync();
            var total = await query.CountAsync();

            var result = new ApplicationResult<CollectionResult<User>>
            {
                Result = new CollectionResult<User>
                {
                    Total = total,
                    Items = items
                }
            };

            if(total == 0)
            {
                result.Errors.Add("No items found for the specified criteria"); 
            }

            return result; 
        }        
    }
}
