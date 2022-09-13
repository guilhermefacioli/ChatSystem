using ChatSystem.Common;
using ChatSystem.Filter;
using ChatSystem.Model.Chats;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.DatabaseAccess.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatContext _context;

        private IQueryable<Chat> Chats => _context.Chats.AsQueryable();

        public ChatRepository(ChatContext context)
        {
            _context = context;
        }


        public async Task<ApplicationResult> Create(Chat chat)
        {
            chat.DateCreated = DateTime.UtcNow;
            chat.DateModified = DateTime.UtcNow;

            await _context.AddAsync(chat);
            await _context.SaveChangesAsync();

            return new ApplicationResult();
        }
        public async Task<ApplicationResult> Update(Chat chat)
        {
            var item = await _context.Chats.FindAsync(chat.Id);

            item.Name = chat.Name;
            item.DateModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ApplicationResult();
        }

        public async Task<ApplicationResult> Delete(Guid id)
        {
            var item = await _context.Chats.FindAsync(id);

            item.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ApplicationResult(); 
        }

        public async Task<ApplicationResult<Chat>> Get(Guid id)
        {
            var item = await Chats.Where(x => x.Id == id).ToListAsync();

            var result = new ApplicationResult<Chat>()
            {
                Result = item.FirstOrDefault()
            };
            if (!item.Any())
            {
                result.Errors.Add($"No items found for the id: {id}");
            }

            return result;

        }

        public async Task<ApplicationResult<CollectionResult<Chat>>> GetAll(Filters filter, PagingOptions pagingOptions)
        {
            var query = Chats;

            if (filter.SearchText != null)
            {
                query = query.Where(x => x.Name == filter.SearchText);
            }

            var items = await query.Skip(pagingOptions.Offset).Take(pagingOptions.Limit).ToListAsync();
            var total = await query.CountAsync();

            var result = new ApplicationResult<CollectionResult<Chat>>
            {
                Result = new CollectionResult<Chat>
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
