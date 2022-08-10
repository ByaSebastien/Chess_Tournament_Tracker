using Chess_Tournament_Tracker.BLL.DTO.Tournament;
using Chess_Tournament_Tracker.BLL.Mappers;
using Chess_Tournament_Tracker.BLL.Services;
using Chess_Tournament_Tracker.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chess_Tournament_Tracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private ITournamentService _service;

        public TournamentController(ITournamentService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult Insert(InsertTournamentDTO tournament)
        {
            try
            {
                _service.Insert(tournament);
                return Ok(tournament);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]

        public IActionResult Delete(Tournament tournament)
        {
            _service.Delete(tournament);
            return Ok();

        }
    }
}
