using System;
using System.ComponentModel.DataAnnotations;

namespace HomeTeamWebSite.Models
{
    public class Players
    {
        public int idPlayer { get; set; }
        public string strNationality { get; set; }
        public string strPlayer { get; set; }
        public string strNumber { get; set; }
        public string strPosition { get; set; }
        public string strHeight { get; set; }
        public string strtSide { get; set; }
        public string strTeam { get; set; }
        public string strTeam2 { get; set; }
        public string strKit { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateBorn { get; set; }
        public string strBirthLocation { get; set; }
        public string strWage { get; set; }
        public string strSigning { get; set; }
        public string strAgent { get; set; }
        public string strCutout { get; set; }
        public string strtThumb { get; set; }
    }
}