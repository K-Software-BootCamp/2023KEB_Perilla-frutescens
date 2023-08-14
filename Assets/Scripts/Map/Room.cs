using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoomType
{
    Normal,
    Golden,
    Trap,
    Jump,
    Boss
}

public class Room : MonoBehaviour
{
    public static Room instance;

    public RoomType roomType;

    public List<GameObject> entrances;

    public bool isClearedRoom = false;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    public void ClearRoom(int playerRoomIndex)
    {
        MapGenerator.Instance.OpenDoor(playerRoomIndex);
        ChangeImage();
    }
    public void Open(Transform parent)
    {
        int childCount = parent.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);

            Open(child);

            if (child.CompareTag("Entrance"))
            {
                Destroy(child.gameObject);
            }
        }
    }
    public void ChangeImage()
    {
        // �ڽ� ������Ʈ �� door �±׸� ���� ������Ʈ �˻�
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Door"))
            {
                // Door ��ũ��Ʈ ������Ʈ ��������
                Door doorScript = child.GetComponent<Door>();
                if (doorScript != null)
                {
                    Image doorImage = doorScript.GetComponent<Image>();

                    // ������ door ������Ʈ�� roomType�� ���� ���� ����
                    if (doorScript.connectRoomType == roomType)
                    {
                        doorScript.ChangeDoorColor(Color.green); // ���ϴ� �������� ����
                    }
                }
            }
        }
    }
}
