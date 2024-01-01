using PRTelegramBot.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExample.Models
{
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
