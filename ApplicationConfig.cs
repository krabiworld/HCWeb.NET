namespace HCWeb.NET;

public static class ApplicationConfig
{
    private const string DbHost = "ep-tight-poetry-581583.eu-central-1.aws.neon.tech";
    private const string DbUser = "headcrabj1";
    private const string DbPassword = "UF9Ril5htuEK";
    private const string Db = "neondb";
    public const string ConnectionString = $"Host={DbHost};Username={DbUser};Password={DbPassword};Database={Db}";
}