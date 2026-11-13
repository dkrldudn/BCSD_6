using UnityEngine;
using TMPro;

public enum MESSAGE {MONEY = 0, BUILD}

public class SystemTextViewer : MonoBehaviour
{
    private TextMeshProUGUI textSystem;
    private TMPAlpha tmpAlpha;

    private void Awake()
    {
        textSystem = GetComponent<TextMeshProUGUI>();
        tmpAlpha = GetComponent<TMPAlpha>();
    }

    public void PrintText(MESSAGE type)
    {
        switch(type)
        {
            case MESSAGE.MONEY:
                textSystem.text = "System : Not enough money..";
                break;
            case MESSAGE.BUILD:
                textSystem.text = "System : Invalid build tower..";
                break;
        }

        tmpAlpha.FadeOut();
    }
}
