using PRTelegramBot.Interfaces;

namespace TestDI.Models
{
    /// <summary>
    /// Cache used for step-by-step command execution
    /// </summary>
    public class StepCache : ITelegramCache
    {
        public string Name { get; set; }
        public string BirthDay { get; set; }
        public bool ClearData()
        {
            this.BirthDay = string.Empty; 
            this.Name = string.Empty;
            return true;
        }
    }
}
