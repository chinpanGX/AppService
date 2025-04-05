using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Playables;

namespace AppService.Runtime
{
    public static class PlayableDirectorExtensions
    {
        public static async UniTask PlayAsyncSafe(this PlayableDirector self, CancellationToken cancellationToken)
        {
            if (self == null) return;

            self.Play();
            switch (self.extrapolationMode)
            {
                case DirectorWrapMode.Hold:
                    // Holdモードの場合は、再生時間を見る
                    await UniTask.WaitUntil(() =>
                                self.time >= self.duration, cancellationToken: cancellationToken
                        )
                        .SuppressCancellationThrow();
                    break;
                case DirectorWrapMode.None:
                    await UniTask.WaitUntil(() => self.state != PlayState.Playing, cancellationToken: cancellationToken)
                        .SuppressCancellationThrow();
                    break;
            }
        }
    }
}