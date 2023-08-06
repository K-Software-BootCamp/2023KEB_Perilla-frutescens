using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapVector2 : MonoBehaviour
{
    public List<Vector2Int> mapVector = new();
    public List<Vector2Int> candidateVector = new();
    public List<Vector2Int> temp = new();

    public Vector2Int startPoint = new(0, 0);
    public int numOfRooms = 5;
    public int Stage = 1;

    void Start()
    {
        mapVector.Clear();
        candidateVector.Clear();

        if(Stage == 1)
            numOfRooms = 7;
        else if (Stage == 2)
            numOfRooms = 10;
        else if (Stage == 3)
            numOfRooms = 15;
        
        mapVector.Add(startPoint);

        StartFindingVector(mapVector[mapVector.Count-1]);
    }

    public void StartFindingVector(Vector2Int startVector)
    {
        if(mapVector.Count < numOfRooms) 
        {
            Temping(temp, startVector, candidateVector);
            Selecting(mapVector, candidateVector);
            StartFindingVector(mapVector[mapVector.Count - 1]);
        }
    }

    //�����¿� ��ǥ�� ���� �ߺ����� �ʴ� ���� �ĺ��ں��Ϳ� ����
    public void Temping(List<Vector2Int> listVector, Vector2Int vecTemp, List<Vector2Int> candiVector)
    {
        listVector.Clear();
        listVector.Add(new Vector2Int(vecTemp.x+5, vecTemp.y));
        listVector.Add(new Vector2Int(vecTemp.x, vecTemp.y+5));
        listVector.Add(new Vector2Int(vecTemp.x-5, vecTemp.y));
        listVector.Add(new Vector2Int(vecTemp.x, vecTemp.y-5));

        List<Vector2Int> templist = new(candiVector);

        foreach (Vector2Int vec2 in listVector.Except(templist))
        {
            if (vec2.Equals(new Vector2Int(0, 0)))
                listVector.Remove(vec2);
            else
                candiVector.Add(vec2);
        }
    }

    // �ĺ��� �������� �ϳ��� ��� �̰� �� ���Ϳ� ���ִ��� ���� �߰��ϰų� �����ϰ� �ٽû���
    public void Selecting(List<Vector2Int> mapVector, List<Vector2Int> candiVector)
    {
        var rand = Random.Range(0, candiVector.Count-1);
        var randVector = candiVector[rand];

        bool check = candiVector.Any(randVector => mapVector.Contains(randVector));

        //�ߺ����� ������ mapVector�� �߰��� �ĺ��ڿ��� ������
        if (!check)
        {
            mapVector.Add(randVector);
            candiVector.Remove(randVector);
        }
        //�ߺ����� �̾��� ��� �ĺ��ڿ��� ������ �ٽ� ����
        else
        {
            candiVector.Remove(randVector);
            Selecting(mapVector, candiVector);
        }

    }
}
