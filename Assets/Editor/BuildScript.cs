using System.Collections.Generic;
using UnityEditor;

public class BuildScript
{
    private const string BUILD_ROOT = "build/";
    private const string BUILD_CLIENT_PATH = BUILD_ROOT + "client/";

    [MenuItem("Builder/Build")]
    public static void BuildClient()
    {
        BuildClientNumber();
    }

    private static void BuildClientNumber()
    {
        BuildPlayerOptions buildOption = new BuildPlayerOptions();
        buildOption.locationPathName = $"{BUILD_CLIENT_PATH}/client.exe";
        buildOption.scenes = GetBuildSceneList();
        buildOption.target = BuildTarget.StandaloneWindows64;
        buildOption.options = BuildOptions.AutoRunPlayer;
        BuildPipeline.BuildPlayer(buildOption);
    }

    private static string[] GetBuildSceneList()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        List<string> listScenePath = new List<string>();
        for (int i = 0; i < scenes.Length; ++i)
        {
            if (scenes[i].enabled)
            {
                listScenePath.Add(scenes[i].path);
            }
        }

        return listScenePath.ToArray();
    }
}
