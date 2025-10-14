using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knife_KSM : MonoBehaviour
{
    public enum Type { Knife, Gun };
    public Type type;
    public int damage = 3;
    public float rate = 4f;
    public BoxCollider knifeArea;
    public TrailRenderer trailEffect;

    public void Use()
    {
        if (type == Type.Knife)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    IEnumerator Swing()
    {
        //1
        yield return new WaitForSeconds(0.1f);//0.1f 대기
        knifeArea.enabled = true;
        //2
        yield return new WaitForSeconds(0.3f);//0.3f 대기
        knifeArea.enabled = false;
        //3
        yield return new WaitForSeconds(0.3f);//0.3f 대기
    }
}