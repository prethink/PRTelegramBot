using PRTelegramBot.Models;

namespace ConsoleExample.Models
{
    public class TestParams : CustomParameters
    {
        public TestParams()
        {
            InitData();
        }
        public DateTime? Data { get; set; }
        public string DataOne { get; set; }
        public string DataTwo { get; set; }
        public override void ClearData()
        {
            DataOne = "";
            DataTwo = "";
            Data = null;
        }

        public override void InitData()
        {
            Data = DateTime.Now; 
            
        }
    }
}
