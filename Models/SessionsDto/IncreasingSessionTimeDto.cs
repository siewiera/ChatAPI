using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.SessionsDto
{
    public class IncreasingSessionTimeDto
    {
        public DateTime LastAction { get; set; }
        public string Ip { get; set; }
    }
}
