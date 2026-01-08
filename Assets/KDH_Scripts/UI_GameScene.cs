using DG.Tweening;
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
        Bullets
    }
    enum Images
    {
        CH,
        RCH
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        // 오브젝트 바인딩
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        GetImage((int)Images.RCH).transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        GetImage((int)Images.RCH).transform.gameObject.SetActive(false);
        GetText((int)Texts.Bullets).transform.gameObject.SetActive(false);
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
            if(Inventory.InventoryOpened==true)
            {
                Time.timeScale = 1;
            }
            ESCd = false;
            GetObject((int)GameObjects.ESC).SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameObject.Find("Player").GetComponent<PlayerController_KSM>().enabled = true;
            GameObject.Find("Player").GetComponentInChildren<Inventory>().Sub();
        }
    }
    #endregion
    #region Bullets
    public void BulletUISet(bool b=true,int c=0,int m=0)
    {
        GetText((int)Texts.Bullets).gameObject.SetActive(b);
        if(b==true)
        {
            GetText((int)Texts.Bullets).text = $"{c} / {m}";
            if(c<=0)
            {
                GetText((int)Texts.Bullets).color= Color.red;
            }
            else
            {
                GetText((int)Texts.Bullets).color = Color.white;
            }
        }
    }
 
    public async void Delay()
    {
        GetImage((int)Images.CH).transform.gameObject.SetActive(false);
        GetImage((int)Images.RCH).transform.gameObject.SetActive(true);
        await System.Threading.Tasks.Task.Delay(2000);
        GetImage((int)Images.RCH).transform.gameObject.SetActive(false);
        GetImage((int)Images.CH).transform.gameObject.SetActive(true);
    }
    #endregion
}
