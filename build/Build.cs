using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Nuget);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter] string NugetApiUrl = "https://api.nuget.org/v3/index.json";
    [Parameter] string NugetApiKey;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath OutputDirectory => RootDirectory / "output";

    AbsolutePath TestProject => RootDirectory / "src" / "Xamarin.Responsive.Tests" / "Xamarin.Responsive.Tests.csproj";

    AbsolutePath MainProject => RootDirectory / "src" / "Xamarin.Responsive" / "Xamarin.Responsive.csproj";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Test => _ => _
        .Executes(() =>
        {
            DotNetTest(c => c
                .SetProjectFile(TestProject)
                .SetConfiguration(Configuration));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .DependsOn(Test)
        .Executes(() =>
        {
            MSBuild(c => c
               .SetProjectFile(MainProject)
               .SetConfiguration(Configuration)
               .SetAssemblyVersion(GitVersion.AssemblySemVer)
               .SetFileVersion(GitVersion.AssemblySemFileVer)
               .SetInformationalVersion(GitVersion.InformationalVersion)
               .SetRestore(true));
        });

    Target Nuget => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetPack(s => s
             .SetProject(MainProject)
             .SetConfiguration(Configuration)
             .SetPackageId("Xamarin.Responsive")
             .SetVersion(GitVersion.FullSemVer)
             .SetAuthors("Yau Goh Chow")
             .SetDescription("A simple Responsive Grid for Xamarin Forms inspired by Twitter Bootstrap.")
             .SetTitle("Xamarin.Responsive")
             .SetPackageTags("Xamarin", "Xamarin Forms", "Responsive", "Grid", "Bootstrap")
             .SetRepositoryUrl("https://github.com/YauGoh/Xamarin.Responsive")
             .SetPackageProjectUrl("https://github.com/YauGoh/Xamarin.Responsive")
             .SetPackageLicenseUrl("https://licenses.nuget.org/MIT")
             .SetOutputDirectory(OutputDirectory)
            );
        });

    Target Push => _ => _
       .DependsOn(Nuget)
       .Requires(() => NugetApiUrl)
       .Requires(() => NugetApiKey)
       .Requires(() => Configuration.Equals(Configuration.Release))
       .Executes(() =>
       {
           GlobFiles(OutputDirectory, "*.nupkg")
               .NotEmpty()
               .Where(x => !x.EndsWith("symbols.nupkg"))
               .ForEach(x =>
               {
                   DotNetNuGetPush(s => s
                       .SetTargetPath(x)
                       .SetSource(NugetApiUrl)
                       .SetApiKey(NugetApiKey)
                   );
               });
       });

}
