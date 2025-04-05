#nullable enable
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace AppService.Runtime
{
    public class CustomButton : MonoBehaviour
    {
        [SerializeField] private Button? button;
        [SerializeField] private CustomText? text;

        // クリックのクールダウン時間
        private readonly TimeSpan cooldown = TimeSpan.FromSeconds(0.1f);
        private readonly ReactiveProperty<bool> interactable = new(true);

        /// <summary>
        /// クリック時の処理を登録します。
        /// </summary>
        /// <param name="onClickAction"> クリック時の処理 </param>
        /// <param name="viewCanvasGroup"> ViewのCanvasGroup </param>
        /// <param name="token"></param>
        /// <description>
        /// ViewのCanvasGroupを追加すると View側の interactable を制御して同時押しを防ぐことができる。
        /// </description>
        public void SubscribeToClick(Action onClickAction, CanvasGroup? viewCanvasGroup = null, CancellationToken cancellationToken = default)
        {
            var token = cancellationToken == CancellationToken.None ? destroyCancellationToken : cancellationToken;
            
            if (button == null)
            {
                Debug.LogError("Button が null です。");
                return;
            }

            interactable.Subscribe(value =>
                {
                    SetInteractableSafe(value);
                    viewCanvasGroup.SetInteractableSafe(value);
                }
            ).RegisterTo(token);
            
            button.OnClickAsObservable()
                .Where(_ => interactable.Value)
                .SubscribeAwait(async (_, ct) =>
                    {
                        await ClickActionAsync(onClickAction, ct);
                    }, AwaitOperation.Drop
                )
                .RegisterTo(token);
        }

        private async UniTask ClickActionAsync(Action action, CancellationToken cancellationToken)
        {
            try
            {
                interactable.Value = false;
                action();
                await UniTask.Delay(cooldown, cancellationToken: cancellationToken);
            }
            finally
            {
                interactable.Value = true;
            }
        }

        public void SetInteractableSafe(bool value)
        {
            if (button != null)
                button.interactable = value;
        }

        public void SetTextSafe(string value)
        {
            if (text != null)
                text.SetTextSafe(value);    
        }
    }

}