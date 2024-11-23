namespace FrontEnd.Models
{
    public class UserModel
    {
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }

        public virtual string ErrorMessage { get; set; } = string.Empty;
    }
}
