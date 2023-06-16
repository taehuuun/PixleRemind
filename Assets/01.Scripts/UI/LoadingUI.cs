using System.Collections;
using LTH.PixelRemind.Managers;
using LTH.PixelRemind.Managers.Login;
using LTH.PixelRemind.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.PixelRemind.UI
{
    public class LoadingUI : BodyUI
    {
        [SerializeField] private Image _loadingBar;
        [SerializeField] private TMP_Text _taskText;
        
        private void Start()
        {
            _loadingBar.fillAmount = 0f;
            _taskText.text = "";
            LoadingTaskManager.Instance.AddTask(GPGSUtil.Init,"구글 플레이 서비스 초기화...");
            LoadingTaskManager.Instance.AddTask(LoginManager.Login, "로그인...");
            LoadingTaskManager.Instance.AddTask(LoginManager.LoadUserData, "유저 데이터 가져오는 중...");

            StartCoroutine(ShowLoadingProgress());
            _ = LoadingTaskManager.Instance.RunTasks();
        }

        private IEnumerator ShowLoadingProgress()
        {
            while (!LoadingTaskManager.Instance.AllTaskComplete)
            {
                _loadingBar.fillAmount = LoadingTaskManager.Instance.TaskProgress;
                _taskText.text = LoadingTaskManager.Instance.CurrentTask;
                
                yield return null;
            }

            _loadingBar.fillAmount = 1f;
            _taskText.text = "메인 씬으로 이동중..";
            MoveScene("MainScene");
        }
    }
}
