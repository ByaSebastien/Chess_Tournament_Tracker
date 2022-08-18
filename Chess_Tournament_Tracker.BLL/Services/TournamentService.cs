using Chess_Tournament_Tracker.BLL.DTO.Tournaments;
using Chess_Tournament_Tracker.BLL.DTO.Users;
using Chess_Tournament_Tracker.BLL.Exceptions;
using Chess_Tournament_Tracker.BLL.Mappers;
using Chess_Tournament_Tracker.DAL.Repositories;
using Chess_Tournament_Tracker.Models.Entities;
using Chess_Tournament_Tracker.Models.Enums;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public class TournamentService : ITournamentService
    {
        private ITournamentRepository _tournamentRepository;
        private IUserRepository _userRepository;
        public TournamentService(ITournamentRepository tournamentRepository, IUserRepository userRepository)
        {
            _tournamentRepository = tournamentRepository;
            _userRepository = userRepository;
        }
        public Tournament Insert(FormTournamentDTO insertTournament)
        {
            Tournament tournament = insertTournament.ToDAL();
            tournament.Id = Guid.NewGuid();
            tournament.CreationDate = DateTime.Now;
            tournament.UpdateDate = DateTime.Now;
            return _tournamentRepository.Insert(tournament);

        }
        public bool Delete(Guid id)
        {
            Tournament? tournament = _tournamentRepository.FindOne(id);

            if (tournament == null)
                throw new KeyNotFoundException("Doesn't exist");
            if (tournament.Status == TournamentStatus.InProgress)
                throw new TournamentRulesException("Cannot delete a tournament in progress");
            return _tournamentRepository.Delete(tournament);

        }
        public bool Update(FormTournamentDTO updateTournament, Guid id)
        {
            Tournament? tournament = _tournamentRepository.FindOne(id);
            if (tournament is null)
                throw new KeyNotFoundException("Doesn't exist");
            if (updateTournament.Status == TournamentStatus.InProgress)
                throw new TournamentRulesException("Cannot update a tournament in progress");
            tournament = updateTournament.ToDAL(tournament);
            return _tournamentRepository.Update(tournament);
        }
        public FullTournamentDTO GetById(Guid id)
        {
            Tournament tournament = _tournamentRepository.FindDetail(id) ?? throw new KeyNotFoundException("Not Found");
            return new FullTournamentDTO(tournament);
        }
        public IEnumerable<TournamentDTO> GetAllByTen(Guid userId, int offset = 0)
        {
            User? player = _userRepository.FindOne(userId);
            return _tournamentRepository.GetAllByTen(offset).Select(t =>
              new TournamentDTO(t)
              {
                  IsRegister = t.Users.Any(p => p.Id == player?.Id),
                  CanRegister = player is null ? false : CanRegister(player, t),
                  CountPlayer = t.Users.Count
              }
               );
        }
        public void RegisterPlayerInTournament(Guid tournamentId, Guid userId)
        {
            Tournament? tournament = _tournamentRepository.FindOneWithPlayer(tournamentId);
            User? user = _userRepository.FindOne(userId);
            if (tournament is null)
                throw new KeyNotFoundException("Tournament doesn't exist");
            if (user is null)
                throw new KeyNotFoundException("Player doesn't exist");

            if (CanRegister(user, tournament))
            {
                tournament.Users.Add(user);
                _tournamentRepository.Update(tournament);
            }
        }
        public void UnregisterPlayerInTournament(Guid tournamentId, Guid UserId)
        {
            Tournament? tournament = _tournamentRepository.FindOneWithPlayer(tournamentId);
            User? user = _userRepository.FindOne(UserId);
            if (tournament is null)
                throw new KeyNotFoundException("Tournament doesn't exist");
            if (user is null)
                throw new KeyNotFoundException("Player doesn't exist");
            if (tournament.Status != TournamentStatus.WaitingPlayer)
                throw new TournamentRulesException("Tournament has began");
            if (!tournament.Users.Any(p => p.Id == user.Id))
                throw new TournamentRulesException("this player was not register");
            tournament.Users.Remove(user);
            _tournamentRepository.Update(tournament);
        }
        public void StartTournament(Guid id)
        {
            Tournament? tournament = _tournamentRepository.FindOneWithPlayer(id);

            if (tournament is null)
                throw new KeyNotFoundException("Tournament doesn't exist");
            if (tournament.Users.Count < tournament.MinPlayer)
                throw new TournamentRulesException("Not enough players to start the tournament");
            if (tournament.EndInscription < DateTime.Now)
                throw new TournamentRulesException("Inscription not ended");
            List<User> users = tournament.Users.ToList();
            GenerateGames(users, tournament);
            users = tournament.Users.Reverse().ToList();
            GenerateGames(users, tournament);
            tournament.CurrentRound = 1;
            tournament.UpdateDate = DateTime.Now;
            tournament.Status = TournamentStatus.InProgress;
        }
        public void NextRound(Guid id)
        {
            Tournament? tournament = _tournamentRepository.FindOneWithGame(id);
            if (tournament is null)
                throw new KeyNotFoundException("Tournament doesn't exist");
            if (tournament.Games.Any(g => g.Result == GameResult.NotPlayed && g.Round == tournament.CurrentRound))
                throw new TournamentRulesException("Round is not finished");
            tournament.CurrentRound++;
            _tournamentRepository.Update(tournament);
        }
        public TournamentWithScoreDTO GetTournamentWithPlayerResult(Guid tournamentId, int round)
        {
            Tournament? tournament = _tournamentRepository.GetTournamentWithPlayerResult(tournamentId,round);
            if (tournament is null) throw new KeyNotFoundException("Tournament doesnt exist");
            TournamentWithScoreDTO tournamentWithScoreDTO = new TournamentWithScoreDTO(tournament);
            ICollection<PlayerScoreDTO> players = new List<PlayerScoreDTO>();
            foreach (User user in tournament.Users)
            {
                PlayerScoreDTO player = new PlayerScoreDTO(user)
                {
                    GamePlayed = user.GamesAsWhite.Where(g => g.Result != GameResult.NotPlayed).Count() + user.GamesAsBlack.Where(g => g.Result != GameResult.NotPlayed).Count(),
                    GameWon = user.GamesAsWhite.Where(g => g.Result != GameResult.White).Count() + user.GamesAsBlack.Where(g => g.Result != GameResult.Black).Count(),
                    GameLost = user.GamesAsWhite.Where(g => g.Result != GameResult.Black).Count() + user.GamesAsBlack.Where(g => g.Result != GameResult.White).Count(),
                    GameDrawn = user.GamesAsWhite.Where(g => g.Result != GameResult.Draw).Count() + user.GamesAsBlack.Where(g => g.Result != GameResult.Draw).Count(),
                };
                player.Score = player.GameWon + player.GameDrawn / 2;
                players.Add(player);
            }
            tournamentWithScoreDTO.Players = players;
            return tournamentWithScoreDTO;
        }
        private static void GenerateGames(List<User> users, Tournament tournament)
        {
            for (int currentRound = 1; currentRound <= (users.Count - 1); currentRound++)
            {
                for (int currentMatch = 0; currentMatch < users.Count / 2; currentMatch++)
                {
                    Game game = new Game
                    {
                        Id = Guid.NewGuid(),
                        WhiteId = currentMatch % 2 == 0 ? users[currentMatch].Id : users[users.Count() - currentMatch].Id,
                        BlackId = currentMatch % 2 == 0 ? users[users.Count() - currentMatch].Id : users[currentMatch].Id,
                        TournamentId = tournament.Id,
                        Result = GameResult.NotPlayed,
                        Round = currentRound
                    };
                    tournament.Games.Add(game);
                }
                users.Insert(1, users[users.Count - 1]);
                users.RemoveAt(users.Count - 1);
            }
        }
        private static bool CanRegister(User player, Tournament tournament)
        {
            if (tournament.Status != TournamentStatus.WaitingPlayer)
                throw new TournamentRulesException("Tournament has began");
            if (tournament.EndInscription < DateTime.Now)
                throw new TournamentRulesException("Register closed");
            if (tournament.Users.Any(p => p.Id == player.Id))
                throw new TournamentRulesException("Already register");
            if (tournament.Users.Count >= tournament.MaxPlayer)
                throw new TournamentRulesException("Tournament full");
            if (CheckCategories(tournament, player))
                throw new TournamentRulesException("Can't fit in any category");
            if (CheckELO(tournament, player))
                throw new TournamentRulesException("Cant register cause to ELO");
            if (tournament.IsWomenOnly && player.Gender == UserGender.Male)
                throw new TournamentRulesException("Only women can register");
            return true;
        }

        private static int CalculAge(DateTime endInscription, DateTime birthDate)
        {
            int age = endInscription.Year - birthDate.Year;
            if (birthDate > endInscription.AddYears(-age)) age--;
            return age;
        }

        private static bool CheckCategories(Tournament tournament, User player)
        {
            bool flag = true;
            int age = CalculAge(tournament.EndInscription, player.Birthate);
            if (tournament.Category.HasFlag(TournamentCategory.Junior) && age < 18)
                flag = false;
            if (tournament.Category.HasFlag(TournamentCategory.Senior) && age >= 18 && age < 60)
                flag = false;
            if (tournament.Category.HasFlag(TournamentCategory.Veteran) && age >= 60)
                flag = false;
            return flag;
        }

        public static bool CheckELO(Tournament tournament, User player)
        {
            if (tournament.MinELO is not null && player.ELO < tournament.MinELO)
                return true;
            if (tournament.MaxELO is not null && player.ELO > tournament.MaxELO)
                return true;
            return false;
        }
    }
}
