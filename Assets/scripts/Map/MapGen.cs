
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapGen : MonoBehaviour
{
    public Transform weatherSpawnPoint;
    public NavMeshSurface surface;

    public LevelConstructor[] levelToBuild;
    public Vector2 mapSize;
    [Range(0, 100)]
    public float obstaclePercent;
    public int grassAndFlowerDensity;

    List<Coord> allTileCoords;
    Queue<Coord> shuffledCoords;
    Queue<Coord> shuffledOpenCoords;
    List<Vector3> openPositions;

    Coord mapCentre;
    Transform[,] tileMap;
    bool[,] obstacleMap;
    int levelTypeIndex;
    Vector3 zero = new Vector3(0, 0, 0);

    string holderName = "Generated Map";
    Transform mapholder;


    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        DestroyOldMap();
        CreateCoordList();
        CreateGroundAndWater();
        CreateObstacles();
        surface.BuildNavMesh();
        SpawnFlowers();
        SpawnWeather();
    }

    private void SpawnFlowers()
    {
        for (int i = 0; i < grassAndFlowerDensity; i++)
        {
            int obstacleIndex =  UnityEngine.Random.Range(0, levelToBuild[levelTypeIndex].natureObjects.Length);

            Vector3 position = GetRandomOpenTile().position;
            Vector3 randomisePosition = new Vector3(position.x +  UnityEngine.Random.Range(-1f, 1f), position.y, position.z +  UnityEngine.Random.Range(-1f, 1f));

            Instantiate(levelToBuild[levelTypeIndex].natureObjects[obstacleIndex], randomisePosition, Quaternion.Euler(new Vector3(0,  UnityEngine.Random.Range(0, 360), 0)),mapholder);
        }
    }

    public void SpawnWeather()
    {
        RenderSettings.skybox = levelToBuild[levelTypeIndex].skyboxType;

        foreach (Transform child in weatherSpawnPoint.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        if (levelToBuild[levelTypeIndex].weatherType.Length > 0)
        {

            Instantiate(levelToBuild[levelTypeIndex].weatherType[0], weatherSpawnPoint.position , Quaternion.identity, weatherSpawnPoint);
        }
    }

    private void DestroyOldMap()
    {
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        mapholder = new GameObject(holderName).transform;
        mapholder.parent = transform;
    }

    private void CreateCoordList()
    {
        allTileCoords = new List<Coord>();
        openPositions = new List<Vector3>();

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }

        BoxCollider box = GetComponent<BoxCollider>();
        box.size = new Vector3(mapSize.x * 2, 0.5f, mapSize.y * 2)*2;
        box.center = new Vector3(0, -0.5f, 0);
        shuffledCoords = new Queue<Coord>(ShuffleArray.ShuffleThisArray(allTileCoords.ToArray()));
        mapCentre = new Coord((int)mapSize.x / 2, (int)mapSize.y / 2);
        levelTypeIndex =  UnityEngine.Random.Range(0, levelToBuild.Length);
        tileMap = new Transform[(int)mapSize.x, (int)mapSize.y];
    }

    private void CreateGroundAndWater()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y)*2;

                GameObject newTile = Instantiate(
                    levelToBuild[levelTypeIndex].groundTypes[GetFloorType(x,y)],
                    tilePosition,
                    Quaternion.Euler(0, GetRotation(x, y), 0));

                newTile.transform.parent = mapholder;
                tileMap[x, y] = newTile.transform;
                openPositions.Add(tilePosition);
            }
        }
       
        Instantiate(levelToBuild[levelTypeIndex].waterType, new Vector3(0, -3.5f, 0), Quaternion.identity,mapholder);
    }
    private int GetRotation(int x, int y)
    {
        int[] rotationArray = { 0, 90, 180, 270 };
        int rotationRandom = rotationArray[ UnityEngine.Random.Range(0, rotationArray.Length)];

        int rotation;
        if (y == 0)
        {
            rotation = x == mapSize.x - 1 ? 270 : 0;
            return rotation;
        }

        if (x == 0)
        {
            rotation = y == mapSize.y - 1 ? 90 : 90;
            return rotation;
        }

        if (y == mapSize.y - 1)
        {
            return 180;
        }

        rotation = x == mapSize.x - 1 ? 270 : rotationRandom;

        return rotation;
    }
    private int GetFloorType(int x, int y)
    {
        int floorType;

        if (y == 0)
        {
            floorType = x == 0 ? 2 : 1;

            if (floorType == 1 && x == mapSize.x - 1)
            {
                return 2;
            }


            if (levelToBuild[levelTypeIndex].groundTypes.Length > 2 && floorType == 1)
            {
                return UnityEngine.Random.value < 0.5f ? 1 : RandomNewFloorPiece;
            }


            return floorType;
        }

        if (x == 0)
        {
            floorType = y == mapSize.y -1 ? 2 : 1;


            if (levelToBuild[levelTypeIndex].groundTypes.Length > 2 && floorType == 1)
            {
                return UnityEngine.Random.value < 0.5f ? 1 : RandomNewFloorPiece;
            }


            return floorType;
        }

        if(y == mapSize.y - 1)
        {
            floorType = x == mapSize.x - 1 ? 2 : 1;

            if (levelToBuild[levelTypeIndex].groundTypes.Length > 2 && floorType == 1)
            {
                return UnityEngine.Random.value < 0.5f ? 1 : RandomNewFloorPiece;
            }

            return floorType;
        }

        if (x == mapSize.x - 1)
        {
            if (levelToBuild[levelTypeIndex].groundTypes.Length > 2)
            {
                return UnityEngine.Random.value < 0.5f ? 1 : RandomNewFloorPiece;
            }

            return 1;
        }


        if (levelToBuild[levelTypeIndex].groundTypes.Length > 2)
        {
            return UnityEngine.Random.value < 0.5f ? 0 : 3;
        }

        return 0;
    }

    int RandomNewFloorPiece => UnityEngine.Random.value < 0.5f ? 5 : 4;

    private void CreateObstacles()
    {
        List<Coord> allOpenCoords = new List<Coord>(allTileCoords);
        obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y];
        int count = (int)(mapSize.x * mapSize.y * obstaclePercent) / 100;
        int currentObstacleCount = 0;

        for (int i = 0; i < count; i++)
        {
            Coord randomCoord = GetRandomCoord();
            
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;
            if (randomCoord != mapCentre && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
            {

                Vector3 newPosition = CoordToPosition(randomCoord.x, randomCoord.y)*2;
                openPositions.Remove(newPosition);

                bool spawnObstacleType = UnityEngine.Random.value > 0.5f;

                if (spawnObstacleType)
                {
                    CreateGroundLevelObstacle(newPosition);
                }
                else
                {
                    CreateHighLevelObstacle(newPosition);
                }
               
                allOpenCoords.Remove(randomCoord);

            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }
        shuffledOpenCoords = new Queue<Coord>(ShuffleArray.ShuffleThisArray(allOpenCoords.ToArray()));
    }

    void CreateHighLevelObstacle(Vector3 newPosition)
    {
        int obstacleIndex =  UnityEngine.Random.Range(0, levelToBuild[levelTypeIndex].obsticlesWithLayers.Length);
        float[] direction = { 0, 90, 180, 270 };

        GameObject newObstacle = Instantiate(levelToBuild[levelTypeIndex].obsticlesWithLayers[obstacleIndex], newPosition,
          Quaternion.Euler(new Vector3(0, direction[ UnityEngine.Random.Range(0, direction.Length)], 0)));

        newObstacle.layer = 9;
        newObstacle.transform.parent = mapholder;
    }

    void CreateGroundLevelObstacle(Vector3 newPosition)
    {
        Vector3 randomMovePosition = new Vector3(newPosition.x +  UnityEngine.Random.Range(-1f, 1f), newPosition.y, newPosition.z +  UnityEngine.Random.Range(-1f, 1f));

        int obstacleIndex =  UnityEngine.Random.Range(0, levelToBuild[levelTypeIndex].obsticles.Length);

        GameObject newObstacle = Instantiate(levelToBuild[levelTypeIndex].obsticles[obstacleIndex], randomMovePosition,
            Quaternion.Euler(new Vector3(0,  UnityEngine.Random.Range(0, 360), 0)));

        newObstacle.layer = 9;
        newObstacle.transform.parent = mapholder;
    }

    bool MapIsFullyAccessible(bool[,] _obstacleMap, int _currentObstacleCount)
    {
        bool[,] mapFlags = new bool[_obstacleMap.GetLength(0), _obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(mapCentre);
        mapFlags[mapCentre.x, mapCentre.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;

                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0)
                            && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY]
                                && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }

                }
            }

        }
        int targetAccessibleTileCount = (int)mapSize.x * (int)mapSize.y - _currentObstacleCount;
        return targetAccessibleTileCount == accessibleTileCount;
    }
    Vector3 CoordToPosition(int x, int y)
    {
        float tilePieceSize = 0.5f;
        return new Vector3((-mapSize.x / 2 + tilePieceSize + x) * 2, 0, (-mapSize.y / 2 + tilePieceSize + y) * 2);
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledCoords.Dequeue();
        shuffledCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public Vector3 GetClosestTile(Vector3 playerPosition)
    {
        Vector3 bestTarget = zero;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (Vector3 potentialTarget in openPositions)
        {
            Vector3 directionToTarget = potentialTarget - playerPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
    public Transform GetRandomOpenTile()
    {
        Coord randomCoord = shuffledOpenCoords.Dequeue();
        shuffledOpenCoords.Enqueue(randomCoord);

        return tileMap[randomCoord.x, randomCoord.y];
    }
}

[System.Serializable]
public struct Coord
{
    public int x;
    public int y;

    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static bool operator ==(Coord c1, Coord c2)
    {
        return c1.x == c2.x && c1.y == c2.y;
    }

    public static bool operator !=(Coord c1, Coord c2)
    {
        return !(c1 == c2);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}