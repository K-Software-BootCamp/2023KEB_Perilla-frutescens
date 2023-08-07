using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;

    public List<Vector2Int> mapVec2;
    public List<Vector3> mapVec3 = new();
    public List<int>  EpicRooms = new(); // ���� ��� Ʈ�� ���� ������ ���� ���ȹ� �迭 ����
    private int epicSize = 0;
    private int mapSize = 0;
    public GameObject EntryRoom;
    public GameObject[] NormalRooms;
    public GameObject[] BossRooms;
    public GameObject[] JumpRooms;
    public GameObject[] GoldRooms;
    public GameObject[] TrapRooms;
    public GameObject Room;
    public List<GameObject> Rooms;

    private void Awake()
    {
        Instance = this;
    }
    // 2���� ��ǥ�� 3���� ��ǥ�� ����
    public void DimensionTrans(List<Vector2Int> vector2d)
    {
        foreach (Vector2Int v in vector2d)
        {
            mapVec3.Add(new Vector3(v.x, 0, v.y));
        }
        mapSize = mapVec3.Count;
    }
    // ���� ������Ʈ�� Ȱ��ȭ �Ǿ������� MapVector2�� OnMapAdded�� ����
    public void OnEnable()
    {
        MapVector2.OnMapAdded += HandleMapAdded;
    }
    // ���� ��ǥ�� 3������ǥ�� ���� �� �� ����
    private void HandleMapAdded(List<Vector2Int> vector)
    {
        DimensionTrans(vector);
        Rooms.Clear();
        EpicRooms.Clear();
        RoomGenerator();
    }

    private void RoomGenerator()
    {
        DungeonReset();
        Rooms.Add(Instantiate(EntryRoom, mapVec3[0], Quaternion.identity));
        mapVec3.Remove(mapVec3[0]);
        EpicRoomCreate();
        NormalRoomCreate();
        NavMeshBake(Rooms);
    }
    private void EpicRoomCreate()
    {
        //���ȸ���Ʈ 0: ������ 1: Ȳ�ݹ� 2: ������ ������ ������������ �����ϰ� ���ϱ�
        for (var i = 0; i < EpicRooms.Count; i++)
        {
            EpicRooms[i] = Random.Range(0, MapVector2.instance.Stage + 1);
            epicSize += EpicRooms[i];
        }

        for(var i = 0; i < EpicRooms[0]; i++)
        {
            var rand = Random.Range(0, 4); //������ ���� ����
            var randMap = Random.Range(0, mapVec3.Count);
            Rooms.Add(Instantiate(JumpRooms[rand], mapVec3[randMap], Quaternion.identity));
            mapVec3.Remove(mapVec3[randMap]);
        }
        for(var i = 0; i < EpicRooms[1]; i++)
        {
            var rand = Random.Range(0, 4); //Ȳ�ݹ� ���� ����
            var randMap = Random.Range(0, mapVec3.Count);
            Rooms.Add(Instantiate(GoldRooms[rand], mapVec3[randMap], Quaternion.identity));
            mapVec3.Remove(mapVec3[randMap]);
        }
        for (var i = 0; i < EpicRooms[2]; i++)
        {
            var rand = Random.Range(0, 4); //Ʈ���� ���� ����
            var randMap = Random.Range(0, mapVec3.Count);
            Rooms.Add(Instantiate(TrapRooms[rand], mapVec3[randMap], Quaternion.identity));
            mapVec3.Remove(mapVec3[randMap]);
        }
    }
    private void NormalRoomCreate()
    {
        for(var i = 0; i < mapVec3.Count ; i++)
        {
            var randNor = Random.Range(0, NormalRooms.Length);
            var randMap = Random.Range(0, mapVec3.Count);
            Rooms.Add(Instantiate(NormalRooms[randNor], mapVec3[randMap], Quaternion.identity));
            mapVec3.Remove(mapVec3[randMap]);
        }
    }
    private void NavMeshBake(List<GameObject> bakeroom)
    {
        foreach (var room in bakeroom)
        {
            Transform[] children = room.GetComponentsInChildren<Transform>();
            
            foreach (var child in children)
            {
                MeshCollider meshCollider = child.GetComponent<MeshCollider>();
                if (meshCollider != null)
                {
                    NavMeshSurface navMeshSurface = child.GetComponent<NavMeshSurface>();
                    if (navMeshSurface != null)
                    {
                        NavMeshData newNavMeshData = new NavMeshData();
                        navMeshSurface.navMeshData = newNavMeshData;
                        navMeshSurface.BuildNavMesh();
                    }
                }
            }
        }       
    }
    public void DungeonReset()
    {
        NavMesh.RemoveAllNavMeshData();

        GameObject[] roomTag = GameObject.FindGameObjectsWithTag("Rooms");

        foreach(var room in roomTag)
        {
            Destroy(room);
        }
    }
}
