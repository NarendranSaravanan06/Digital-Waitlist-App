namespace WaitListApp.Models
{
    public class WaitlistModel
    {
        public int Id { get; set; }  // From DB
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Status { get; set; }
        public bool isloggedin { get; set; }
    }
}
