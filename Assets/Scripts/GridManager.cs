using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridWidth = 32;
    [SerializeField] private int gridHeight = 32;

    [SerializeField] private int numNodes = 9;

    [SerializeField] private Tile resourceTile;

    [SerializeField] private Transform Camera;
    private Vector2Int Spawns;
   

    private Dictionary<Vector2Int, Tile> _tiles;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2Int, Tile>();
        
        for (int row = 0; row < gridWidth; row++)
        {
            for (int col = 0; col < gridHeight; col++)
            {
                var spawnTile = Instantiate(resourceTile, new Vector3(row, col), Quaternion.identity);
                spawnTile.tRow = row;
                spawnTile.tCol = col;
                spawnTile.name = $"Tile {row} {col}";

                //var isOffset = (row % 2 == 0 && col % 2 != 0) || (row % 2 != 0 && col % 2 == 0);

                _tiles[new Vector2Int(row, col)] = spawnTile;
            }
        }

        Camera.transform.position = new Vector3((float)gridWidth / 2 - 0.5f, (float)gridHeight / 2 - 0.5f, -10);


        for (int i = 0; i < numNodes; i++)
        {
            int nbRow, nbCol;
            Spawns = new Vector2Int(Random.Range(0,gridWidth),Random.Range(0,gridHeight));
            var tile = GetTileAtPosition(Spawns);
            tile.resNum += 100; 
            for (int r = -2; r < 3; r++)
            {
                nbRow = Spawns.x + r;
                if (nbRow >= 0 && nbRow < 32)
                {
                    for (int c = -2; c < 3; c++)
                    {
                        nbCol = Spawns.y + c;
                        if (nbCol >= 0 && nbCol < 32)
                        {
                            if (Mathf.Abs(r) == 2 || Mathf.Abs(c) == 2)
                            {
                                var midTile = GetTileAtPosition(new Vector2Int(nbRow, nbCol));
                                midTile.resNum += 25;
                            }
                            else if (Mathf.Abs(r) == 1 || Mathf.Abs(c) == 1)
                            {
                                var midTile = GetTileAtPosition(new Vector2Int(nbRow, nbCol));
                                midTile.resNum += 50;
                            }
                        }
                    }
                }

            }

            
        }
    }

    public Tile GetTileAtPosition(Vector2Int pos)
    {
        if (_tiles.TryGetValue(pos,out var tile))
        {
            return tile;
        }
        return null;
    }
}
