using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bricks : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap tilemap2;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap2 = GetComponent<Tilemap>();
    }
    
    public void MakeDot(Vector3 Pos)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos);
        Vector3Int cellPosition2 = tilemap2.WorldToCell(Pos);
        tilemap.SetTile(cellPosition, null);
        tilemap2.SetTile(cellPosition2, null);
    }

    void Update()
    {
        
    }
}
