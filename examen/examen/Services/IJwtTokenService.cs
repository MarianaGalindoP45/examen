using examen.Models;

namespace examen.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(Usuario usuario);
    }
}
