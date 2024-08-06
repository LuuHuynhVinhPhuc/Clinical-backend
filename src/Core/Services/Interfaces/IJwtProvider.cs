using ClinicalBackend.Domain.Entities;

namespace ClinicalBackend.Services.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
