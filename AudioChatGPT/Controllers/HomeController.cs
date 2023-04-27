using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AudioChatGPT.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public string get()
        {
            return JsonConvert.SerializeObject("sucessessss");
        }
    }
}
