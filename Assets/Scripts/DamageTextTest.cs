using UnityEngine;
using UnityEngine.EventSystems;

public class DamageTextTest : MonoBehaviour
{
    public int damage = 1111;
    public GameObject hudDamageText;
    public Transform hudPos;

    public void TakenDamage()
    {
        Debug.Log("�������� �޾ҽ��ϴ�.");
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;
        DamageText damageText = hudText.GetComponent<DamageText>();
        if (damageText != null)
        {
            damageText.ShowDamage(damage); // DamageText ��ũ��Ʈ�� ShowDamage ȣ��
        }
    }
}