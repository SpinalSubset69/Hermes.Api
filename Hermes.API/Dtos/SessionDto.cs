namespace Hermes.API.Dtos
{
    public class SessionDto
    {
        public string Token { get; set; }
        public string ExpiresIn { get; set; }

        public SessionDto(string token, string expiresIn)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }    

        public SessionDto()
        {

        }
    }
}
