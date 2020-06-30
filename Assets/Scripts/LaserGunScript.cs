using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    //public Light impactLight;
    public Transform firePoint;
    public Transform endPoint;

    public float damage = 10;
    public float maxAmmo = 100;
    public float usageRate = 1;
    public float rechargeRate = 5;

    private float ammo;

    private void Fire()
    {
        if (ammo < 0)
        {
            lineRenderer.enabled = false;
            impactEffect.Stop();
            //impactLight.enabled = true;
            return;
        }

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            //impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, endPoint.position);

        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit[] hitObjects = Physics.RaycastAll(ray);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            if (hitObjects[i].transform.CompareTag("Enemy"))
                hitObjects[i].transform.GetComponent<EnemyControllerScript>().hp -= damage;
        }

        ammo -= usageRate;
    }

    private void RechargeAmmo()
    {
        ammo += rechargeRate;
        ammo = (ammo > maxAmmo) ? maxAmmo : ammo;
    }

    private void OnValidate()
    {
        maxAmmo = Mathf.Max(0, maxAmmo);
        usageRate = Mathf.Max(0, usageRate);
        rechargeRate = Mathf.Max(0, rechargeRate);
    }

    private void Start()
    {
        ammo = maxAmmo;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
            impactEffect.Play();
            //impactLight.enabled = false;
        }
        else
        {
            if (ammo < maxAmmo)
            {
                RechargeAmmo();
            }
        }
    }
}
