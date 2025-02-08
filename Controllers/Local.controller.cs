using carrinho_api.Entities;
using carrinho_api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace carrinho_api.Controllers
{
    public class LocalController : BaseController
    {
        private LocalService _localService;

        public LocalController(LocalService localService) {
            _localService = localService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var witnesses = await _localService.GetAll();
            return Ok(witnesses);
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
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Local local)
        {
            var res = await _localService.Update(local);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _localService.Delete(id);

            return Ok(res);
        }
    }
}
