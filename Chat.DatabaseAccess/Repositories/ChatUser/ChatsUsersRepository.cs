using ChatSystem.Common;
using ChatSystem.Filter;
using ChatSystem.Model.Chats;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.DatabaseAccess.Repositories.ChatUser
{
    public class ChatsUsersRepository : IChatsUsersRepository
    {
        private readonly ChatContext _context;

        private IQueryable<ChatsUsers> ChatsUsers => _context.ChatsUsers.AsQueryable();

        public ChatsUsersRepository(ChatContext context)
        {
            _context = context;
        }

        public async Task<ApplicationResult> Create(ChatsUsers chatsUsers)
        {
            await _context.AddAsync(chatsUsers);
            await _context.SaveChangesAsync();

            return new ApplicationResult();
        }

        public async Task<ApplicationResult> Delete(Guid id)
        {
            var item = await _context.ChatsUsers.FindAsync(id);

            item.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ApplicationResult();
        }

        public async Task<ApplicationResult<CollectionResult<ChatsUsers>>> GetAll(ChatsUsersFilter filter, PagingOptions pagingOptions)
        {
            var query = ChatsUsers;

            if (filter.UserId != null)
            {
                query = query.Where(x => x.UserId == filter.UserId);
            }
            
            query = query.Where(x => x.ChatId == filter.ChatId);


            var items = await query.Skip(pagingOptions.Offset).Take(pagingOptions.Limit).ToListAsync();
            var total = await query.CountAsync();

            var result = new ApplicationResult<CollectionResult<ChatsUsers>>
            {
                Result = new CollectionResult<ChatsUsers>
                {
                    Total = total,
                    Items = items
                }
            };

            if (total == 0)
            {
                result.Errors.Add("No items found for the specified criteria");
            }

            return result;

        }
    }
}
