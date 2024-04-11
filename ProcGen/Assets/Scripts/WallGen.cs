using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGen
{
   public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVis tileMapVisualzer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        foreach (var wall in basicWallPositions) 
        {
            tileMapVisualzer.PaintSingleBasicWall(wall);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in  floorPositions)
        {
            foreach(var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition) == false)
                    wallPositions.Add(neighbourPosition);
            }
        }
        return wallPositions;
    }
}
