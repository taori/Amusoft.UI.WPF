using System.IO;

namespace Amusoft.UI.Tests.Utilities
{
	public static class FileUtil
	{
		public static string GetNewTempFolder()
        {
            var path = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
            Directory.CreateDirectory(path);
            return path;
        }
	}
}