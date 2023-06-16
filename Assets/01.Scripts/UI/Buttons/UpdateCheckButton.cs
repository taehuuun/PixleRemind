using System;
using UnityEngine;

public class UpdateCheckButton : MonoBehaviour
{
    public GameObject noticeIcon;
    public UpdateManager updateManager;

    private void Start()
    {
        noticeIcon.SetActive(false);

        updateManager.OnUpdateCheckCompleted += HandleUpdateCheckCompleted;
    }

    public void OnClick()
    {
        _ = updateManager.CheckForUpdated();
    }

    private void HandleUpdateCheckCompleted(bool isUpdateNeed)
    {
        noticeIcon.SetActive(isUpdateNeed);
    }
}