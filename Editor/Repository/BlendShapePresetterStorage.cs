using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

public class BlendShapePresetterStorage
{
    public string folderPath { get; private set; } = string.Empty;
    public BlendShapePresetterStorage()
    {
        folderPath = $@"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))}\BlendShapePresetter";
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);
    }

    public bool SaveBlendShapesToJson(string name = null, List<SkinnedMeshRenderer> skinnedMeshRenderers = null, bool considerEmptyValues = false)
    {
        try
        {
            var blendShapePresetterModel = BlendShapesMapToModel(name, 
                skinnedMeshRenderers.Where(i => i != null && i.sharedMesh != null && i.sharedMesh.blendShapeCount != 0), considerEmptyValues);
            string filePath = Path.Combine(folderPath, $"{name}.json");
            if (!System.IO.File.Exists(filePath))
                System.IO.File.Create(filePath).Close();
            string json = JsonConvert.SerializeObject(blendShapePresetterModel);
            System.IO.File.WriteAllText(filePath, json);
            return true;
        }
        catch (Exception) { return false; }
    }

    public bool DeleteBlendShapePreset(string path)
    {
        try
        {
            if (File.Exists(path))
                File.Delete(Path.Combine(folderPath, path));
            else
                return false;
            return true;
        }
        catch (Exception){ return false; }
    }

    public List<FileModel> GetBlendShapesFromJson()
    {
        List<FileModel> blendShapePresetterModel = new List<FileModel>();
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            foreach (var item in directoryInfo.GetFiles())
            {
                string text = System.IO.File.ReadAllText(Path.Combine(folderPath, item.Name));
                blendShapePresetterModel.Add(new FileModel() {FileName = item.Name, FilePath = item.FullName,
                    SkinnedMeshRenderers = JsonConvert.DeserializeObject<List<SkinnedMeshRendererModel>>(text)});
            }
            return blendShapePresetterModel;
        }
        catch (Exception) { return new List<FileModel>(); }
    }

    private List<SkinnedMeshRendererModel> BlendShapesMapToModel(string name = null, IEnumerable<SkinnedMeshRenderer> skinnedMeshRenderers = null, bool considerEmptyValues = false)
    {
        List<SkinnedMeshRendererModel> skinnedMeshRendererModel = new List<SkinnedMeshRendererModel>();
        foreach (var item in skinnedMeshRenderers)
        {
            var mesh = item.sharedMesh;
            if (mesh != null)
            {
                List<BlendShapesModel> blendShapes = new List<BlendShapesModel>();
                for (int i = 0; i < mesh.blendShapeCount; i++)
                {
                    if (considerEmptyValues)
                    {
                        blendShapes.Add(new BlendShapesModel()
                        {
                            BlendShapeName = mesh.GetBlendShapeName(i),
                            BlendShapeWeight = item.GetBlendShapeWeight(i)
                        });
                    }
                    else
                    {
                        if (item.GetBlendShapeWeight(i) > 0)
                        {
                            blendShapes.Add(new BlendShapesModel()
                            {
                                BlendShapeName = mesh.GetBlendShapeName(i),
                                BlendShapeWeight = item.GetBlendShapeWeight(i)
                            });
                        }
                    }
                }
                if (blendShapes.Count() > 0)
                    skinnedMeshRendererModel.Add(new SkinnedMeshRendererModel() { SkinnedMeshRendererName = item.name,
                        BlendShapesModel = blendShapes });
            }
        }
        return skinnedMeshRendererModel;
    }

}
