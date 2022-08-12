using Chess_Tournament_Tracker.API.Extensions;
using Chess_Tournament_Tracker.BLL.DTO.Tournaments;
using Chess_Tournament_Tracker.BLL.Exceptions;
using Chess_Tournament_Tracker.BLL.Mappers;
using Chess_Tournament_Tracker.BLL.Services;
using Chess_Tournament_Tracker.Models.Entities;
using Microsoft.AspNetCore.Http;
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
                return BadRequest("Error");
            }
        }
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
        }
        [HttpGet]
        public IActionResult GetLastTenTournamentsInProgressOnDateDescending()
        {
            IEnumerable<LastTenTournamentsInProgressOnDateDescendingDTO> tournaments = _service.GetLastTenTournamentsInProgressOnDateDescending();
            return Ok(tournaments);
        }

        [HttpPost("tournament/{tournamentId}")]
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
        [HttpDelete("tournament/{tournamentId}")]
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
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_service.GetById(id));                
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

