using System.Collections.Generic;
using UnityEngine;

public class World
{
    private readonly Dictionary<Vector3Int, WorldCell> _worldCells;

    internal World()
    {
        _worldCells = new Dictionary<Vector3Int, WorldCell>();
    }

    public WorldCell GetCell(Vector3Int cellPosition)
    {
        if (_worldCells.ContainsKey(cellPosition))
            return _worldCells[cellPosition];
        var newCell = new WorldCell();
        _worldCells[cellPosition] = newCell;
        return newCell;
    }
}