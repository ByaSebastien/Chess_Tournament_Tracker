using Chess_Tournament_Tracker.API.Extensions;
using Chess_Tournament_Tracker.BLL.DTO.Tournaments;
using Chess_Tournament_Tracker.BLL.Exceptions;
using Chess_Tournament_Tracker.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Insert(FormTournamentDTO tournament)
        {
            try
            {
                _service.Insert(tournament);
                return Ok(tournament);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
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
                throw;
            }
        }
        [Authorize("Auth")]
        [Authorize("Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(FormTournamentDTO tournament, Guid id)
        {
            try
            {
                _service.Update(tournament, id);
                return NoContent();
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
                throw;
            }
        }
        [HttpGet("{offset}")]
        public IActionResult GetAllByTen(int offset = 0)
        {
            IEnumerable<TournamentDTO> tournaments = _service.GetAllByTen(User.GetId(),offset);
            return Ok(tournaments);
        }
        [HttpGet("id/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                DetailTournamentDTO t = _service.GetById(id);
                return Ok(t);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("Auth")]
        [HttpPost("id/{tournamentId}")]
        public IActionResult RegisterPlayerInTournament(Guid tournamentId)
        {
            try
            {
                _service.RegisterPlayerInTournament(tournamentId, User.GetId());
                return NoContent();
            }
            catch(TournamentRulesException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("Auth")]
        [HttpDelete("Unregister/{tournamentId}/{id}")]
        public IActionResult UnRegisterPlayerInTournament(Guid tournamentId)
        {
            try
            {
                _service.UnregisterPlayerInTournament(tournamentId, User.GetId());
                return NoContent();
            }
            catch (TournamentRulesException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

