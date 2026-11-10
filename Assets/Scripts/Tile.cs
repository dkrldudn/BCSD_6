using UnityEditor.Rendering;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //현재 타일의 타워 건설 여부를 저장/확인하는 프로퍼티
    public bool IsBuildTower{set; get;}

    private void Awake()
    {
        IsBuildTower = false;
    }
}
