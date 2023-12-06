namespace NewsApp.Dto
{
    public class AuthOutputDto
    {
        public PublicUserDto user {  get; set; }
        public string? accessToken { get; set; }
    }
}
