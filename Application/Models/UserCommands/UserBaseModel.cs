namespace Application.Models.UserCommands
{
    public class UserBaseModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
