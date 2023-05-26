namespace crm_backend.Models
{
    public class BaseApiResponse
    {
        public string? title { get; set; }
        public int status { get; set; }
        public string? message { get; set; }
        public string? detail { get; set; }
    }
}
