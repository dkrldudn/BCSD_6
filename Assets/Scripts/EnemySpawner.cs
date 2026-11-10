using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; //적 프리팹
    [SerializeField]
    private GameObject enemyHPPrefab; //적 체력을 출력하는 Slider UI 프리팹
    [SerializeField]
    private Transform  hpUIParent; //체력 UI가 충족될 부모 오브젝트
    [SerializeField]
    private float spawnTime; //적 생성 추가
    [SerializeField]
    private Transform[] wayPoints; //현재 스테이지의 이동 경로
    [SerializeField]
    private PlayerHP playerHP; //플레이어의 체력 정보
    [SerializeField]
    private PlayerGold playerGold; //플레이어 골드 정보

    private List<Enemy> enemyList;

    //적의 생성과 삭제는 EnemySpawer에서 하기 때문에 set은 필요 없다.
    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {   
        //적 리스트 메모리 할당
        enemyList = new List<Enemy>();
        //적 생성 코루틴 메소드 호출
        StartCoroutine(nameof(SpawnEnemy));
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            GameObject clone = Instantiate(enemyPrefab); //적 오브젝트 생성
            Enemy enemy = clone.GetComponent<Enemy>(); //방금 생성된 적의 Enemy 컴포넌트

            //this는 나 자신(자신의 EnemySpawer 정보)
            enemy.Setup(this, wayPoints); //wayPoints 정보를 매개변수로 Setup() 메소드 호출
            enemyList.Add(enemy); //리스트에 방금 생성한 적 정보 저장

            SpawnEnemyHP(clone); //적 체력을 출력하는 UI 생성 및 설정

            yield return new WaitForSeconds(spawnTime); //spawnTime 시간 동안 대기
        }
    }

    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold)
    {
        //적이 목표지점까지 도착했을 때
        if(type == EnemyDestroyType.Arrive)
        {
            //플레이어 체력 -1
            playerHP.TakeDamage(1);
        }
        //적이 플레이어의 공격에 사망했을 때
        else if(type == EnemyDestroyType.kill)
        {
            //적의 종류에 따라 사망 시 골드 획득
            playerGold.CurrentGold += gold;
        }
        //리스트에서 사망하는 적 정보 삭제
        enemyList.Remove(enemy);
        //적 오브젝트 삭제
        Destroy(enemy.gameObject);
    }
    
    private void SpawnEnemyHP(GameObject enemy)
    {
        //적 체력을 출력하는 UI 생성
        var clone = Instantiate(enemyHPPrefab);
        //UI 오브젝트를 parent("Canvas" or "Canvas"의 자식으로 설정)
        clone.transform.SetParent(hpUIParent);
        //계층 설정으로 바뀐 크기를 다시 (1, 1, 1)로 설정
        clone.transform.localScale = Vector3.one;

        //UI가 추적할 대상을 방금 생성한 적 오브젝트로 설정
        clone.GetComponent<FollowTargetUI>().SetTarget(enemy.GetComponent<Enemy>().HUDPoint);
        //UI에 방금 생성한 적 오브젝트의 체력 정보를 출력하도록 설정
        clone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
