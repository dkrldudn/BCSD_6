using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves; //현재 스테이지의 모든 웨이브 정보
    [SerializeField]
    private EnemySpawner enemySpawner;

    private int currentWaveIndex = -1; //현재 웨이브 인덱스

    //웨이브 정보 출력을 위한 Get 프로퍼티(현재 웨이브, 총 웨이브)
    public int CurretWave => currentWaveIndex+1; //시작이 0이기 때문에 +1
    public int MaxWave => waves.Length;

    public void StartWave()
    {
        //현재 맵에 적이 없고 wave가 남아있으면
        if(enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length-1)
        {
            //인덱스의 시작이 -1이기 때문에 웨이브 인덱스 증가를 제일 먼저 함
            currentWaveIndex++;
            //EnemeySpawner의 StartWave() 메소드 호출. 매개변수로 현재 웨이브 정보 제공
            enemySpawner.StartWave(waves[currentWaveIndex]);
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime; //현재 웨이브 적 생성 추가
    public int maxEnemyCount; //현재 웨이브 적 등장 숫자
    public GameObject[] enemyPrefabs; //현재 웨이브 적 등장 종류
}
