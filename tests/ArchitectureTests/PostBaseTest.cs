using System.Reflection;

namespace ArchitectureTests;

public abstract class PostBaseTest
{
    protected static readonly Assembly MainApplicationAssembly = Application.AssemblyReference.Assembly;
    protected static readonly Assembly MainInfrastructureAssembly = Infrastructure.AssemblyReference.Assembly;
    protected static readonly Assembly PresentationAssembly = Presentation.AssemblyReference.Assembly;

    protected static readonly Assembly DomainAssembly = Modules.Posts.Domain.AssemblyReference.Assembly;
    protected static readonly Assembly ApplicationAssembly = Modules.Posts.Application.AssemblyReference.Assembly;
    protected static readonly Assembly InfrastructureAssembly = Modules.Posts.Infrastructure.AssemblyReference.Assembly;
}
