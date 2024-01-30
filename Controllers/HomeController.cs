using CasheOrnekProje.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Diagnostics;

namespace CasheOrnekProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _cache;

        public HomeController(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            DateTime cacheEntry;
            string cacheKey = "zaman";
            if (!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                cacheEntry = DateTime.Now;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1))
    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);

            }

            return View(cacheEntry);
        }


    }
}
