using Microsoft.AspNetCore.Mvc;
using WebCrudAdvanced.Models.MUser;
using WebCrudAdvanced.Services.SUser;

namespace WebCrudAdvanced.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
        {
            //Create User
            routes.MapPost("/register", async ([FromBody] User user, IUserService userService) =>
            {
                // Validação básica dos dados de entrada
                if (user == null)
                {
                    return Results.BadRequest("User data is required.");
                }

                if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                {
                    return Results.BadRequest("Username and Password are required.");
                }

                try
                
                {
                    //User user1 = new User() { Username = user.Username, PasswordHash = user.Password };
                    // Chama o serviço de criação de usuário
                    var result = await userService.CreateUser(user);

                    if (result.Success)
                    {
                        // Retorna 201 Created com o ID do usuário recém-criado
                        return Results.Created($"/users/{result.UserId}", new { UserId = result.UserId });
                    }

                    // Retorna 400 Bad Request com a mensagem de erro, caso falhe
                    return Results.BadRequest(result.Message);
                }
                catch (Exception ex)
                {
                    // Captura erro inesperado e retorna 500 Internal Server Error
                    return Results.StatusCode(500);
                }
            });

            //Login User
            routes.MapPost("/login", async ([FromBody] User user, IUserService userService) =>
            {
                // Chama o serviço de autenticação
                var response = await userService.Authenticate(user);

                if (response != null)
                {
                    // Se o token for válido, retorna 200 OK com o token JWT
                    return response;
                }

                // Se não, retorna 401 Unauthorized
                return Results.Unauthorized();
            });
        }

    }
}
