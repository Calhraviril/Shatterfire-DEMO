using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BulletWorks : MonoBehaviour
{
    [Header("Stats")]
    public int dmg;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float firerate;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private float range;
    [SerializeField] private float recoil;
    [SerializeField] private float accuracy;
    [SerializeField] private float multishot;

    [Header("Misc")]
    [SerializeField] private TMP_Text ammoCount;
    [SerializeField] private TMP_Text gunName;
    [SerializeField] private GameObject bullet;

    private Transform shotSpot; // Point where shots come from
    private Camera cam; // The main camera
    private LineRenderer render; // The renderer of hitscan bullets
    private Inventory invent; // The system used to store items and tools

    // Misc stuff
    private int curAmmo; // Current bullets of the magazine
    private bool reloading; // Activated during a reload
    public bool activated; // If a weapon is in use
    private int toolCount; // The amount of weapons held
    private int activeSpot; // Which weapon is active
    private WaitForSeconds awaiten = new WaitForSeconds(/*0.3f*/99);

    private float nextFire;
    private Vector3 shotSpotter()
    {
        Vector3 aruk = cam.transform.forward;
        float arkuX = Random.Range(aruk.x + accuracy, aruk.x - accuracy);
        float arkuY = Random.Range(aruk.y + accuracy, aruk.y - accuracy);
        float arkuZ = Random.Range(aruk.z + accuracy, aruk.z - accuracy);
        Vector3 shotMaster = new Vector3(arkuX, arkuY, arkuZ);
        otherSpotter = shotMaster;
        return shotMaster;
    }
    private Vector3 otherSpotter;
    private void Awake()
    {
        invent = GameObject.Find("Player").GetComponent<Inventory>();
    }
    private void Start()
    {
        shotSpot = GameObject.Find("ShootPoint").transform;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        render = gameObject.GetComponent<LineRenderer>();
        activated = false;        
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
            for (int i = 0; i < multishot; i++) // Activates by the amount of Multishot
            {
                Vector3 aimed = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                RaycastHit hit;
                print("Shot");
                if (Physics.Raycast(aimed, shotSpotter(), out hit, range))
                {
                    Instantiate(bullet, shotSpot.position, Quaternion.LookRotation(otherSpotter));
                    print(hit.point);
                    FoeLife hitLife = hit.collider.GetComponent<FoeLife>();
                    if (hitLife != null)
                    {
                        hitLife.Damager(1);
                    }
                }
                else
                {
                    Instantiate(bullet, shotSpot.position, Quaternion.LookRotation(shotSpotter()));
                    render.SetPosition(1, aimed + (cam.transform.forward * range));
                }
            }

        }
        // Reload activations
        else if (curAmmo == 0 && !reloading)
        {
            StartCoroutine(Reloader());
        }
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            StartCoroutine(Reloader());
        }
        if (Input.GetKeyDown(KeyCode.F) && !reloading)
        {
            activeSpot += 1;
            print(activeSpot);
            if (activeSpot > toolCount)
            {
                activeSpot = 1;
            }
            ChangeTool(invent.toolInver[activeSpot - 1]);
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


    public void ChangeTool(Tool equipped)
    {
        dmg = equipped.dmg;
        maxAmmo = equipped.maxAmmo;
        firerate = equipped.firerate;
        reloadSpeed = equipped.reloadSpeed;
        range = equipped.range;
        recoil = equipped.recoil;
        accuracy = equipped.accuracy;
        multishot = equipped.multishot;
        toolCount = invent.toolInver.Count;
        gunName.text = equipped.namer;
    }
}
