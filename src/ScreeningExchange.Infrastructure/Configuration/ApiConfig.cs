namespace ScreeningExchange.Infrastructure.Configuration;

public class ApiConfig
{
    public Email Email { get; set; } = new();
    public BlobStorageConfig BlobStorage { get; set; } = new();
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

public class BlobStorageConfig
{
    public string BlobStorageConnectionString { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
}