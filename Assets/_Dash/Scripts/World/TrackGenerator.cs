using UnityEngine;
using System.Collections.Generic;

public class TrackGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject obstaclePrefab;
    public Transform playerTransform;
    public int numberOfTiles = 15;
    public float tileLength = 10f;
    public float spawnZ = 0f;
    public float obstacleFrequency = 0.3f;
    [SerializeField] private float laneWidth = 3f;
    [SerializeField] private float patternChance = 0.2f;

    private const int MinLane = -1;
    private const int MaxLane = 1;

    private List<GameObject> activeTiles = new List<GameObject>();
    private List<GameObject> activeObstacles = new List<GameObject>();

    private int lastObstacleLane = int.MinValue;
    private int sameLaneStreak = 0;
    private const int MaxSameLaneStreak = 2;

    private int lastPatternOpenLane = int.MinValue;
    private int samePatternOpenLaneStreak = 0;
    private const int MaxSamePatternOpenLaneStreak = 2;

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile(i < 5); // Don't spawn obstacles on the first few tiles
        }
    }

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying) return;

        if (playerTransform.position.z - 20 > (spawnZ - numberOfTiles * tileLength))
        {
            SpawnTile(false);
            DeleteTile();
        }
    }

    public void SpawnTile(bool empty)
    {
        GameObject go = Instantiate(tilePrefab, transform.forward * spawnZ, transform.rotation);
        activeTiles.Add(go);

        if (!empty && Random.value < obstacleFrequency)
        {
            SpawnObstacle(spawnZ);
        }

        spawnZ += tileLength;
    }

    private void SpawnObstacle(float zPos)
    {
        if (Random.value < patternChance)
        {
            int openLane = GetPatternOpenLane();
            SpawnTwoLanePattern(openLane, zPos);
        }
        else
        {
            int lane = GetRandomLane();

            if (lane == lastObstacleLane && sameLaneStreak >= MaxSameLaneStreak)
            {
                lane = GetRandomLaneAvoiding(lane);
            }

            RegisterObstacleLane(lane);
            SpawnObstacleInLane(lane, zPos);
        }
    }

    private void SpawnObstacleInLane(int lane, float zPos)
    {
        Vector3 pos = new Vector3(lane * laneWidth, 1f, zPos);
        GameObject obs = Instantiate(obstaclePrefab, pos, Quaternion.identity);
        activeObstacles.Add(obs);
    }

    private void SpawnTwoLanePattern(int openLane, float zPos)
    {
        for (int lane = MinLane; lane <= MaxLane; lane++)
        {
            if (lane != openLane)
            {
                SpawnObstacleInLane(lane, zPos);
            }
        }
    }

    private int GetPatternOpenLane()
    {
        int openLane = GetRandomLane();

        if (openLane == lastPatternOpenLane && samePatternOpenLaneStreak >= MaxSamePatternOpenLaneStreak)
        {
            openLane = GetRandomLaneAvoiding(openLane);
        }

        RegisterPatternOpenLane(openLane);
        return openLane;
    }

    private void RegisterPatternOpenLane(int openLane)
    {
        if (openLane == lastPatternOpenLane)
        {
            samePatternOpenLaneStreak++;
        }
        else
        {
            lastPatternOpenLane = openLane;
            samePatternOpenLaneStreak = 1;
        }
    }

    private int GetRandomLane()
    {
        return Random.Range(MinLane, MaxLane + 1);
    }

    private int GetRandomLaneAvoiding(int laneToAvoid)
    {
        int lane = GetRandomLane();
        while (lane == laneToAvoid)
        {
            lane = GetRandomLane();
        }
        return lane;
    }

    private void RegisterObstacleLane(int lane)
    {
        if (lane == lastObstacleLane)
        {
            sameLaneStreak++;
        }
        else
        {
            lastObstacleLane = lane;
            sameLaneStreak = 1;
        }
    }

    private void DeleteTile()
    {
        if (activeTiles.Count > 0)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }

        // Cleanup obstacles that are behind the player or already destroyed
        while (activeObstacles.Count > 0 && (activeObstacles[0] == null || activeObstacles[0].transform.position.z < playerTransform.position.z - 10f))
        {
            if (activeObstacles[0] != null) Destroy(activeObstacles[0]);
            activeObstacles.RemoveAt(0);
        }
    }
}
