#nullable enable
using UnityEngine;

namespace AppService.Runtime
{
    public static class CanvasGroupExtensions
    {
        public static void SetInteractableSafe(this CanvasGroup? canvasGroup, bool value)
        {
            if (canvasGroup != null)
                canvasGroup.interactable = value;    
        }
        
        public static void SetAlphaSafe(this CanvasGroup? canvasGroup, float value)
        {
            if (canvasGroup != null)
                canvasGroup.alpha = value;    
        }
    }
}