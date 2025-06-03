namespace UserAPI.Services.Dto
{
    public class JwtTokenDto
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
        public JwtTokenDto() { }
        public JwtTokenDto(string token, DateTime validTo)
        {
            Token = token;
            ValidTo = validTo;
        }
    }
}
