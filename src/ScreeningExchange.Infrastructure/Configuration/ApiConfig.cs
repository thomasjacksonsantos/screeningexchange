namespace ScreeningExchange.Infrastructure.Configuration;

public class ApiConfig
{
    public Email Email { get; set; } = new();
    public WhatsApp WhatsApp { get; set; } = new();
    public BlobStorageConfig BlobStorage { get; set; } = new();
    public FirebaseAuthentication FirebaseAuthentication { get; set; } = new();
}

public class Email
{
    public string Smtp { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Login { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class WhatsApp
{
    public string AccountSid { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
    public string NumberPhone { get; set; } = string.Empty;
}

public class BlobStorageConfig
{
    public string BlobStorageConnectionString { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
}

public class FirebaseAuthentication
{
    public string TokenUri { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
}