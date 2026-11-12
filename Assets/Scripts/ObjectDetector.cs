using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpanwer;
    [SerializeField]
    private UIPopupTower uiPopupTower;

    private Camera mainCamera;
    private Vector2 screenPoint;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        screenPoint = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        //버튼을 누르거나 뗄 때 호출
        if(!context.performed) return;
        //버튼을 눌렀을 때만 true 변환 (뗄 때 호출되지 않도록 처리)

        //카메라 위치에서 화면의 터치 위치를 관통하는 광선 생성
        //ray.origin: 광선의 시작 위치(=카메라 위치)
        //ray.direction: 광선의 진행 방향
        Ray ray = mainCamera.ScreenPointToRay(screenPoint);

        //2D 모니터를 통해 3D 월드의 오브젝트를 마우스로 선택하는 방법
        //광선과 충돌하는 오브젝트를 검출해 hit에 저장
        if(Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            //광선과 충돌한 오브젝트의 태그가 "Tilel"이면
            if(hit.transform.CompareTag("Tile"))
            {
                //타워를 생성하는 TrySpawnTower() 메소드 호출
                towerSpanwer.TrySpawnTower(hit.transform);
            }
            //타워를 선택하면 해당 타워 정보를 출력하는 타워 Popup UI On
            else if(hit.transform.CompareTag("Tower"))
            {
                uiPopupTower.OnPopup(hit.transform);
            }
        }
    }
}
