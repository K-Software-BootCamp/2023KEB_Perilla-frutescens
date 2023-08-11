using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleRecordManager : MonoBehaviour
{
    public TextMeshProUGUI[] clearTimeTexts;
    private List<ClearTimeData> clearTimeList;

    void Start()
    {
        // ClearTimeManager�� ã�Ƽ� LoadClearTimesFromPlayerPrefs() �޼��� ȣ��
        ClearTimeManager clearTimeManager = FindObjectOfType<ClearTimeManager>();
        if (clearTimeManager != null)
        {
            clearTimeManager.LoadClearTimesFromPlayerPrefs();
            clearTimeList = clearTimeManager.clearTimeList;
        }
        else
        {
            clearTimeList = new List<ClearTimeData>();
        }

        UpdateClearTimeUI();
    }

    void UpdateClearTimeUI()
    {
        // Ŭ���� Ÿ�� �����͸� TMPro UI�� ǥ��
        for (int i = 0; i < clearTimeTexts.Length; i++)
        {
            if (i < clearTimeList.Count)
            {
                clearTimeTexts[i].text = $"{i + 1}. {clearTimeList[i].clearTime:F2} seconds";
            }
            else
            {
                clearTimeTexts[i].text = $"{i + 1}. -";
            }
        }
    }
}
