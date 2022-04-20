using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWorks : MonoBehaviour
{
    [SerializeField] private int dmg;
    [SerializeField] private float firerate;
    [SerializeField] private float range;

    private Transform shotSpot;
    private Camera cam;
    private LineRenderer render;

    private WaitForSeconds awaiten = new WaitForSeconds(0.3f);

    private float nextFire;
    private void Start()
    {
        shotSpot = GameObject.Find("ShootPoint").transform;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        render = gameObject.GetComponent<LineRenderer>();
    }

    private void Update() // Recoil may require additional functions
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + firerate;
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
    }

    private IEnumerator flareGone()
    {
        render.enabled = true;
        yield return awaiten;
        render.enabled = false;
    }
}
