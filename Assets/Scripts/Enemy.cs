using UnityEngine;
using System.Collections;

public enum EnemyDestroyType {kill = 0, Arrive}

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform hudPoint;
    [SerializeField]
    private Transform rotator;
    [SerializeField]
    private float timeOffset = 1f; //이동시간 = 거리*timeOffset
    [SerializeField]
    private int gold = 10; //적 사망 시 획득 가능한 골드 

    private Transform[] wayPoints; //이동 경로 정보
    private int wayPointCount; //이동 경로 개수
    private int currentIndex = 0; //현재 목표지점 인덱스
    private EnemySpawner enemySpawner; //적의 삭제를 본인이 하지 않고 EnemySpawner에게 알려서 삭제

    public Transform HUDPoint => hudPoint;


    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        //적을 생성한 EnemySpawner 저장
        this.enemySpawner = enemySpawner;

        //적 이동 경로(WayPoints) 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = wayPoints;

        //적 위치를 첫 번째 wayPoint 위치로 설정
        transform.position = wayPoints[currentIndex].position;

        currentIndex++;

        //적 이동 제어
        StartCoroutine(nameof(Process));
    }

    private void Update()
    {
        //적 캐릭터를 화면에 출력하는 Renderer 오브젝트 회전
        rotator.Rotate(Vector3.forward, 360*Time.deltaTime);
    }

    private IEnumerator Process()
    {
        while (true)
        {
            //현재 위치에서 목표 위치(WayPoint)까지 이동
            yield return StartCoroutine(MoveAToB(transform.position, wayPoints[currentIndex].position));

            //다음 이동 위치(WayPoint) 설정
            if(currentIndex < wayPointCount-1) currentIndex++;
            else
            {
                //목표 지점에 도달해서 사망할 때는 gold를 주지 않도록 처리
                gold = 0;

                OnDie(EnemyDestroyType.Arrive);
            }
        }
    }

    private IEnumerator MoveAToB(Vector3 start, Vector3 end)
    {
        float percent = 0f;
        float moveTime = Vector3.Distance(start, end)*timeOffset;

        while(percent < 1f)
        {
            percent += Time.deltaTime / moveTime;
            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        //EnemySpawner에서 리스트로 적 정보를 관리하기 때문에 Destroy()를 직접하지 않고,
        //EnemySpawner에게 본인이 삭제될 때 피룡한 처리를 하도록 DestroyEnemy() 메소드 호출
        enemySpawner.DestroyEnemy(type, this, gold);
    }
}
