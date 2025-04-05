using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AppService.Runtime
{
    public class CustomSlider : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField] private Slider slider;

        private readonly Subject<float> onPointerUpSubject = new();
        public Observable<float> OnValueChangedObservable => slider.OnValueChangedAsObservable();
        public Observable<float> OnPointerUpObservable => onPointerUpSubject;

        public void SetMinMaxValueSafe(float minValue, float maxValue)
        {
            if (slider == null)
            {
                Debug.LogError("Slider is null");
                return;
            }

            slider.minValue = minValue;
            slider.maxValue = maxValue;
        }

        public void SetValueWithNotifySafe(float value)
        {
            if (slider == null)
            {
                Debug.LogError("Slider is null");
                return;
            }
            slider.SetValueWithoutNotify(value);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (slider == null)
            {
                Debug.LogError("Slider is null");
                return;
            }

            onPointerUpSubject.OnNext(slider.value);
        }
    }
}