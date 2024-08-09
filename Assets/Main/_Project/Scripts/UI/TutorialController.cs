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
        [SerializeField] private GameObject skillTapTutorial;
        [SerializeField] private GameObject skillTapHand;
        [SerializeField] private GameObject swipeHand;
        [SerializeField] private GameObject goBtnHand;
        [Inject] private SavedTutorialData _savedData;
        [Inject] private InLevelEvents _inLevelEvents;
        [Inject] private GameData _gameData;
        public SavedTutorialData SavedTutorialData => _savedData;
        private void Start()
        {
            skillTapTutorial.SetActive(false);
            goBtnHand.SetActive(false);
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
        #region Swipe
        public void StartSwipeTutorial(Vector3 startPos)
        {

            if (_savedData.isCompletedSwipeTutorial) return;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            swipeHand.transform.position = startPos;
            swipeTutorial.SetActive(true);
            SwipeAnim(startPos, _cancellationTokenSource.Token).Forget();

        }
        internal void CompletedSwipe()
        {
            _savedData.isCompletedSwipeTutorial = true;
            _cancellationTokenSource?.Cancel();
            swipeTutorial.SetActive(false);
            StartGoBtnTutorial();
        }



        private async UniTaskVoid SwipeAnim(Vector3 startPos, CancellationToken cancellationToken)
        {
            float duration = 1f;
            float scaleDuration = 0.5f; // Objenin küçülüp büyüme süresi
            Vector3 originalScale = swipeHand.transform.localScale;
            Vector3 minScale = originalScale * 0.8f; // Objenin küçüleceği minimum boyut
            var endPos = new Vector3(Screen.width / 2, Screen.height / 4, Camera.main.nearClipPlane);
            endPos.z = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                float elapsedTime = 0f;

                // Obje küçülsün
                while (elapsedTime < scaleDuration && !cancellationToken.IsCancellationRequested)
                {
                    elapsedTime += Time.unscaledDeltaTime;
                    swipeHand.transform.localScale = Vector3.Lerp(originalScale, minScale, elapsedTime / scaleDuration);
                    await UniTask.Yield();
                }

                // // Obje büyüsün
                // elapsedTime = 0f;
                // while (elapsedTime < scaleDuration && !cancellationToken.IsCancellationRequested)
                // {
                //     elapsedTime += Time.unscaledDeltaTime;
                //     swipeHand.transform.localScale = Vector3.Lerp(minScale, originalScale, elapsedTime / scaleDuration);
                //     await UniTask.Yield();
                // }

                // Obje swipe hareketi yapsın
                elapsedTime = 0f;
                while (elapsedTime < duration && !cancellationToken.IsCancellationRequested)
                {
                    elapsedTime += Time.unscaledDeltaTime;
                    swipeHand.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
                    await UniTask.Yield();
                }

                // Başlangıç pozisyonuna ve boyutuna geri dön
                swipeHand.transform.position = startPos;
                swipeHand.transform.localScale = originalScale;
            }

            // Animasyon iptal edildiğinde başlangıç pozisyonuna ve boyutuna geri dön
            swipeHand.transform.position = startPos;
            swipeHand.transform.localScale = originalScale;
        }

        public async UniTaskVoid WaitAndWork(float second, Action action)
        {
            await UniTask.WaitForSeconds(second);
            action?.Invoke();
        }

        #endregion

        public void SetKillTheEnemyTutorial(bool status)
        {
            killTheEnemyTutorial.SetActive(status);
        }
        #region  GoBtn
        public Vector3 GoBtnPosition { get; set; }

        private CancellationTokenSource _goBtncancellationTokenSource;
        private void StartGoBtnTutorial()
        {
            _goBtncancellationTokenSource?.Cancel();
            if (_savedData.isCompletedGoBtnTutorial) return;
            _goBtncancellationTokenSource = new CancellationTokenSource();
            goBtnHand.SetActive(true);
            ScaleUpDowAnim(_goBtncancellationTokenSource.Token, goBtnHand.transform, GoBtnPosition).Forget();
        }
        public void StopGoBtnTutorial()
        {
            _goBtncancellationTokenSource?.Cancel();
            goBtnHand.SetActive(false);
        }
        private async UniTaskVoid ScaleUpDowAnim(CancellationToken cancellationToken, Transform animTransform, Vector3 position)
        {

            float scaleDuration = 0.5f; // Objenin küçülüp büyüme süresi
            Vector3 originalScale = animTransform.localScale;
            Vector3 minScale = originalScale * 0.8f; // Objenin küçüleceği minimum boyut
            animTransform.position = position;
            while (!cancellationToken.IsCancellationRequested)
            {
                float elapsedTime = 0f;

                // Obje küçülsün
                while (elapsedTime < scaleDuration && !cancellationToken.IsCancellationRequested)
                {
                    elapsedTime += Time.unscaledDeltaTime;
                    animTransform.localScale = Vector3.Lerp(originalScale, minScale, elapsedTime / scaleDuration);
                    await UniTask.Yield();
                }

                // Obje büyüsün
                elapsedTime = 0f;
                while (elapsedTime < scaleDuration && !cancellationToken.IsCancellationRequested)
                {
                    elapsedTime += Time.unscaledDeltaTime;
                    animTransform.localScale = Vector3.Lerp(minScale, originalScale, elapsedTime / scaleDuration);
                    await UniTask.Yield();
                }
                animTransform.localScale = originalScale;
            }
        }

        internal void GoBtnClicked()
        {
            StopGoBtnTutorial();

            if (_savedData.isCompletedSwipeTutorial) _savedData.isCompletedGoBtnTutorial = true;
            _savedData.isCompletedGoBtnTutorial = true;
            _gameData.Save();
        }
        #endregion


        public Vector3 TapToSkillStartPoint { get; set; }
        #region  TapToSkill
        private CancellationTokenSource _tapToSkillTokenSource;


        public void StartTapToSkillTutorial()
        {
            _tapToSkillTokenSource?.Cancel();
            if (_savedData.isCompletedTapTutorial || !_savedData.isCompletedSwipeTutorial) return;
            skillTapTutorial.SetActive(true);
            _tapToSkillTokenSource = new CancellationTokenSource();
            skillTapHand.SetActive(true);
            ScaleUpDowAnim(_tapToSkillTokenSource.Token, skillTapHand.transform, TapToSkillStartPoint).Forget();

        }
        public void SkillTaped()
        {
            skillTapTutorial.SetActive(false);
            _tapToSkillTokenSource?.Cancel();
            _savedData.isCompletedTapTutorial = true;


        }
        #endregion
    }

}