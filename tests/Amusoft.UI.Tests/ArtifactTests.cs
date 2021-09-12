using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Amusoft.UI.Tests.Utilities;
using Amusoft.UI.WPF.Notifications;
using Shouldly;
using Xunit;

namespace Amusoft.UI.Tests
{
    public class ArtifactTests
    {
        [Fact]
        public void ManifestName_Containts_G_Resources_Folder()
        {
            var assembly = typeof(NotificationHostManager).Assembly;
            var manifestNames = assembly.GetManifestResourceNames();
            manifestNames.ShouldContain("Amusoft.UI.WPF.g.resources");
        }

        [Fact]
        public async Task DotNetPack_ManifestNames_Containts_G_Resources_Folder()
        {
            /**
             * This test makes sure, that the resources are contained in the packaged nuget, which is not the case at all times for some reason.
             */
            string tempFolder = null;
            try
            {
                tempFolder = FileUtil.GetNewTempFolder();
                Directory.Exists(tempFolder).ShouldBeTrue();

                var location = typeof(NotificationHostManager).Assembly.Location;
                var libraryAssemblyLocation = Path.GetDirectoryName(location);
                var buildCsproj = Path.Combine(libraryAssemblyLocation, @"..\..\..\..\..\src\Amusoft.UI.WPF\Amusoft.UI.WPF.csproj");
                buildCsproj = new Uri(buildCsproj).AbsolutePath;
                File.Exists(buildCsproj).ShouldBeTrue();

                using var buildRunner = new SimpleProcessRunner("dotnet", $"build {buildCsproj} -c Release");
                await buildRunner.ExecuteAsync(TimeSpan.FromSeconds(15));
                buildRunner.ExitCode.ShouldBe(0);
                
                
                using var packRunner = new SimpleProcessRunner("dotnet", $"pack {buildCsproj} -o {tempFolder} -c Release");
                await packRunner.ExecuteAsync(TimeSpan.FromSeconds(15));
                packRunner.ExitCode.ShouldBe(0);

                var nupkg = Directory.EnumerateFiles(tempFolder, "*.nupkg", SearchOption.AllDirectories).FirstOrDefault();
                nupkg.ShouldNotBeNull();

                var unzipTarget = FileUtil.GetNewTempFolder();
                ZipFile.ExtractToDirectory(nupkg, unzipTarget);

                var libDll = Directory.EnumerateFiles(unzipTarget + "\\lib", "Amusoft.UI.WPF.dll", SearchOption.AllDirectories).FirstOrDefault();
                File.Exists(libDll).ShouldBeTrue();

                var remoteAssembly = Assembly.LoadFile(libDll);
                remoteAssembly.ShouldNotBeNull();

                remoteAssembly.GetManifestResourceNames().ShouldContain("Amusoft.UI.WPF.g.resources");
            }
            catch (Exception e)
            {
                Assert.True(false, $"Unexpected exception occured. {e.ToString()}.");
            }
            finally
            {
                Directory.Delete(tempFolder, true);
            }
        }
    }
}