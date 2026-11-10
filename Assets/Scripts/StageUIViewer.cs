using UnityEngine;
using TMPro;

public class StageUIViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP; //플레이어 체력 출력 UI
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; //플레이어 골드 출력 UI
    [SerializeField]
    private PlayerHP playerHP; //플레이어 체력 정보
    [SerializeField]
    private PlayerGold playerGold; //플레이어 골드 정보

    private void Update()
    {
        textPlayerHP.text = $"{playerHP.CurrentHP}/{playerHP.MaxHP}";
        textPlayerGold.text = $"{playerGold.CurrentGold}";
    }
}
