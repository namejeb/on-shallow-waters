using UnityEngine;

public class TileData
{
    public int id;
    public int row;
    public int col;

    public float offsetX;
    public float offsetZ;
    

    public TileData(int id, int row, int col, float offsetX, float offsetZ)
    {
        this.id = id;
        this.row = row;
        this.col = col;
        
        this.offsetX = offsetX;
        this.offsetZ = offsetZ;
    }
}
public class Tile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private int id;
    
    public int Id
    {
        get => id;
        set => id = value;
    }
    
    public MeshRenderer MeshRenderer
    {
        get => meshRenderer;
    }
}