using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;

    [SerializeField]
    public int walkLength = 10;

    [SerializeField]
    public bool startRandomlyEachIteration = true;

    [SerializeField]
    TileMapVis tilemap;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject Enemy;

    private bool canSpawnEnemy = true;
    private bool canSpawnPlayer = true;


    public void Start()
    {
        RunProceduralGeneration();
    }

    protected void RunProceduralGeneration()
        {
            HashSet<Vector2Int> floorPositions = RunRandomWalk(startPosition);
            tilemap.Clear();
            tilemap.PaintFloorTiles(floorPositions);
           
        }

        protected HashSet<Vector2Int> RunRandomWalk(Vector2Int position)
        {
            var currentPosition = position;
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            for (int i = 0; i < iterations; i++)
            {
                var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
                floorPositions.UnionWith(path);
                if (canSpawnPlayer) {
                    
                }
                if (startRandomlyEachIteration)
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
            return floorPositions;
        }
}
