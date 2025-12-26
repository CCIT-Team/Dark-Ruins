using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class UI_GameScene : UIScene
{
    enum GameObjects
    {
        ESC
    }
    enum Buttons
    {

    }
    enum Texts
    {

    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        // 오브젝트 바인딩
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        return true;
    }
    private void Awake()
    {
        Init();
    }
    void OnEnable()
    {
        Managers_KSM.Input.OnKeysPressed += OnKey;
    }

    void OnDisable()
    {
        if (Managers_KSM.Instance != null)
        {
            Managers_KSM.Input.OnKeysPressed -= OnKey;
        }
    }
    #region ESC
    private bool ESCd = false;
    private void OnKey(KeyCode key)
    {
        if(key==KeyCode.Escape)
        {
            OnESC();
#if UNITY_EDITOR
            Debug.Log("TESTESC");
#endif
        }
    }

    private void OnESC()
    {
        if (ESCd == false)
        {
            Time.timeScale = 0;
            ESCd = true;
            GetObject((int)GameObjects.ESC).SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.Find("Player").GetComponent<PlayerController_KSM>().enabled = false;
            GameObject.Find("Player").GetComponentInChildren<Inventory>().UnSub();
        }
        else
        {
            Time.timeScale = 1;
            ESCd = false;
            GetObject((int)GameObjects.ESC).SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameObject.Find("Player").GetComponent<PlayerController_KSM>().enabled = true;
            GameObject.Find("Player").GetComponentInChildren<Inventory>().Sub();
        }
    }
    #endregion
}
