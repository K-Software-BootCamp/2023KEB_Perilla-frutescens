using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<Vector2Int> mapVec2;
    public List<Vector3> mapVec3 = new();

    private void Start()
    {

    }

    // 2���� ��ǥ�� 3���� ��ǥ�� ����
    public void DimensionTrans(List<Vector2Int> vector2d)
    {
        foreach (Vector2Int v in vector2d)
        {
            mapVec3.Add(new Vector3(v.x, 0, v.y));
        }
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
    }
}
