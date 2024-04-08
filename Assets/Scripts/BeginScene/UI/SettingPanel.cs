using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button buttonQuit;
    public Toggle toggleMusic, toggleSound;
    public Slider sliderMusic, sliderSound;
    public override void Init()
    {
        MusicData data = DataManager.Instance.musicData;
        toggleMusic.isOn = data.musicOpen;
        toggleSound.isOn = data.soundOpen;
        sliderMusic.value = data.musicVal; 
        sliderSound.value = data.soundVal;

        buttonQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<SettingPanel>();
            DataManager.Instance.SaveMusicData();
        });

        sliderMusic.onValueChanged.AddListener((val) =>
        {
            DataManager.Instance.musicData.musicVal = val;
            BkSource.Instance.ChangeVal(val);
        });

        sliderSound.onValueChanged.AddListener((val) =>
        {
            DataManager.Instance.musicData.soundVal = val;
        });

        toggleMusic.onValueChanged.AddListener((val) =>
        {
            DataManager.Instance.musicData.musicOpen = val;
            BkSource.Instance.SetIsOpen(val);
        });

        toggleSound.onValueChanged.AddListener((val) =>
        {
            DataManager.Instance.musicData.soundOpen = val;
        });
    }

    
}
