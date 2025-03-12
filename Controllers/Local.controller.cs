using carrinho_api.Entities;
using carrinho_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace carrinho_api.Controllers
{
    public class LocalController : BaseController
    {
        private LocalService _localService;
        private IDistributedCache _cache;
        private string cacheKey = "Locals";

        public LocalController(LocalService localService, IDistributedCache cache)
        {
            _localService = localService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           
            var cachedLocals = await _cache.GetStringAsync(cacheKey);

            IEnumerable<Local> locals;

            if (cachedLocals is not null)
                locals = JsonSerializer.Deserialize<IEnumerable<Local>>(cachedLocals);
            else
            {
                locals = await _localService.GetAll();
                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(locals), options);
            }

            return Ok(locals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var witnesses = await _localService.FindById(id);
            return Ok(witnesses);
        }

        [ProducesResponseType<bool>((int)HttpStatusCode.Created)]
        [HttpPost]
        public async Task<IActionResult> Create(Local local)
        {
            var res = await _localService.Create(local);
            await _cache.RemoveAsync(cacheKey);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Local local)
        {
            var res = await _localService.Update(local);
            await _cache.RemoveAsync(cacheKey);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _localService.Delete(id);
            await _cache.RemoveAsync(cacheKey);
            
            return Ok(res);
        }
    }
}
