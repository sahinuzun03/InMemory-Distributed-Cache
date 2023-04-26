using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCache.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public void SetName()
        {
            _memoryCache.Set("name", "sahin"); 
        }

        [HttpGet]
        public string GetName()
        {
            return _memoryCache.Get<string>("name"); 
        }

        [HttpGet]
        public string GetName2()
        {
            if (_memoryCache.TryGetValue<string>("name",out string name))
            {
                return name.Substring(3);
            }
            return "";

        }

        [HttpGet]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.UtcNow, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),//30 sn boyunca burada atılan veriye ulaaşabilirim. Sliding Expiration maksimum 30 sn boyunca olur gibi düşünebilirsiniz.
                SlidingExpiration = TimeSpan.FromSeconds(5), //Eğer bu veriyi 5sn boyunca kullanmazsan bu veri direk silinecektir.
            });  
        }

        [HttpGet]
        public DateTime GettDate()
        {
            return _memoryCache.Get<DateTime>("date");  
        }
    }
}
