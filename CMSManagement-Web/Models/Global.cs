using System.Net.Http.Headers;

namespace CMSManagement_Web.Models
{
    public class Global
    {

        private readonly IHttpContextAccessor _accessor;

        public Global(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public HttpClient HttpClients()
        {
            var client = new HttpClient();
            string token = _accessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri("https://localhost:7053/api/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
