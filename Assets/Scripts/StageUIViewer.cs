using UnityEngine;
using TMPro;

public class StageUIViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP; //플레이어 체력 출력 UI
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; //플레이어 골드 출력 UI
    [SerializeField]
    private TextMeshProUGUI textWave; //현재 웨이브 / 총 웨이브 출력 UI
    [SerializeField]
    private TextMeshProUGUI textEnemyCount; //현재 적 숫자 / 최대 적 숫자 UI
    [SerializeField]
    private PlayerHP playerHP; //플레이어 체력 정보
    [SerializeField]
    private PlayerGold playerGold; //플레이어 골드 정보
    [SerializeField]
    private WaveSystem waveSystem; //웨이브 정보
    [SerializeField]
    private EnemySpawner enemySpawner; //적 정보

    private void Update()
    {
        textPlayerHP.text = $"{playerHP.CurrentHP}/{playerHP.MaxHP}";
        textPlayerGold.text = $"{playerGold.CurrentGold}";
        textWave.text = $"{waveSystem.CurretWave}/{waveSystem.MaxWave}";
        textEnemyCount.text = $"{enemySpawner.CurrentEnemyCount}/{enemySpawner.MaxEnemyCount}";
    }
}
