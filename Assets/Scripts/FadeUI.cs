using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���ӸŴ��� ��ũ��Ʈ���� public FadeUI fadeUI�� �̿��ؼ� �ҷ����� ȿ���� �� ������
//fadeUI.gameObject.SetActive(true) �Ŀ� fadeUI.StartFadeIn/Out�� ����
//�ڷ�ƾ �ȿ� �ִ� SetActive(false)/WaitForSeconds(5)�� test�� �̹Ƿ� ����� ��
public class FadeUI : MonoBehaviour
{
    public Image fadeUI;

    public void StartFadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float fadeAlpha = 0;
        while (fadeAlpha < 1.0f)
        {
            fadeAlpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            fadeUI.color = new Color(0, 0, 0, fadeAlpha);
        }
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }

    IEnumerator FadeInCoroutine()
    {
        float fadeAlpha = 1;
        while (fadeAlpha > 0f)
        {
            fadeAlpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            fadeUI.color = new Color(0, 0, 0, fadeAlpha);
        }
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
