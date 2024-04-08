using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button buttonStart, buttonQuit, buttonSetting;
    public override void Init()
    {
        buttonStart.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<BeginPanel>();
            Camera.main.GetComponent<CameraAni>().TurnLeft(() =>
            {
                UIManager.Instance.ShowPanel<ChooseHeroPanel>();
            });
        });

        buttonSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        buttonQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });

    }

    
}
