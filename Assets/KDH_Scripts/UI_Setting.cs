using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using UnityEngine.UI;
public class UI_Setting : UIPopup
{
    enum GameObjects
    {
        VolumeSize
    }
    enum Buttons
    {
        VolumeActive,
        SettingClose
    }
    enum Texts
    {

    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        GetObject((int)GameObjects.VolumeSize).gameObject.BindUIEvent(SetVolume,Utils.Defines.UIEventTypes.DRAG);
        GetButton((int)Buttons.VolumeActive).gameObject.BindUIEvent(OnOffVolume);
        GetButton((int)Buttons.SettingClose).gameObject.BindUIEvent(GetComponentInParent<UI_TitleScene>().CloseSetting);
        bool b = (bool)DataLoader.Instance.FindByName("Player")["VolumeActive"];
#if UNITY_EDITOR
        Debug.Log($"{DataLoader.Instance.FindByName("Player")["VolumeSize"]}");
#endif
        GetObject((int)GameObjects.VolumeSize).gameObject.GetComponent<Slider>().value=float.Parse($"{DataLoader.Instance.FindByName("Player")["VolumeSize"]}");
        GetObject((int)GameObjects.VolumeSize).gameObject.SetActive(b);
        if (b == true)
        {
            GetButton((int)Buttons.VolumeActive).GetComponentInChildren<TextMeshProUGUI>().text = "Sound\nON";
        }
        else
        {
            GetButton((int)Buttons.VolumeActive).GetComponentInChildren<TextMeshProUGUI>().text = "Sound\nOFF";
        }
        
        return true;
    }
    protected void SetVolume(PointerEventData _)
    {
        DataLoader.Instance.SetByName("Player","VolumeSIze", $"{_.pointerDrag.transform.GetComponent<Slider>().value / 15}");
        if ((bool)DataLoader.Instance.FindByName("Player")["VolumeActive"] == false)
        {
            return;
        }
        //Play
    }
    protected void OnOffVolume(PointerEventData _)
    {
        bool b = !(bool)DataLoader.Instance.FindByName("Player")["VolumeActive"];
        DataLoader.Instance.SetByName("Player", "VolumeActive", b);
        //키고끄는 로직 추가예정
        if (b == true)
        {
            GetObject((int)GameObjects.VolumeSize).gameObject.SetActive(true);
            GetButton((int)Buttons.VolumeActive).GetComponentInChildren<TextMeshProUGUI>().text = "Sound\nON";
        }
        else
        {
            GetObject((int)GameObjects.VolumeSize).gameObject.SetActive(false);
            GetButton((int)Buttons.VolumeActive).GetComponentInChildren<TextMeshProUGUI>().text = "Sound\nOFF";
        }
    }
}
