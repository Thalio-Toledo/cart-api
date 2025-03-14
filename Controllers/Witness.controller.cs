﻿using carrinho_api.DTOs;
using carrinho_api.Entities;
using carrinho_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Xml.Linq;

namespace carrinho_api.Controllers
{
    public class WitnessController : BaseController
    {
        private WitnessService _witnessService;
        private readonly IMemoryCache _cache;
        private const string CacheWitnessKey = "CacheWitness";

        public WitnessController(WitnessService witnessService, IMemoryCache cache)
        {
            _witnessService = witnessService;
            _cache = cache;
        }


        [HttpGet("stream")]
        public async IAsyncEnumerable<object> Get()
        {
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(100);
                yield return new { Id = i };
            }
        }

        [HttpGet("streamNum")]
        public async IAsyncEnumerable<int> GetNumbers()
        {
            for (var i = 1; i <= 100; i++)
            {
                yield return i;
                await Task.Delay(100);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!_cache.TryGetValue(CacheWitnessKey, out IEnumerable<WitnessDTO> witnesses))
            {
                witnesses = await _witnessService.GetAll();

                if (witnesses is not null && witnesses.Any()) 
                {
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                        SlidingExpiration = TimeSpan.FromSeconds(15),
                        Priority = CacheItemPriority.High
                    };
                    _cache.Set(CacheWitnessKey, witnesses, cacheOptions);
                }
            }
            return Ok(witnesses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var cacheWitnessKey = $"CacheWitness_{id}";

            if (!_cache.TryGetValue(cacheWitnessKey, out WitnessDTO witness))
            {
                witness = await _witnessService.FindById(id);

                if (witness is not null)
                {
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                        SlidingExpiration = TimeSpan.FromSeconds(15),
                        Priority = CacheItemPriority.High
                    };
                    _cache.Set(cacheWitnessKey, witness, cacheOptions);
                }
            }

            return Ok(witness);
        }

        [ProducesResponseType<bool>((int)HttpStatusCode.Created)]
        [HttpPost]
        public async Task<IActionResult> Create(WitnessCreateDTO witnessDTO)
        {
            var res = await _witnessService.Create(witnessDTO);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Witness witness)
        {
            var res = await _witnessService.Update(witness);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _witnessService.Delete(id);

            return Ok(res);
        }
    }
}
