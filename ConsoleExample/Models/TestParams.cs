using PRTelegramBot.Models;

namespace ConsoleExample.Models
{
    public class TestParams 
    {
        public TestParams()
        {
            InitData();
        }
        public DateTime? Data { get; set; }
        public string DataOne { get; set; }
        public string DataTwo { get; set; }
        public void ClearData()
        {
            DataOne = "";
            DataTwo = "";
            Data = null;
        }

        public void InitData()
        {
            Data = DateTime.Now; 
            
        }
    }
}
