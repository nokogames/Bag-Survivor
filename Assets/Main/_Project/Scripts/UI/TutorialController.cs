using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Level;
using Cysharp.Threading.Tasks;
using Pack.GameData;
using UnityEngine;
using UnityEngine.XR;
using VContainer;
namespace _Project.Scripts.UI
{

    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private GameObject killTheEnemyTutorial;
        [SerializeField] private GameObject swipeTutorial;
        [SerializeField] private GameObject swipeHand;
        [Inject] private SavedTutorialData _savedData;
        [Inject] private InLevelEvents _inLevelEvents;
        public SavedTutorialData SavedTutorialData => _savedData;
        private void Start()
        {
            SetKillTheEnemyTutorial(false);
            CustomExtentions.ColorLog("SavedDat" + _savedData, Color.blue);
            _inLevelEvents.onNextLevel += OnNextLevel;
        }


        private void OnNextLevel()
        {
            SetKillTheEnemyTutorial(true);
            WaitAndWork(5f, () =>
           SetKillTheEnemyTutorial(false)

            ).Forget();

        }
        private CancellationTokenSource _cancellationTokenSource;

        public void SetSwipeTutorial(bool status, Vector3 startPos, Vector3 endPos)
        {
            if (status)
            {
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();

                swipeHand.transform.position = startPos;
                swipeTutorial.SetActive(status);
                SwipeAnim(startPos, endPos, _cancellationTokenSource.Token).Forget();
            }
            else
            {
                _cancellationTokenSource?.Cancel();
                swipeTutorial.SetActive(false);
            }
        }

        private async UniTaskVoid SwipeAnim(Vector3 startPos, Vector3 endPos, CancellationToken cancellationToken)
        {
            float duration = 1f;

            while (!cancellationToken.IsCancellationRequested)
            {
                float elapsedTime = 0f;

                // Başlangıçtan bitişe doğru hareket
                while (elapsedTime < duration && !cancellationToken.IsCancellationRequested)
                {
                    elapsedTime += Time.unscaledDeltaTime; // unscaledDeltaTime kullanarak zaman hesaplama
                    swipeHand.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
                    await UniTask.Yield();
                }


            }

            // Animasyon iptal edildiğinde başlangıç pozisyonuna geri dön
            swipeHand.transform.position = startPos;
        }

        public async UniTaskVoid WaitAndWork(float second, Action action)
        {
            await UniTask.WaitForSeconds(second);
            action?.Invoke();
        }



        public void SetKillTheEnemyTutorial(bool status)
        {
            killTheEnemyTutorial.SetActive(status);
        }
    }

}