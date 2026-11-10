using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP; //최대 체력
    private float currentHP; //현재 체력
    private bool isDie = false; //적이 사망 상태이면 isDie를 true로 설정
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        //현재 체력을 최대 체력과 같게 설정
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        //여러 타워의 공격을 동시에 받으면 enemy.OnDie()메소드가
        //여러 번 실행될 수 있기 때문에 변수를 이용해
        //사망 상태일 때는 실행하지 않도록 통제
        if(isDie == true) return;

        //현재 체력을 damage만큼 감소
        currentHP -= damage;

        StopCoroutine(nameof(HitAlphaAnimation));
        StartCoroutine(nameof(HitAlphaAnimation));

        //체력이 0이하이면 적 캐릭터 사망
        if(currentHP <= 0)
        {
            isDie = true;
            //적 캐릭터 사망
            enemy.OnDie(EnemyDestroyType.kill);
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        //현재 적 색상을 color 변수에 저장
        Color color = spriteRenderer.color;

        //적의 투명도를 40%로 설정
        color.a = 0.4f;
        spriteRenderer.color = color;

        //0.05초 동안 대기
        yield return new WaitForSeconds(0.05f);

        //적의 투명도를 100%로 설정
        color.a = 1.0f;
        spriteRenderer.color = color;
    }
}
