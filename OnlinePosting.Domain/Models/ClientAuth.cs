namespace OnlinePosting.Domain.Models
{
    public class ClientAuth
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }
}
