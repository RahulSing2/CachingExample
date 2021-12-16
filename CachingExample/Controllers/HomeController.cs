using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CachingExample.Controllers
{
    [Route("Home/Index")]
    public class HomeController : Controller
    {
        private IMemoryCache memorycache;

        public HomeController(IMemoryCache memorycache)
        {
            this.memorycache = memorycache;
        }

        public IActionResult Index()
        {
            DateTime currentTime;
            bool AlreadyExit = memorycache.TryGetValue("cachetime", out currentTime);
            if(!AlreadyExit)
            {
                currentTime = DateTime.Now;
                var catchEntryOptions = new MemoryCacheEntryOptions().
                SetSlidingExpiration(TimeSpan.FromSeconds(10));
                memorycache.Set("cachetime", currentTime, catchEntryOptions);
            }
            return View(currentTime);
        }
    }
}
