namespace crm_backend.Models
{
    public class Pagination<T>
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
        public List<T> Data { get; set; }
    }
}
