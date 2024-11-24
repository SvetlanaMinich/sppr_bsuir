using Microsoft.AspNetCore.Mvc;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.API.Services.CupService;
using Microsoft.AspNetCore.Authorization;

namespace WEB_253503_MINICH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CupsController : ControllerBase
    {
        
        private readonly ICupService _cupService;

        public CupsController(ICupService cupService)
        {
            _cupService = cupService;
        }

        // GET: api/Cups
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseData<ProductListModel<Cup>>>> GetCups(string? category, [FromQuery]int pageNo = 1, [FromQuery]int pageSize = 3)
        {
            var response = await _cupService.GetCupListAsync(category, pageNo, pageSize);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<ResponseData<ProductListModel<Cup>>>(response);
        }

        // GET: api/Cups/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseData<Cup>>> GetCup(int id)
        {
            var response = await _cupService.GetCupByIdAsync(id);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<ResponseData<Cup>>(response);
        }

        // PUT: api/Cups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        [Authorize(Policy = "admin")]
        public async Task<ActionResult<ResponseData<bool>>> PutCup(int id, Cup cup)
        {
            if (id != cup.Id)
            {
                return BadRequest();
            }

            await _cupService.UpdateCupAsync(id, cup);

            return new ActionResult<ResponseData<bool>>(Ok());
        }

        // POST: api/Cups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "admin")]
        public async Task<ActionResult<ResponseData<Cup>>> PostCup(Cup cup)
        {
            var response = await _cupService.CreateCupAsync(cup);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<ResponseData<Cup>>(response);
        }

        // DELETE: api/Cups/5
        /*[HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCup(int id)
        {
            var response = await _cupService.DeleteCupAsync(id);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<bool>(response.Data);
        }*/

        /*private bool CupExists(int id)
        {
            return _context.Cups.Any(e => e.Id == id);
        }*/
    }
}
