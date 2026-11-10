using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScreenRed : MonoBehaviour
{
    private Image imageScreen; //전체화면을 덮는 빨간색 이미지

    private void Awake()
    {
        imageScreen = GetComponent<Image>();
    }

    public void OnHitAnimation()
    {
        StopCoroutine(nameof(Process));
        StartCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        //전체화면 크기로 배치된 imageScreen의 색상을 color 변수에 저장
        //imageScreen의 투명도를 40%로 설정
        Color color = imageScreen.color;
        color.a = 0.4f;
        imageScreen.color = color;

        //투명도가 0%가 될때까지 감소
        while(color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreen.color = color;

            yield return null;
        }
    }
}
