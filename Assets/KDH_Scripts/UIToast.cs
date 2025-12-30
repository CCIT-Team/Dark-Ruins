
using UnityEngine;


public class UIToast : UIBase
{
    #region Enum

    enum Images
    {
        BackgroundImage
    }

    enum Texts
    {
        ToastMessageValueText,
    }
    public void OnEnable()
    {
        PopupOpenAnimation(gameObject);
    }
    #endregion
    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        #region Object Bind
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        #endregion

        Refresh();
        return true;
    }

    public void SetInfo(string msg)
    {
        transform.localScale = Vector3.one;
        GetText((int)Texts.ToastMessageValueText).text = msg;
        Refresh();
    }

    void Refresh()
    {


    }

}