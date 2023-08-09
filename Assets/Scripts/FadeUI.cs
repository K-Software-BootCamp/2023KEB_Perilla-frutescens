using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���ӸŴ��� ��ũ��Ʈ���� public FadeUI fadeUI�� �̿��ؼ� �ҷ����� ȿ���� �� ������
//fadeUI.gameObject.SetActive(true) �Ŀ� fadeUI.StartFadeIn/Out�� ����
//�ڷ�ƾ �ȿ� �ִ� SetActive(false)/WaitForSeconds(5)�� test�� �̹Ƿ� ����� ��
public class FadeUI : MonoBehaviour
{
    public enum FadeState
    {
        None,
        FadeingIn,
        Fade,
        FadingOut
    }
    public FadeState fadeState = FadeState.None;
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
        fadeState = FadeState.FadingOut;
        while (fadeAlpha < 1.0f)
        {
            fadeAlpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            fadeUI.color = new Color(0, 0, 0, fadeAlpha);
        }
        fadeState = FadeState.Fade;
        //yield return new WaitForSeconds(3);
        //gameObject.SetActive(false);
    }

    IEnumerator FadeInCoroutine()
    {
        float fadeAlpha = 1;
        fadeState = FadeState.FadeingIn;
        while (fadeAlpha > 0f)
        {
            fadeAlpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            fadeUI.color = new Color(0, 0, 0, fadeAlpha);
        }
        fadeState = FadeState.None;

        //yield return new WaitForSeconds(3);
        //gameObject.SetActive(false);
    }
}
