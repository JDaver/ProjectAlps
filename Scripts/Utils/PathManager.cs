using System;
using System.IO;

namespace ProjectAlps.Utils.PathManager;

public static class PathManager
{
    public static string Root
    {
        get
        {
            DirectoryInfo? directory =
                new DirectoryInfo(AppContext.BaseDirectory);

            while (directory != null)
            {
                if (Directory.Exists(
                    Path.Combine(directory.FullName, "Assets")))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException(
                "Project root not found"
            );
        }
    }


    public static string WorldArchetypeRules =>
        Path.Combine(
            Root,
            "Assets",
            "GenerationRules",
            "WorldArchetypeRules.json"
        );


    public static string RegionRules =>
        Path.Combine(
            Root,
            "Assets",
            "GenerationRules",
            "RegionRules.json"
        );
}