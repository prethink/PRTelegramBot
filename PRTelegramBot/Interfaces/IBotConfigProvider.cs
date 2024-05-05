namespace PRTelegramBot.Interfaces
{
    public interface IBotConfigProvider
    {
        public void SetConfigPath(string  configPath);  

        public T GetSettings<T>();

        public string GetValueByKey<T>(string key) where T : class;

        public TReturn GetValue<TReturn>(string section);
    }
}
