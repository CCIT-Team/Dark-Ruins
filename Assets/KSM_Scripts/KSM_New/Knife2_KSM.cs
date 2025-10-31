using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Knife2_KSM : MonoBehaviour, IWeapon_KSM
{
    public enum Type { Knife, Gun };
    public Type type;
    public int damage = 3;

    [SerializeField] private float _rate = 4f;
    public float rate { get { return _rate; } }

    public BoxCollider knifeArea;
    public ParticleSystem[] hitEffect;

    private bool hasPlayedHitEffectThisSwing;

    private List<IDamageable_KSM> hitTargetsList;

    private void Awake()
    {
        hitTargetsList = new List<IDamageable_KSM>();
    }

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
        yield return new WaitForSeconds(0.1f);
        hitTargetsList.Clear();
        hasPlayedHitEffectThisSwing = false;
        knifeArea.enabled = true;

        yield return new WaitForSeconds(0.3f);
        knifeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable_KSM damageable = other.GetComponentInParent<IDamageable_KSM>();

        if (damageable == null || hitTargetsList.Contains(damageable))
        {
            return;
        }

        bool isWeakPoint = other.GetComponent<WeakPoint>() != null;

        damageable.OnDamaged(damage, transform.root, isWeakPoint);
        hitTargetsList.Add(damageable);

        if (!hasPlayedHitEffectThisSwing)
        {
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            HitParticles(hitPoint);
            hasPlayedHitEffectThisSwing = true;
        }
    }

    public void HitParticles(Vector3 position)
    {
        foreach (ParticleSystem ps in hitEffect)
        {
            if (ps != null)
            {
                ps.transform.position = position;

                ps.Play();
            }
        }
    }
}