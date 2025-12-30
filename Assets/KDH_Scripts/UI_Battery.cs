using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Battery : UIBase
{
    [SerializeField]
    private Sprite OnImage, OffImage;

    private FlashlightItem_KSM _fLSM;
    private UnityEngine.UI.Image _image;
    enum Images
    {
        UI_FlashLight
    }
    enum Texts
    {
        UI_Per
    }
    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        _fLSM=GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FlashlightItem_KSM>();
        _image=GetImage((int)Images.UI_FlashLight).GetComponent<UnityEngine.UI.Image>();
        Subcribe();
        return true;
    }
    protected void Subcribe()
    {
        FlashlightItem_KSM.OnUVLightToggled -= Check;
        FlashlightItem_KSM.OnUVLightToggled += Check;
    }
    protected void UnSUbcribe()
    {
        FlashlightItem_KSM.OnUVLightToggled -= Check;
    }
    protected void Check(bool b)
    {
        if(b==true)
        {
            _image.sprite = OnImage;
        }
        else
        {
            _image.sprite=OffImage;
        }
        GetText((int)Texts.UI_Per).text = $"{_fLSM.currentBattery:0}%";
    }
    private void Update()
    {
        if (FlashlightItem_KSM.isUVLightActive == false)
        {
            return;
        }
        GetText((int)Texts.UI_Per).text = $"{_fLSM.currentBattery:0}%";
    }
}
