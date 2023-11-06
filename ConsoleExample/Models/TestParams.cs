using PRTelegramBot.Models;
using PRTelegramBot.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExample.Models
{
    internal class TestParams : CustomParameters
    {
        public TestParams()
        {
            InitData();
        }
        public DateTime? Data { get; set; }
        public override void ClearData()
        {
            Data = null;
        }

        public override void InitData()
        {
            Data = DateTime.Now; 
        }
    }
}
