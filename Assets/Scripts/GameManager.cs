using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public World World { get; private set; }

    private void Start()
    {
        World = new World();
    }
}