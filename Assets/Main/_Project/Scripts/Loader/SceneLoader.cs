using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
namespace _Project.Scripts.Loader
{

    public class SceneLoader
    {
        [Inject] private LoaderMediator _loaderMediator;

        AsyncOperation _asyncLoad;

        public async void LoadLevel(int sceneIndex)
        {
            if (_asyncLoad != null && !_asyncLoad.isDone) return;

            _loaderMediator.StartLoading();
            _asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            while (!_asyncLoad.isDone)
            {
                var fillAmount = Mathf.Clamp01(_asyncLoad.progress / 0.9f);
                _loaderMediator.Loading(fillAmount);
                await Task.Yield();
            }
            await Task.Delay(10);
            _loaderMediator.FinishLoading();

        }
        public async void LoadLevel(string levelName)
        {
            if (_asyncLoad != null && !_asyncLoad.isDone) return;
            _loaderMediator.StartLoading();

            _asyncLoad = SceneManager.LoadSceneAsync(levelName);
            while (!_asyncLoad.isDone)
            {
                var fillAmount = Mathf.Clamp01(_asyncLoad.progress / 0.9f);
                _loaderMediator.Loading(fillAmount);
                await Task.Yield();
            }
            await Task.Delay(10);
            _loaderMediator.FinishLoading();

        }
        public async void LoadLevelWithSplash(string levelName, Action action)
        {
            if (_asyncLoad != null && !_asyncLoad.isDone) return;

            _loaderMediator.StartSplashLoading();
            await Task.Delay(500);
            _asyncLoad = SceneManager.LoadSceneAsync(levelName);
            while (!_asyncLoad.isDone) await Task.Yield();
            _loaderMediator.StopSplashLoading();
            await Task.Delay(1000);
            // Thread.Sleep(10);
            _loaderMediator.FinishSplashLoading();
            action?.Invoke();
        }
        public async void LoadLevelWithSplash(string levelName)
        {
            if (_asyncLoad != null && !_asyncLoad.isDone) return;

            _loaderMediator.StartSplashLoading();
            await Task.Delay(500);
            _asyncLoad = SceneManager.LoadSceneAsync(levelName);
            while (!_asyncLoad.isDone) await Task.Yield();
            _loaderMediator.StopSplashLoading();
            await Task.Delay(500);
            // Thread.Sleep(10);
            _loaderMediator.FinishSplashLoading();
          
        }

        public void TestLog()
        {
            Debug.Log("SceneLoader Test");
        }
    }

}