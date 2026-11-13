using UnityEngine;
using System.Collections;
using TMPro;

public class TMPAlpha : MonoBehaviour
{
    [SerializeField]
    private float duration = 0.5f;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void FadeOut()
    {
        StartCoroutine(OnFade(1, 0));
    }

    private IEnumerator OnFade(float start, float end)
    {
        float percent = 0f;
        while(percent < 1f)
        {
            //duration 시간 동안 while() 반복문 실행
            percent += Time.deltaTime / duration;

            //Text - TextMeshPro의 폰트 투명도를 start에서 end로 변경
            Color color = text.color;
            color.a = Mathf.Lerp(start, end, percent);
            text.color = color;

            yield return null;
        }
    }
}
