using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;
using Utils.Defines;
using UnityEngine.SceneManagement;
public class UI_TitleScene : UIScene
{
    enum GameObjects
    {
        SettingMenu
    }
    enum Buttons
    {
        Start,
        Setting,

    }
    enum Texts
    {

    }
    bool isPreload = false;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        // 오브젝트 바인딩
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        
        GetButton((int)Buttons.Start).gameObject.BindUIEvent((_) =>
        {
            //if (isPreload)
            //Managers_KSM.SceneManagerEx.LoadScene(SceneNames.LobbyScene); //이건 일회성으로 뭐 더 볼일 없기도 할 듯 하여
#if UNITY_EDITOR
            Debug.Log("GameScene");
#endif
            if (isPreload==true)
            {

                SceneManager.LoadScene("GameScene");
            }
        });
        GetButton((int)Buttons.Setting).gameObject.BindUIEvent(OpenSetting);
        return true;
    }
    private void Awake()
    {
        Init();

    }
    private void Start()
    {
        isPreload = true;
    }

    protected void OpenSetting(PointerEventData _)
    {
        GetButton((int)Buttons.Setting).gameObject.SetActive(false);
        GetButton((int)Buttons.Start).gameObject.SetActive(false);
        GetObject((int)GameObjects.SettingMenu).gameObject.SetActive(true);
    }
    public void CloseSetting(PointerEventData _)
    {
        GetObject((int)GameObjects.SettingMenu).gameObject.SetActive(false);
        GetButton((int)Buttons.Setting).gameObject.SetActive(true);
        GetButton((int)Buttons.Start).gameObject.SetActive(true);
    }
#if UNITY_EDITOR
    protected void CloseTest(PointerEventData _)
    {
        CloseSetting(default(PointerEventData));
    }
#endif
}
