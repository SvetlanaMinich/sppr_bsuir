using Microsoft.AspNetCore.Mvc;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.API.Services.CupService;

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
        public async Task<ActionResult<Cup>> GetCup(int id)
        {
            var response = await _cupService.GetCupByIdAsync(id);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<Cup>(response.Data!);
        }

        // PUT: api/Cups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseData<bool>>> PutCup(int id, Cup cup)
        {
            if (id != cup.Id)
            {
                return BadRequest();
            }

            var response = await _cupService.UpdateCupAsync(id, cup);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<ResponseData<bool>>(response);
        }*/

        // POST: api/Cups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<ResponseData<int>>> PostCup(Cup cup)
        {
            var response = await _cupService.CreateCupAsync(cup);

            if (!response.Successfull)
            {
                return NotFound();
            }

            return new ActionResult<ResponseData<int>>(response);
        }*/

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
