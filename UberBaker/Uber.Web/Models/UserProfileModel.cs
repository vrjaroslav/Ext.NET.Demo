namespace Uber.Web.Models
{
    public class UserProfileModel : BaseModel
    {
        public UserModel User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}