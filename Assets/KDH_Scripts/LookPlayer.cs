using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class LookPlayer : UIPopup
{
    enum GameObjects
    {
        Canvas
    }
    enum Images
    {

    }
    private Transform cam;
    private void Awake()
    {
        cam = Camera.main.transform;
        BindObject(typeof(GameObjects));
        GetObject((int)GameObjects.Canvas).SetActive(false);
    }

    public void On(ItemBase item)
    {
        
        var data = DataLoader.Instance.FindByName(item.GetType().Name);
        if (data == null)
        {
            return;
        }
        GetObject((int)GameObjects.Canvas).SetActive(true);
        string s= $"{data["Name"]}\n";
        if(item is IWeapon_KSM)
        {
            s += $"공격력 {data["Damage"]} / 약점 {data["CriticalDamage"]}\n" +
                $"공격 속도 {data["AttackSpeed"]}\n";
            if (item is GunBase)
            {
                s += $"탄창 {((GunBase)item).GetAmmos}/{data["MaxAmmo"]}\n";
            }
        }
        else
        {
            s += $"개수 {item.Count}/{data["MAX"]}\n";
        }
        s += $"{data["Description"]}";
        GetObject((int)GameObjects.Canvas).GetChild<TextMeshProUGUI>("Text").text = s;
    }
    private void FixedUpdate()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
    }
}
