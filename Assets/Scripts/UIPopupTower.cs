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
    [SerializeField]
    private Button buttonUpgrade;
    [SerializeField]
    private SystemTextViewer systemTextViewer;
    
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
        imageTower.sprite = currentTower.TowerSprite;
        textDamage.text = $"Damage : {currentTower.Damage}";
        textRate.text = $"Ratee : {currentTower.Rate}";
        textRange.text = $"Range : {currentTower.Range}";
        textLevel.text = $"Level : {currentTower.Level}";

        //업그레이드가 불가능해지면 버튼 비활성화
        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel;
    }

    public void OnclickEventTowerUpgrade()
    {
        //타워 업그레이드 시도 (성공: true, 실패: false)
        bool isSuccess = currentTower.Upgrade();

        if(isSuccess == true)
        {
            //타워가 업그레이드 되었기 떄문에 타워 정보 갱신
            UpdateTowerDate();
            //타워 주변에 보이는 공격 범위도 갱신
            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
        }
        else
        {
            //타워 업그레이드에 필요한 비용이 부족하다고 출력
            systemTextViewer.PrintText(MESSAGE.MONEY);
        }
    }

    public void OnClickEventTowerSell()
    {
        //타워 판매
        currentTower.Sell();
        //선택한 타워가 사라지기 때문에 popupUI, 공격 범위 비활성화
        OffPopup();
    }
}
