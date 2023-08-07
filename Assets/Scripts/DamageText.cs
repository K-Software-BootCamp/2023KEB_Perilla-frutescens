using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    private TextMeshPro text; // TextMeshProUGUI ������Ʈ ���
    private Color alpha;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
    }

    private void Start()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        Invoke("DestroyObject", destroyTime);
    }

    private void Update()
    {
        // �ؽ�Ʈ�� ���� �̵�
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        // �ؽ�Ʈ ���İ� ����
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    public void ShowDamage(int damage)
    {
        text.text = damage.ToString(); // ���� ������ ���� �ؽ�Ʈ�� ����
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}