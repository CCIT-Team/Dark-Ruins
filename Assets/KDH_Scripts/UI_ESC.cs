using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
public class UI_ESC : UIPopup
{
    enum GameObjects
    {

    }
    enum Buttons
    {
        GTMM,
        ALTF4
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
        GetButton((int)Buttons.GTMM).gameObject.BindUIEvent(GTMM);
        return true;
    }
    protected void GTMM(PointerEventData _)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }
}
