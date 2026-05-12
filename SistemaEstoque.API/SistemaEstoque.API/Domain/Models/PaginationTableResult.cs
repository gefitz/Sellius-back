namespace Sellius.API.Domain.Models
{
    public class PaginationTableResult<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public List<T>? Dados { get; set; }
        
        
    }
}
