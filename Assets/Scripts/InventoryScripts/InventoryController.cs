using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel; // �κ��丮 �г��� ������ ����
    private bool isInventoryOpen = false; // �κ��丮 ���� ���¸� ����

    private void Start()
    {
        inventoryPanel.SetActive(false); // ���� ���� �� �κ��丮 �г��� ��Ȱ��ȭ
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // I Ű�� ������ ��
        {
            isInventoryOpen = !isInventoryOpen; // �κ��丮 ���� ���¸� ����

            if (isInventoryOpen)
            {
                OpenInventory(); // �κ��丮 ����
            }
            else
            {
                CloseInventory(); // �κ��丮 �ݱ�
            }
        }
    }

    private void OpenInventory()
    {
        inventoryPanel.SetActive(true); // �κ��丮 �г��� Ȱ��ȭ�Ͽ� ���� ���·� ����
    }

    private void CloseInventory()
    {
        inventoryPanel.SetActive(false); // �κ��丮 �г��� ��Ȱ��ȭ�Ͽ� ���� ���·� ����
    }
}
