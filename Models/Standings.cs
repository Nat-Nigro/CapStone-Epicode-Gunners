namespace HomeTeamWebSite.Models
{
    public class Standings
    {
        public int idStanding { get; set; }
        public int idTeam { get; set; }
        public int intRank { get; set; }
        public string strTeam { get; set; }
        public string strTeamBadge { get; set; }
        public string strForm { get; set; }
        public string strDescription { get; set; }
        public int intPlayed { get; set; }
        public int intWin { get; set; }
        public int intLoss { get; set; }
        public int intDraw { get; set; }
        public int intGoalsFor { get; set; }
        public int intGoalsAgainst { get; set; }
        public int intGoalDifference { get; set; }
        public int intPoints { get; set; }

    }
}