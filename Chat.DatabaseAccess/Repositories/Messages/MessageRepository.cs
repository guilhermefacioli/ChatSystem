using ChatSystem.Common;
using ChatSystem.Filter;
using ChatSystem.Model.Messages;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.DatabaseAccess
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatContext _context;

        public MessageRepository(ChatContext context)
        {
            _context = context;
        }

        private IQueryable<Message> Messages => _context.Messages.AsQueryable();

        public async Task<ApplicationResult> Create(Message message)
        {
            await _context.Messages.FindAsync(message.Id);

            message.DateCreated = DateTime.UtcNow;
            await _context.AddAsync(message);
            await _context.SaveChangesAsync();

            return new ApplicationResult();
        }

        public async Task<ApplicationResult> Delete(Guid id)
        {
            var item = await _context.Messages.FindAsync(id);

            item.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();


            return new ApplicationResult();
        }

        public async Task<ApplicationResult<CollectionResult<Message>>> GetAll(MessageFilter filter, PagingOptions pagingOptions) 
        {
            var query = Messages.Where(x => x.Id == filter.Id);

            if (filter.SearchText != null)
            {
                query = query.Where(x => x.Text == filter.SearchText);
            }

            var items = await query.Skip(pagingOptions.Offset).Take(pagingOptions.Limit).ToListAsync();
            var total = await query.CountAsync();

            var result = new ApplicationResult<CollectionResult<Message>>
            {
                Result = new CollectionResult<Message>
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
