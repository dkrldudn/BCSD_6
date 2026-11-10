using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    private EnemyHP enemyHP;
    private Slider slider;

    public void Setup(EnemyHP enemyHP)
    {
        slider = GetComponent<Slider>();
        this.enemyHP = enemyHP;
    }

    private void Update()
    {
        slider.value = enemyHP.CurrentHP / enemyHP.MaxHP;
    }
}
