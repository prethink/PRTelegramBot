using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Models.Configs
{
    public abstract class BaseConfig
    {
        public IConfigurationRoot config { get; internal set; }

        public virtual T GetSettings<T>()
        {
            var section = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }

        public static T GetSettings<T>(IConfigurationRoot config)
        {
            var section = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }
    }
}
