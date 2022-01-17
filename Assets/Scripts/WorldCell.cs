using UnityEngine;

public class WorldCell
{
    public bool IsEmpty { get; set; }
    private GameObject _instance;
    
    public WorldCell()
    {
        IsEmpty = true;
    }

    public void Create(GameObject prefab, Vector3 worldPos)
    {
        _instance = Object.Instantiate(prefab, worldPos, Quaternion.identity);
        IsEmpty = false;
    }

    public void Delete()
    {
        Object.Destroy(_instance);
        IsEmpty = true;
    }
}
