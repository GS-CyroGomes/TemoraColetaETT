using DotNetEnv;

namespace TemoraColetaETT.Infrastructure.Configuration
{
    public static class Env
    {
        static Env()
        {
            DotNetEnv.Env.Load();
        }

        public static string ApiBaseUrl => DotNetEnv.Env.GetString("API_BASE_URL") ?? "";
    }
}
