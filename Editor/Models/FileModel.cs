using System.Collections.Generic;

public class FileModel
{
    public string FileName {  get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public List<SkinnedMeshRendererModel> SkinnedMeshRenderers { get; set; }
}
