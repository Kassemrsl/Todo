namespace Todo.OnlineTaskManagement.Shared.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
