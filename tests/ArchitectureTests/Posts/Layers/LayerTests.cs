﻿namespace ArchitectureTests.Posts.Layers;

public sealed class LayerTests : PostBaseTest
{
    [Fact]
    public void DomainLayer_Should_NotHaveDependencyOn_ApplicationLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .Or()
            .NotHaveDependencyOn(MainApplicationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .Or()
            .NotHaveDependencyOn(MainInfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(ApplicationAssembly)
           .Should()
           .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
           .Or()
           .NotHaveDependencyOn(MainInfrastructureAssembly.GetName().Name)
           .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(ApplicationAssembly)
           .Should()
           .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
           .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(InfrastructureAssembly)
           .Should()
           .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
           .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
