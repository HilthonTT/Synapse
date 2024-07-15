using System.Reflection;

namespace ArchitectureTests;

public abstract class UserBaseTest
{
    protected static readonly Assembly DomainAssembly = Modules.Users.Domain.AssemblyReference.Assembly;
    protected static readonly Assembly ApplicationAssembly = Application.AssemblyReference.Assembly;
    protected static readonly Assembly InfrastructureAssembly = Infrastructure.AssemblyReference.Assembly;
    protected static readonly Assembly PresentationAssembly = Presentation.AssemblyReference.Assembly;
}
