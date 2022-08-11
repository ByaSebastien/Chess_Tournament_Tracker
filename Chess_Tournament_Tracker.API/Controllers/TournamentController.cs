using Chess_Tournament_Tracker.BLL.DTO.Tournament;
using Chess_Tournament_Tracker.BLL.Exceptions;
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
        public IActionResult Insert(FormTournamentDTO tournament)
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
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TournamentRulesException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Error");
            }
        }
        [HttpPut("{id}")]

        public IActionResult Update(FormTournamentDTO tournament, Guid id)
        {
            try
            {
                _service.Update(tournament, id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TournamentRulesException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}

