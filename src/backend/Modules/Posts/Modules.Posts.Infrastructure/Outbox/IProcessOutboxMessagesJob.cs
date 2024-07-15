namespace Modules.Posts.Infrastructure.Outbox;

public interface IProcessOutboxMessagesJob
{
    Task ProcessAsync();
}
