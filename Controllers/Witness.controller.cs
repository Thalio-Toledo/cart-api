using carrinho_api.DTOs;
using carrinho_api.Entities;
using carrinho_api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace carrinho_api.Controllers
{
    public class WitnessController : BaseController
    {
        private WitnessService _witnessService;

        public WitnessController(WitnessService witnessService)
        {
            _witnessService = witnessService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var witnesses = await _witnessService.GetAll();
            return Ok(witnesses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var witnesses = await _witnessService.FindById(id);
            return Ok(witnesses);
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
