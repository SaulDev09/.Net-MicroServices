namespace SC.Services.EmailAPI.Models
{
    public class EmailLogger
    {
        public int Id { get; set; }
        public string Emai { get; set; }
        public string Message { get; set; }
        public DateTime? EmailSent { get; set; }
    }
}
