namespace WebCrudAdvanced.Models.MUser
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }

        public int UserId { get; set; }

        // Construtores
        public AuthenticationResult()
        {
        }

        public AuthenticationResult(bool success, string message = null, string token = null, int userId = 0)
        {
            Success = success;
            Message = message;
            Token = token;
            UserId = userId;
        }
    }
}
