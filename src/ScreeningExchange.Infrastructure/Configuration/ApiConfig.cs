namespace ScreeningExchange.Infrastructure.Configuration;

#pragma warning disable CS8618
public class ApiConfig
{
    public Email Email { get; set; }
    public WhatsApp WhatsApp { get; set; }
    public BlobStorageConfig BlobStorage { get; set; }
    public FirebaseAuthentication FirebaseAuthentication { get; set; }
}

public class Email
{
    public string Smtp { get; set; }
    public int Port { get; set; }
    public string Login { get; set; }
    public string From { get; set; }
    public string DisplayName { get; set; }
    public string Password { get; set; }
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
#pragma warning restore CS8618