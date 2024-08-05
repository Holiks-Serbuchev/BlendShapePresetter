using UnityEditor;
using UnityEngine;

public class BlendShapePresetterWindow : EditorWindow
{
    private int _toolBarInt = 0;
    private string[] _toolBarStrings = { "Create", "View", "Settings" };
    private BlendShapePresetterStorage _blendShapePresetterStorage;
    private BlendShapePresetterCreateWindow _blendShapePresetterCreateWindow;
    private BlendShapePresetterSettingsWindow _blendShapePresetterSettingsWindow;
    private BlendShapePresetterViewWindow _blendShapePresetterViewWindow;

    public BlendShapePresetterWindow()
    {
        _blendShapePresetterStorage = new BlendShapePresetterStorage();
        _blendShapePresetterSettingsWindow = new BlendShapePresetterSettingsWindow(_blendShapePresetterStorage);
        _blendShapePresetterCreateWindow = new BlendShapePresetterCreateWindow(_blendShapePresetterStorage);
        _blendShapePresetterViewWindow = new BlendShapePresetterViewWindow(_blendShapePresetterStorage);
    }

    [MenuItem("TCExtensions/BlendShapePresetter")]
    public static void BlendShapePresetter()
    {
        GetWindow<BlendShapePresetterWindow>("BlendShapePresetter");
    }

    public void OnGUI()
    {
        _toolBarInt = GUILayout.Toolbar(_toolBarInt, _toolBarStrings);
        switch (_toolBarInt)
        {
            case 0:
                ShowCreateWindow(_blendShapePresetterSettingsWindow);
                break;
            case 1:
                ShowViewWindow(_blendShapePresetterSettingsWindow);
                break;
            case 2:
                ShowSettingsWindow();
                break;
        }
    }
    private void ShowCreateWindow(BlendShapePresetterSettingsWindow blendShapePresetterSettings) => 
        _blendShapePresetterCreateWindow.ShowWindow(blendShapePresetterSettings);
    private void ShowViewWindow(BlendShapePresetterSettingsWindow blendShapePresetterSettings) => 
        _blendShapePresetterViewWindow.ShowWindow(blendShapePresetterSettings);
    private void ShowSettingsWindow() => _blendShapePresetterSettingsWindow.ShowWindow();
}
