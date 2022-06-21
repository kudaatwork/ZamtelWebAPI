namespace ZamtelWebAPI.Models
{
    public class UserWithToken : User
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Token { get; set; }

        public UserWithToken(User user)
        {
            this.Id = user.Id;
            this.Firstname = user.Firstname;
            this.Middlename = user.Middlename;
            this.Lastname = user.Lastname;
            this.Email = user.Email;
            this.Address = user.Address;
            this.Gender = user.Gender;
            this.DateOfBirth = user.DateOfBirth;
            this.MobileNumber = user.MobileNumber;
            this.DateTimeCreated = user.DateTimeCreated;
            this.DateTimeModified = user.DateTimeModified;
            this.RoleId = user.RoleId;
        }
    }
}
