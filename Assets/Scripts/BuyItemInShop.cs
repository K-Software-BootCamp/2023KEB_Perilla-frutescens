using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItemInShop : MonoBehaviour
{
    public GameObject[] itemPrefabs; // ������ ������ �迭
    public Transform[] spawnPoints; // ��ȯ ���� �迭

    private bool isInsideShop = false;

    private void Start()
    {
        SpawnItems();
    }

    private void Update()
    {
        if (isInsideShop && Input.GetKeyDown(KeyCode.B))
        {
            BuyItem();
        }
    }

    void SpawnItems()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randomItemIndex = Random.Range(0, itemPrefabs.Length); // ���� ������ �ε���
            Vector3 spawnPosition = spawnPoints[i].position; // ��ȯ ������ ��ġ

            GameObject newItem = Instantiate(itemPrefabs[randomItemIndex], spawnPosition, Quaternion.identity); // ������ ������ ��ȯ
            newItem.tag = "Item"; // ������ �±� ���� (�ʿ��� ���)
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            isInsideShop = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            isInsideShop = false;
        }
    }

    private void BuyItem()
    {
        // ������ ���� ������ ���⿡ ����
        Debug.Log("�������� �����߽��ϴ�.");
    }
}
