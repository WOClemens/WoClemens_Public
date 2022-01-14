using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookWeapon : MonoBehaviour, GunInterface
{   
    [Header("Shooting")]
    public int range;
    public int chargeTime;
    public float timer = 0;
    public int damag;

    int count;

    public GameObject missil;
    public Transform spawnPoint;
    public GameObject[] enemysLooked = new GameObject[3];

    [Header("LineRenderer + Gradient")]
    private LineRenderer lr;
    public GameObject laserend;
    public GameObject laserstart;
    public Gradient normalGradient;
    public Gradient aimGradient;

    [Header("UI")]
    public GameObject panel_ui;
    public Image[] image_lockedPoints;
    public Image chargeBar;

    private IEnumerator coroutine;
    bool isStarted;

    void Start()
    {
        chargeBar = GameObject.Find("charge").GetComponent<Image>();
        isStarted = false;
        lr = GetComponent<LineRenderer>();
        count = 0;
        lr.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        chargeBar.fillAmount = timer/2;

        if (lr.enabled)
        {
            lr.SetPosition(0, laserstart.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 15))
            {
                lr.colorGradient = aimGradient;
                if (hit.collider)
                {
                    lr.SetPosition(1, laserend.transform.position);
                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        timer += Time.deltaTime;
                        if (timer >= chargeTime && count < 3)
                        {
                            //Debug.Log("Enemy Loocked");
                            enemysLooked[count] = hit.transform.gameObject;
                            ChangeLockPoint(count, Color.red);
                            count++;
                            timer = 0;
                        }
                        else if (count > 2)
                        {
                            if (!isStarted)
                            {
                                isStarted = true;
                                coroutine = Shoot();
                                count = 0;
                                StartCoroutine(coroutine);
                            }
                        }
                    }
                    else
                    {
                        timer = 0;
                    }
                }
                else
                {
                    timer = 0;
                }
            }
            else
            {
                lr.colorGradient = normalGradient;
                lr.SetPosition(1, laserend.transform.position);
                timer = 0;
            }
        }    
    }

    void ChangeLockPoint(int count, Color color)
    {
        image_lockedPoints[count].color = color;
    }
    IEnumerator Shoot()
    {
        Debug.Log("Shoot");
        for (int i = 0; i < enemysLooked.Length; i++)
        {
            yield return new WaitForSeconds(.2f);
            if (enemysLooked[i] != null)
            {
                ChangeLockPoint(i, Color.white);
                //Debug.Log("Shoot on " + enemysLooked[i]);
                GameObject go = Instantiate(missil, spawnPoint.position, Quaternion.identity);
                go.GetComponent<MissileProjectil>().target = enemysLooked[i].transform;
                enemysLooked[i] = null;
            }
        }
        isStarted = false;
    }

    public void NotifyShowLine()
    {
        lr.enabled = true;
    }

    public void NotifyHideLine()
    {
        lr.enabled = false;
    }
    public int GetDamag()
    {
        return damag;
    }
    public string GetGun()
    {
        return "Look";
    }

    public bool GetAim()
    {
        return false;
    }
}
