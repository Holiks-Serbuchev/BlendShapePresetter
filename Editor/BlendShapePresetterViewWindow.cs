using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class BlendShapePresetterViewWindow
{
    private GameObject _copyToObject;
    private string _searchField = string.Empty;
    private IEnumerable<string> _searchModel;
    private BlendShapePresetterStorage _blendShapePresetterStorage;
    private Vector2 _scrollPosition = Vector2.zero;
    public BlendShapePresetterViewWindow(BlendShapePresetterStorage blendShapePresetterStorage = null)
    {
        _blendShapePresetterStorage = blendShapePresetterStorage;
    }
    public void ShowWindow(BlendShapePresetterSettingsWindow blendShapePresetterSettings = null)
    {
        if (false)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Search:", GUILayout.Width(50));
            _searchField = GUILayout.TextField(_searchField, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
        }    
        GUILayout.Label("Select the object you want to copy blendshapes to:");
        _copyToObject = EditorGUILayout.ObjectField(_copyToObject, typeof(GameObject), true, GUILayout.Height(19)) as GameObject;
        GUILayout.Space(5);
        GUILayout.Label("List of saved blendshapes:");
        var blendShapePresetterModel = _blendShapePresetterStorage.GetBlendShapesFromJson();
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
        for (int i = 0; i < blendShapePresetterModel.Count; i++)
        {
            GUILayout.BeginVertical(new GUIStyle(GUI.skin.box));
            GUILayout.Label($"Preset Name: {blendShapePresetterModel[i].FileName}");
            GUILayout.BeginHorizontal();
            if (_copyToObject != null)
            {
                if (GUILayout.Button("Copy to the model"))
                {
                    CopyBlendShapesToModel(blendShapePresetterModel[i].SkinnedMeshRenderers);
                }
            }
            if (GUILayout.Button("Delete"))
            {
                var result = _blendShapePresetterStorage.DeleteBlendShapePreset(blendShapePresetterModel[i].FilePath);
                if (result)
                    EditorUtility.DisplayDialog("Success", "The deletion was completed successfully.", "OK");
                else
                    EditorUtility.DisplayDialog("Error", "An error occurred while deleting.", "OK");
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        GUILayout.EndScrollView();
    }
    private bool CopyBlendShapesToModel(List<SkinnedMeshRendererModel> sourceSkinnedMeshRenderer = null)
    {
        try
        {
            var targetSkinnedMeshes = _copyToObject.GetComponentsInChildren<SkinnedMeshRenderer>().ToList().
            Where(i => i != null && i.sharedMesh != null && i.sharedMesh.blendShapeCount != 0).ToList();

            foreach (var sourceSkinnedMesh in sourceSkinnedMeshRenderer)
            {
                foreach (var targetSkinnedMesh in targetSkinnedMeshes)
                {
                    if (sourceSkinnedMesh.SkinnedMeshRendererName == targetSkinnedMesh.name)
                    {
                        for (int i = 0; i < sourceSkinnedMesh.BlendShapesModel.Count; i++)
                        {
                            var sourceName = sourceSkinnedMesh.BlendShapesModel[i].BlendShapeName;
                            for (int j = 0; j < targetSkinnedMesh.sharedMesh.blendShapeCount; j++)
                            {
                                var targetName = targetSkinnedMesh.sharedMesh.GetBlendShapeName(j);
                                if (sourceName == targetName)
                                {
                                    var targetIndex = targetSkinnedMesh.sharedMesh.GetBlendShapeIndex(targetName);
                                    targetSkinnedMesh.SetBlendShapeWeight(targetIndex, sourceSkinnedMesh.BlendShapesModel[i].BlendShapeWeight);
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
        catch (System.Exception){ return false; }
    }
}
