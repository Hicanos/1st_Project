using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data;

public class Skill : MonoBehaviour
{
    private Image remainGaugeBar;
    private TextMeshProUGUI remainText;

    private int remainSkillCnt; // 현재 남은 스킬 사용 가능 횟수
    private readonly int maxSkillCnt = 5; // 최대 스킬 사용 가능 횟수

    private void Awake()
    {
        remainSkillCnt = maxSkillCnt;

        //구성을 단순화하기 위해 이렇게 초기화했습니다. GetChild를 활용해서 초기화하는 방법은 권장되지 않습니다.
        remainGaugeBar = transform.GetChild(0).GetComponent<Image>();
        remainText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        SetSkillUI();
    }

    public void UseSkill()
    {
        if (remainSkillCnt <= 0) return;

        Debug.Log("스킬을 사용했다.");
        remainSkillCnt--;
        SetSkillUI();
    }

    void SetSkillUI()
    {
        //TODO
        //remainGaugeBar, remainText을 활용할 것
        // Fillamount를 조정
        //Text는 remainText/maxSkillCnt를 string으로 작성

        remainGaugeBar.fillAmount = remainSkillCnt / maxSkillCnt;
        remainText.text = $"{remainSkillCnt.ToString()}/{maxSkillCnt.ToString()}";

    }
}