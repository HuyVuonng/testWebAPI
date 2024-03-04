namespace TestApi
{
    static class GetValueAppSetting
    {
        public static IConfiguration AppSetting { get; }
        static GetValueAppSetting()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
