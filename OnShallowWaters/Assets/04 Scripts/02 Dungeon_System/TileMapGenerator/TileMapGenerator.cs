using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


public class TileMapGenerator : MonoBehaviour
{
    [Header("Ref:")]
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform mapOrigin;
    
    private List<TileData> _tileDataList = new List<TileData>();
    private readonly List<Tile> _prevGeneratedTilesList = new List<Tile>();

    public bool IsListEmpty => _tileDataList.Count == 0;
    
    
    public void GenerateTiles(int maxRows, int maxCols, float offsetX, float offsetY, float spawnRate)
    {
        _tileDataList.Clear();

        int id = 0;
        for (int i = 0; i < maxRows; i++)
        {
            for (int j = 0; j < maxCols; j++)
            {
                float rate = Random.Range(0f, 1f);
                if (rate > spawnRate) continue;
                
                
                TileData tileData;
  
                tileData = new TileData(id, i, j, offsetX, offsetY);
                
                _tileDataList.Add(tileData);
   
                id++;
            }
        }
    }
    
    public void SpawnTiles()
    {
        ClearPreviousTiles();

        Vector3 spawnPos = mapOrigin.position;
        
        for (int i = 0; i < _tileDataList.Count; i++)
        {
            TileData tileData = _tileDataList[i];
            Tile tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
            tile.Id = i;

            Transform tileTransform = tile.transform;
            tileTransform.parent = mapOrigin.transform;
            
            
            Vector3 tileWidth = tile.MeshRenderer.bounds.size;
            Vector3 coords = new Vector3(tileData.col, 0f, tileData.row);
            Vector3 offsets = new Vector3(tileData.offsetX, 0f, tileData.offsetZ);
            
            Vector3 temp = Vector3.Scale(coords, offsets);
            Vector3 pos = Vector3.Scale(temp, tileWidth);
                         
            tileTransform.position = pos + spawnPos;
            SetTileInfo(tile, pos + spawnPos);

            _prevGeneratedTilesList.Add(tile);
        }
    }
    
    
    public void SaveTiles(int maxRow, int maxCols, float spawnRate)
    {
        print("Saving...");

        string name = $"size_({maxRow}, {maxCols}), rate_{spawnRate}";
        string _generatedRoomPath = "Assets/03 Prefab/TileGeneration/Rooms/" + name + ".prefab";
        
        //create prefab
        //name prefab with size and column
        _generatedRoomPath = AssetDatabase.GenerateUniqueAssetPath(_generatedRoomPath);
        PrefabUtility.SaveAsPrefabAsset(mapOrigin.gameObject, _generatedRoomPath);
    }

    
    public void ClearMap()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        
        foreach (Tile t in tiles)
        {
            DestroyImmediate(t.gameObject);
        }
        _prevGeneratedTilesList.Clear();
        _tileDataList.Clear();
    }

    public void ChangeTileSize(float size)
    {
        Vector3 newSize = new Vector3(size, size, size);
        tilePrefab.transform.localScale = newSize;
    }
    
    private void ClearPreviousTiles()
    {
        if (_prevGeneratedTilesList.Count > 0)
        {
            foreach (Tile tile in _prevGeneratedTilesList)
            {
                DestroyImmediate(tile.gameObject);
            }
            _prevGeneratedTilesList.Clear();
        }
    }

    private void SetTileInfo(Tile tile, Vector2 pos)
    {
        tile.name = $"({pos.x}, {pos.y})";
    }
}
