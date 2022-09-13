using ChatSystem.Common;
using ChatSystem.Filter;
using ChatSystem.Model.Images;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.DatabaseAccess.Repositories
{
    internal class ImageRepository : IImageRepository
    {
        private readonly ChatContext _context;

        private IQueryable<Image> Images => _context.Images.AsQueryable();

        public ImageRepository(ChatContext context)
        {
            _context = context;
        }

        public async Task<ApplicationResult> Create(Image image)
        {
            image.DateCreated = DateTime.UtcNow;
            await _context.AddAsync(image);
            await _context.SaveChangesAsync();

            return new ApplicationResult();

        }

        public async Task<ApplicationResult> Update(Image image)
        {
            var item = await _context.Images.FindAsync(image.Id);

            item.Name = image.Name;
            item.Path = image.Path;

            await _context.SaveChangesAsync();

            return new ApplicationResult();
        }

        public async Task<ApplicationResult> Delete(Guid id)
        {
            var item = await _context.Images.FindAsync(id);

            item.DateDeleted = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new ApplicationResult();
        }

        public async Task<ApplicationResult<CollectionResult<Image>>> GetAll(Filters filter, PagingOptions pagingOptions)
        {
            var query = Images;

            if (filter.SearchText != null)
            {
                query = query.Where(x => x.Name == filter.SearchText);
            }

            var items = await query.Skip(pagingOptions.Offset).Take(pagingOptions.Limit).ToListAsync();
            var total = await query.CountAsync();

            var result = new ApplicationResult<CollectionResult<Image>>
            {
                Result = new CollectionResult<Image>
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

        public async Task<ApplicationResult<Image>> Get(Guid id)
        {
            var item = await Images.Where(x => x.Id == id).ToListAsync();

            var result = new ApplicationResult<Image>
            {
                Result = item.FirstOrDefault()
            };

            if (!item.Any())
            {
                result.Errors.Add($"No items found for the id: {id}");
            }
            return result;

        }
    }
}
