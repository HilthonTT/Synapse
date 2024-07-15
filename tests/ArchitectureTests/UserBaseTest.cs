using System.Reflection;

namespace ArchitectureTests;

public abstract class UserBaseTest
{
    protected static readonly Assembly MainApplicationAssembly = Application.AssemblyReference.Assembly;
    protected static readonly Assembly MainInfrastructureAssembly = Infrastructure.AssemblyReference.Assembly;
    protected static readonly Assembly PresentationAssembly = Presentation.AssemblyReference.Assembly;

    protected static readonly Assembly DomainAssembly = Modules.Users.Domain.AssemblyReference.Assembly;
    protected static readonly Assembly ApplicationAssembly = Modules.Users.Application.AssemblyReference.Assembly;
    protected static readonly Assembly InfrastructureAssembly = Modules.Users.Infrastructure.AssemblyReference.Assembly;
}
