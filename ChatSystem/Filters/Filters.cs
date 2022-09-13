namespace ChatSystem.Filter
{
    public class Filters
    {
        public Filters(string searchText)
        {
            SearchText = searchText;
        }

        public string SearchText { get; set; }
    }
}
