using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public List<Vector2Int> mapVec2;
    public List<Vector3> mapVec3 = new();
    public int[] EpicRooms = new int[3]; // ���� ��� Ʈ�� ���� ������ ���� ���ȹ� �迭 ����
    private int epicSize = 0;
    private int roomSize = 0;
    public GameObject EntryRoom;
    public GameObject[] NormalRooms;
    public GameObject[] BossRooms;
    public GameObject[] JumpRooms;
    public GameObject[] GoldRooms;
    public GameObject[] TrapRooms;
    public GameObject Room;
  
    // 2���� ��ǥ�� 3���� ��ǥ�� ����
    public void DimensionTrans(List<Vector2Int> vector2d)
    {
        foreach (Vector2Int v in vector2d)
        {
            mapVec3.Add(new Vector3(v.x, 0, v.y));
        }
        roomSize = mapVec3.Count;
    }
    // ���� ������Ʈ�� Ȱ��ȭ �Ǿ������� MapVector2�� OnMapAdded�� ����
    private void OnEnable()
    {
        MapVector2.OnMapAdded += HandleMapAdded;
    }
    // ���� ��ǥ�� 3������ǥ�� ������ �޼��� ����
    private void HandleMapAdded(List<Vector2Int> vector)
    {
        DimensionTrans(vector);
        MapVector2.OnMapAdded -= HandleMapAdded;
        RoomGenerator();
    }

    private void RoomGenerator()
    {
        Instantiate(EntryRoom, mapVec3[0], Quaternion.identity);
        EpicRoomCreate();
        NormalRoomCreate();
        NavMeshBake(Room);
    }
    private void EpicRoomCreate()
    {
        //���ȸ���Ʈ 0: ������ 1: Ȳ�ݹ� 2: ������ ������ ������������ �����ϰ� ���ϱ�
        for (var i = 0; i < EpicRooms.Length; i++)
        {
            EpicRooms[i] = Random.Range(0, MapVector2.instance.Stage + 1);
            epicSize += EpicRooms[i];
        }
        // ������ ������ŭ �� ���� -> mapVec3[0] �� Entry���̹Ƿ� �� �ڷ� ����
        for(var i = 0; i < EpicRooms[0]; i++)
        {
            var rand = Random.Range(0, 4); //���ȹ� ���� ����
            Room = Instantiate(JumpRooms[rand], mapVec3[i+1], Quaternion.identity);
            NavMeshBake(Room);
        }
        // Ȳ�ݹ� ������ŭ �� ���� -> mapVec3[1 + ������ ��] �� �ڷ� ����
        for(var i = 0; i < EpicRooms[1]; i++)
        {
            var rand = Random.Range(0, 4); //���ȹ� ���� ����
            Room = Instantiate(GoldRooms[rand], mapVec3[i + 1 + EpicRooms[0]], Quaternion.identity);
            NavMeshBake(Room);
        }
        // ������ ������ŭ �� ���� -> mapVec3[1 + ������ + Ȳ�ݹ� ��] �� �ڷ� ����
        for (var i = 0; i < EpicRooms[2]; i++)
        {
            var rand = Random.Range(0, 4); //���ȹ� ���� ����
            Room = Instantiate(TrapRooms[rand], mapVec3[i + 1 + EpicRooms[0] + EpicRooms[1]], Quaternion.identity);
            NavMeshBake(Room);
        }
    }
    private void NormalRoomCreate()
    {
        for(var i = epicSize + 1 ; i < roomSize; i++)
        {
            var randNor = Random.Range(0, NormalRooms.Length);
            Room = Instantiate(NormalRooms[randNor], mapVec3[i], Quaternion.identity);
            NavMeshBake(Room);
        }
    }
    private void NavMeshBake(GameObject room)
    {
        
        if(room.TryGetComponent<MeshCollider>(out var meshCollider))
        {
            
            if(!room.TryGetComponent<NavMeshSurface>(out var navMeshSurface))
                navMeshSurface = room.AddComponent<NavMeshSurface>();

            navMeshSurface.collectObjects = CollectObjects.Children;
            navMeshSurface.BuildNavMesh();
        }
        
    }
}
