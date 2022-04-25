using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BulletWorks : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int dmg;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float firerate;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private float range;

    [Header("Statistics")]
    [SerializeField] private TMP_Text ammoCount;

    private Transform shotSpot;
    private Camera cam;
    private LineRenderer render;

    private int curAmmo;
    private bool reloading;

    private WaitForSeconds awaiten = new WaitForSeconds(0.3f);

    private float nextFire;
    private void Start()
    {
        shotSpot = GameObject.Find("ShootPoint").transform;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        render = gameObject.GetComponent<LineRenderer>();

        curAmmo = maxAmmo;
        ammoCount.text = curAmmo + " / " + maxAmmo;
    }

    private void Update() // Recoil may require additional functions
    {
        // Firing Mechanism
        if (Input.GetButton("Fire1") && Time.time > nextFire && curAmmo != 0 && !reloading)
        {
            nextFire = Time.time + firerate;
            curAmmo -= 1;
            Vector3 aimed = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)); // This may be usable to manifest accuracy
            RaycastHit hit;

            render.SetPosition(0, shotSpot.position);
            StartCoroutine(flareGone());
            if (Physics.Raycast(aimed, cam.transform.forward, out hit, range))
            {
                render.SetPosition(1, hit.point);
                FoeLife hitLife = hit.collider.GetComponent<FoeLife>();
                if (hitLife != null)
                {
                    hitLife.Damager(1);
                }
            }
            else
            {
                render.SetPosition(1, aimed + (cam.transform.forward * range));
            }
        }
        // Reload activations
        else if (curAmmo == 0)
        {
            StartCoroutine(Reloader());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reloader());
        }
        ammoCount.text = curAmmo + " / " + maxAmmo;
    }

    private IEnumerator Reloader()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        curAmmo = maxAmmo;
        reloading = false;
    }

    private IEnumerator flareGone()
    {
        render.enabled = true;
        yield return awaiten;
        render.enabled = false;
    }
}
