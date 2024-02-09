using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class AuthOutputDto
    {
        public PublicUserDto? User {  get; set; }

        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiaWF0IjoxNjg0OTE3MDc5LCJleHAiOjE2ODYxMjY2Nzl9.s0YPmaDO1nqfgonIK5bEB6RMrgpILd1Fgh5nOTpDvn8</example>
        public string? AccessToken { get; set; }
    }
}
