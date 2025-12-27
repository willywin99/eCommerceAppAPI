namespace eCommerceApp.Application.DTOs.Identity
{
    public class CreateUser : BaseModel
    {
        public string Fullname {  get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
