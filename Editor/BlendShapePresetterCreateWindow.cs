using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BlendShapePresetterCreateWindow
{
    private GameObject _gameObject;
    private List<SkinnedMeshRenderer> _skinnedMeshRenderers;
    private BlendShapePresetterStorage _blendShapePresetterStorage;
    private Vector2 _scrollPosition = Vector2.zero;
    public BlendShapePresetterCreateWindow(BlendShapePresetterStorage blendShapePresetterStorage = null)
    {
        _blendShapePresetterStorage = blendShapePresetterStorage;
    }
    public void ShowWindow(BlendShapePresetterSettingsWindow blendShapePresetterSettings = null)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Select the object you want to copy the blendshape from:");
        if (GUILayout.Button($"Auto Mode: {(blendShapePresetterSettings.autoDetectBlendShapes == true ? "On" : "Off")}",
            new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight }))
                blendShapePresetterSettings.autoDetectBlendShapes = !blendShapePresetterSettings.autoDetectBlendShapes;
        GUILayout.EndHorizontal();
        _gameObject = EditorGUILayout.ObjectField(_gameObject, typeof(GameObject), true, GUILayout.Height(19)) as GameObject;
        if (_gameObject != null)
        {
            GUILayout.Space(5);
            GUILayout.Label("Select the object that has blendshapes on it:");
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            if (blendShapePresetterSettings.autoDetectBlendShapes)
                _skinnedMeshRenderers = GetSkinnedMeshRenderersFromObject();
            for (int i = 0; i < _skinnedMeshRenderers.Count; i++)
            {
                GUILayout.BeginHorizontal();
                _skinnedMeshRenderers[i] = EditorGUILayout.ObjectField(_skinnedMeshRenderers[i], typeof(SkinnedMeshRenderer), 
                    true, GUILayout.Height(19)) as SkinnedMeshRenderer;
                if (_skinnedMeshRenderers.Count() != 1 && !blendShapePresetterSettings.autoDetectBlendShapes)
                {
                    if (GUILayout.Button("Remove"))
                        _skinnedMeshRenderers.RemoveAt(i);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            GUILayout.Label($"Max count object with blendshapes: {GetSkinnedMeshRenderersFromObject().Count()}",
                new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight });
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save"))
                SaveAction(blendShapePresetterSettings);
            if (_skinnedMeshRenderers.Count() != GetSkinnedMeshRenderersFromObject().Count())
            {
                if (GUILayout.Button("Add Row"))
                    _skinnedMeshRenderers.Add(null);
            }
            GUILayout.EndHorizontal();
        }
        else
            _skinnedMeshRenderers = new List<SkinnedMeshRenderer>() { null };
    }

    private List<SkinnedMeshRenderer> GetSkinnedMeshRenderersFromObject()
    {
        if (_gameObject == null)
        {
            return new List<SkinnedMeshRenderer>();
        }
        var skinnedMeshes = _gameObject.GetComponentsInChildren<SkinnedMeshRenderer>().ToList().
            Where(i => i != null && i.sharedMesh != null && i.sharedMesh.blendShapeCount != 0).ToList();
        return skinnedMeshes;
    }

    private void SaveAction(BlendShapePresetterSettingsWindow blendShapePresetterSettings = null)
    {
        bool success = false;
        if (!blendShapePresetterSettings.autoDetectBlendShapes)
            success = _blendShapePresetterStorage.SaveBlendShapesToJson(_gameObject.name, _skinnedMeshRenderers,
                blendShapePresetterSettings.considerEmptyValues);
        else
            success = _blendShapePresetterStorage.SaveBlendShapesToJson(_gameObject.name, _gameObject.GetComponentsInChildren<SkinnedMeshRenderer>().ToList(),
                blendShapePresetterSettings.considerEmptyValues);

        if (success)
            EditorUtility.DisplayDialog("Success", "The save was completed successfully.", "OK");
        else
            EditorUtility.DisplayDialog("Error", "An error occurred while saving.", "OK");
    }
}
