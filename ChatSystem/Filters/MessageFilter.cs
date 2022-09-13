namespace ChatSystem.Filter
{
    public class MessageFilter : Filters
    {
        public MessageFilter(Guid id, string searchText)
            :base(searchText)
        {
            Id = id;
        }
        public Guid Id { get; set; }

    }
}
