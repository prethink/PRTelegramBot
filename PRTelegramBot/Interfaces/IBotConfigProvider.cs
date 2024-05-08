namespace PRTelegramBot.Interfaces
{
    public interface IBotConfigProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configPath"></param>
        public void SetConfigPath(string configPath);  

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSettings<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValueByKey<T>(string key) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="section"></param>
        /// <returns></returns>
        public TReturn GetValue<TReturn>(string section);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetKeysAndValues();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Dictionary<string, string> GetKeysAndValuesBySection<T>();
    }
}
