using UnityEngine;

public class BlendShapePresetterSettingsWindow
{
    private const string _version = "1.0";
    public bool autoDetectBlendShapes { get; set; } = true;
    public bool considerEmptyValues { get; private set; } = true;
    private BlendShapePresetterStorage _blendShapePresetterStorage;
    public BlendShapePresetterSettingsWindow(BlendShapePresetterStorage blendShapePresetterStorage = null)
    {
        _blendShapePresetterStorage = blendShapePresetterStorage;
    }
    public void ShowWindow()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("The path to the files:");
        GUILayout.Label($"Tool Version: {_version}", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight });
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.TextField(_blendShapePresetterStorage.folderPath);
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("Rules:");
        autoDetectBlendShapes = GUILayout.Toggle(autoDetectBlendShapes, "Auto detect objects with blendshapes");
        considerEmptyValues = GUILayout.Toggle(considerEmptyValues, "Consider empty values");
        GUILayout.Space(5);
        if (GUILayout.Button("Github Repository"))
            Application.OpenURL("https://github.com/Holiks-Serbuchev/BlendShapePresetter");
    }
}
