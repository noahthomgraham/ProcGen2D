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
           WallGen.CreateWalls(floorPositions, tilemap);
        }

        protected HashSet<Vector2Int> RunRandomWalk(Vector2Int position)
        {
            var currentPosition = position;
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            for (int i = 0; i < iterations; i++)
            {
                int random = Random.Range(0, 101);
                var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
                floorPositions.UnionWith(path);
                if (canSpawnPlayer) {
                    float xPos = currentPosition.x;
                    float yPos = currentPosition.y;
                    
                    Instantiate(player, new Vector3(xPos, yPos, 0) , Quaternion.identity);
                    canSpawnPlayer = false;
                }

                if(!canSpawnPlayer && canSpawnEnemy && random > 80)
                {
                    float xPos = currentPosition.x;
                    float yPos = currentPosition.y;

                    Instantiate(Enemy, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    canSpawnEnemy = false;
            }

                if (startRandomlyEachIteration)
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
            return floorPositions;
        }
}
