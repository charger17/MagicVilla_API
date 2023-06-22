using MagicVilla_Utilites;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IHttpClientFactory _httpClient;
        private string _villUrl;

        public UsuarioService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _villUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Login<T>(LoginRequestDto dto)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _villUrl + "/api/v1/usuario/login"
            });
        }

        public Task<T> Registrar<T>(RegistroRequestDto dto)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _villUrl + "/api/v1/usuario/registrar"
            });
        }
    }
}
