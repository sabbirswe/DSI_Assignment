namespace BlazorApp.Model
{
    public class PagedResponse<T>
    {
        public T Data { get; set; }
        public int TotalRows { get; set; }
    }
}
