using ConfigurationService.Entities.Repositories.Entities;
using ConfigurationService.Entities.Repositories.Helpers.Interfaces;
using IdentityModel;

namespace ConfigurationService.Entities.Helpers
{
    /// <summary>
    /// пока не надо возможно понадобится когда нужно будет записывать клиентов в  бд
    /// </summary>
    public class ClientHelper : IClientsHelper
    {
        private HttpContext _httpContext;
        public ClientHelper(HttpContext HttpContext)
        {
            _httpContext = HttpContext;
        }
        public ClientEntity GetClient()
        {
            if (_httpContext.User.Identity == null || _httpContext.User.Identity.IsAuthenticated == false)
            {
                return new ClientEntity();
            }

            var id = _httpContext.User.FindFirst(JwtClaimTypes.ClientId)?.Value;
            var client = new ClientEntity
            {
                Id = !string.IsNullOrEmpty(id) ? Guid.Parse(id) : null
            };
            return client;
        }
    }
}
