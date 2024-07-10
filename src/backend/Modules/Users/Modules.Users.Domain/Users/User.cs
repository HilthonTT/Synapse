using SharedKernel;

namespace Modules.Users.Domain.Users;

public sealed class User : Entity, IAuditableEntity
{
    private User(
        Guid id, 
        ObjectIdentifier objectIdentifier, 
        Name name, 
        Username username, 
        Email email, 
        string imageUrl) 
        : base(id)
    {
        ObjectIdentifier = objectIdentifier;
        Name = name;
        Username = username;
        Email = email;
        ImageUrl = imageUrl;
    }

    private User()
    {
    }

    public ObjectIdentifier ObjectIdentifier { get; private set; }

    public Name Name { get; private set; }

    public Username Username { get; private set; }

    public Email Email { get; private set; }

    public string ImageUrl { get; private set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public static User Create(
        ObjectIdentifier objectIdentifier, 
        Name name, 
        Username username, 
        Email email,
        string imageUrl)
    {
        var user = new User(
            Guid.NewGuid(),
            objectIdentifier,
            name,
            username,
            email,
            imageUrl);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public void Update(Name name, Username username, Email email, string imageUrl)
    {
        Name = name;
        Username = username;
        Email = email;
        ImageUrl = imageUrl;
    }
}
