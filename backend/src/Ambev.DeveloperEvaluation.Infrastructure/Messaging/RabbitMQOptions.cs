namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging;

public class RabbitMQOptions
{
    public string HostName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}