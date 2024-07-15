using System.Reflection;

namespace ArchitectureTests;

public abstract class PostBaseTest
{
    protected static readonly Assembly DomainAssembly = Modules.Posts.Domain.AssemblyReference.Assembly;
    protected static readonly Assembly ApplicationAssembly = Application.AssemblyReference.Assembly;
    protected static readonly Assembly InfrastructureAssembly = Infrastructure.AssemblyReference.Assembly;
    protected static readonly Assembly PresentationAssembly = Presentation.AssemblyReference.Assembly;
}
