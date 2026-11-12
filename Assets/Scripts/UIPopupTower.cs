using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class UIPopupTower : MonoBehaviour
{
    [SerializeField]
    private Image imageTower;
    [SerializeField]
    private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textRate;
    [SerializeField]
    private TextMeshProUGUI textRange;
    [SerializeField]
    private TextMeshProUGUI textLevel;
    [SerializeField]
    private TowerAttackRange towerAttackRange;
    
    private TowerWeapon currentTower;

    private void Awake()
    {
        OffPopup();
    }

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OffPopup();
        }
    }

    public void OnPopup(Transform towerWeapon)
    {
        //출력해야하는 타워 정보를 받아와서 저장
        currentTower = towerWeapon.GetComponent<TowerWeapon>();
        //타워 정보 갱신
        UpdateTowerDate();
        //타워 정보 Popup On
        gameObject.SetActive(true);
        //타워 오브젝트 주변에 출력하는 타워 공격 범위 활성화
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
    }

    public void OffPopup()
    {
        //타워 정보 Popup Off
        gameObject.SetActive(false);
        //타워 공격 범위 활성화
        towerAttackRange.OffAttackRange();
    }

    private void UpdateTowerDate()
    {
        textDamage.text = $"Damage : {currentTower.Damage}";
        textRate.text = $"Ratee : {currentTower.Rate}";
        textRange.text = $"Range : {currentTower.Range}";
        textLevel.text = $"Level : {currentTower.Level}";
    }
}
