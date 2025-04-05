#nullable enable
using TMPro;
using UnityEngine;

namespace AppService.Runtime
{
    public class CustomText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI? textMeshProUGUI;

        public void SetTextSafe(string text)
        {
            if (textMeshProUGUI == null)
            {
                Debug.LogError("TextMeshProUGUI is null");
                return;
            }
            textMeshProUGUI.text = text;
        }

        public string GetTextSafe()
        {
            if (textMeshProUGUI == null)
            {
                Debug.LogError("TextMeshProUGUI is null");
                return "";
            }
            return textMeshProUGUI.text;
        }

        public void SetColorSafe(Color color)
        {
            if (textMeshProUGUI == null)
            {
                Debug.LogError("TextMeshProUGUI is null");
                return;
            }
            textMeshProUGUI.color = color;
        }
    }
}