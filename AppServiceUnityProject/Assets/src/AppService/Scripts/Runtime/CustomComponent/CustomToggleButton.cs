#nullable enable
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace AppService.Runtime
{
    public class CustomToggleButton : MonoBehaviour
    {
        [SerializeField] private CustomButton? customButton;
        [SerializeField] private Image onImage;
        [SerializeField] private Image offImage;

        private readonly Subject<bool> onOffSubject = new();
        private bool isActivated;
        public Observable<bool> Activated => onOffSubject;

        public void SetupSafe(bool isActivated)
        {
            if (customButton == null)
            {
                Debug.LogError("CustomButton が null です。");
                return;
            }

            this.isActivated = isActivated;
            customButton.SubscribeToClick(() =>
                {
                    this.isActivated = !this.isActivated;
                    onOffSubject.OnNext(this.isActivated);
                    UpdateView();
                }
            );
        }

        public void Publish()
        {
            onOffSubject.OnNext(isActivated);
        }

        private void UpdateView()
        {
            onImage.gameObject.SetActive(isActivated);
            offImage.gameObject.SetActive(!isActivated);
        }
    }
}