using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife3_KSM : ItemBase, IWeapon_KSM
{
    public enum Type { Knife };
    public Type type = Type.Knife;
    public int damage = 3;
    public bool isAttacking = false;

    [SerializeField] private float _rate = 1f;
    public float rate { get { return _rate; } }

    public BoxCollider knifeArea;
    public ParticleSystem[] hitEffect;

    private bool hasPlayedHitEffectThisSwing;
    private List<IDamageable_KSM> hitTargetsList = new List<IDamageable_KSM>();

    private void Awake()
    {
        hitTargetsList = new List<IDamageable_KSM>();
    }

    public override void ItemUse(List<KeyCode> keys)
    {
        if (keys.Contains(KeyCode.Mouse0))
        {
            Use();
        }
    }

    public void Use()
    {
        if (!gameObject.activeInHierarchy || isAttacking) return;

        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        isAttacking = true;
        hitTargetsList.Clear();
        hasPlayedHitEffectThisSwing = false;

        yield return new WaitForSeconds(0.1f);

        if (knifeArea != null) knifeArea.enabled = true;

        yield return new WaitForSeconds(0.3f);

        if (knifeArea != null) knifeArea.enabled = false;

        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;

        IDamageable_KSM damageable = other.GetComponentInParent<IDamageable_KSM>();

        if (damageable == null || hitTargetsList.Contains(damageable)) return;

        bool isWeakPoint = other.GetComponent<WeakPoint_KSM>() != null;

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
        if (hitEffect == null) return;
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