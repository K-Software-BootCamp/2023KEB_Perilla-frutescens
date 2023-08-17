using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItemInShop : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform[] spawnPoints;
    public int[] itemPrices;

    public int playerCoins = 500; // �ʱ� �÷��̾� ����

    private int[] shopPrices; // �� ������ �ش��ϴ� ������ ���� �迭

    private void Start()
    {
        shopPrices = new int[spawnPoints.Length]; // shopPrices �迭 �ʱ�ȭ
        SpawnItems();
    }

    private void SpawnItems()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randomItemIndex = Random.Range(0, itemPrefabs.Length);
            GameObject newItem = Instantiate(itemPrefabs[randomItemIndex], spawnPoints[i].position + (Vector3.up * 1), Quaternion.identity);
            newItem.transform.SetParent(spawnPoints[i]); // �������� spawnPoints[i]�� ������ ����

            // shopPrices �迭�� �ش� ������ ���� ����
            shopPrices[i] = itemPrices[randomItemIndex];
        }

        Debug.Log("shopPrices:" + shopPrices.Length);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Shop") && Input.GetKeyDown(KeyCode.B)) // ���� ĭ�� �浹���� ���
        {
            int shopIndex = -1; // ���� ĭ �ε����� ������ ���� �ʱ�ȭ

            // ���� ĭ�� Transform�� ��ġ�ϴ� �ε��� ã��
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i] == collision.transform)
                {
                    shopIndex = i; // ���� ĭ �ε��� ����
                    break; // ��ġ�ϴ� �ε����� ã���� �ݺ��� ����
                }
            }

            if (shopIndex != -1)
            {
                BuyItem(shopIndex);
            }
        }
    }

    private void BuyItem(int shopIndex)
    {
        if (shopIndex >= 0 && shopIndex < spawnPoints.Length)
        {
            int itemPrice = shopPrices[shopIndex];

            if (playerCoins >= itemPrice)
            {
                if (spawnPoints[shopIndex].childCount > 1)
                {
                    Transform secondItemTransform = spawnPoints[shopIndex].GetChild(1); // �� ��° ������ ��������
                    string itemName = secondItemTransform.name;

                    // ������ ���� ����
                    Debug.Log("�������� �����߽��ϴ�: " + itemName);
                    playerCoins -= itemPrice; // ���� ����

                    // ������ �ڽ��� �ı�
                    Transform shopTransform = spawnPoints[shopIndex];
                    if (shopTransform.childCount > 0)
                    {
                        Destroy(shopTransform.GetChild(0).gameObject);
                    }
                }               
            }
            else
            {
                Debug.Log("������ �����մϴ�.");
            }
        }
    }
}