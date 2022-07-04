using UnityEditor;
using UnityEngine;


public class TileMapGeneratorEditor : EditorWindow
{
    //Settings
    private int _maxRows = 5;
    private int _maxCols = 5;

    private float _offsetX = 1f;
    private float _offsetY = 1f;

    private float _spawnRate = 1f;
    private float _tileSize = 1f;
    
    //Reference
    private static TileMapGenerator _tileMapGenerator;

    
    private Vector2 _scrollPos;

    
    [MenuItem("Window/Custom/TileMapGeneratorEditor")]
    public static void ShowWindow()
    {
        GetWindow<TileMapGeneratorEditor>();
    }
    
    private void OnGUI()
    {
        if (_tileMapGenerator == null)
        {
            _tileMapGenerator = FindObjectOfType<TileMapGenerator>();
        }
        
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        
        Settings();
        GeneralUtility();
        
        EditorGUILayout.EndScrollView();
    }

    private void Settings()
    {
        //Header
        GUILayout.Space(10f);
        GUILayout.Label("Settings:", EditorStyles.boldLabel);
        
        //Map size
        GUILayout.Space(8f);
        GUILayout.Label("Map Size", EditorStyles.boldLabel);
        _maxRows = EditorGUILayout.IntField("Max Rows: ", _maxRows);
        _maxCols = EditorGUILayout.IntField("Max Cols: ", _maxCols);
        
        if (_maxRows <= 0 || _maxCols <= 0)
        {
            EditorGUILayout.HelpBox("Must at least have 1 row and 1 column.", MessageType.Error);
        }
        
        //Offsets
        GUILayout.Space(8f);
        GUILayout.Label("Offsets", EditorStyles.boldLabel);
        _offsetX = EditorGUILayout.FloatField("Offset X: ", _offsetX);
        _offsetY = EditorGUILayout.FloatField("Offset Y: ", _offsetY);
        
        if (_offsetX < 1f || _offsetY < 1f)
        {
            EditorGUILayout.HelpBox("Offsets are recommended to be at least 1.", MessageType.Warning);
        }
        
        //Spawnrate
        GUILayout.Space(8f);
        GUILayout.Label("Spawn Rate", EditorStyles.boldLabel);
        _spawnRate = EditorGUILayout.FloatField("Spawn Rate: ", _spawnRate);
        
        //Change tile size
        GUILayout.Space(8f);
        GUILayout.Label("Tile Size", EditorStyles.boldLabel);
        _tileSize = EditorGUILayout.FloatField("Tile Size: ", _tileSize);
        
        if (GUILayout.Button("Confirm Tile Size (updates Tile Prefab)"))
        {
            _tileMapGenerator.ChangeTileSize(_tileSize);
        }
    }

    private void GeneralUtility()
    {
        GUILayout.Space(10f);
        GUILayout.Label("General Utility:", EditorStyles.boldLabel);
        
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Map"))
        {
            _tileMapGenerator.GenerateTiles(_maxRows, _maxCols, _offsetX, _offsetY, _spawnRate);
            SpawnTileMap();
        }
        
        if (GUILayout.Button("Clear Map"))
        {
            _tileMapGenerator.ClearMap();
        }
        GUILayout.EndHorizontal();
        
        
        //Tile Generation
        GUILayout.BeginHorizontal();
        if (!_tileMapGenerator.IsListEmpty)
        {
            if (GUILayout.Button("Save Map (Create Prefab)"))
            {
                _tileMapGenerator.SaveTiles(_maxRows, _maxCols, _spawnRate);
            }
        }
        GUILayout.EndHorizontal();
    }
    
    private void SpawnTileMap()
    {
        _tileMapGenerator.SpawnTiles();
    }
}
