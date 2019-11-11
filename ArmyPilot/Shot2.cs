using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot2 : MonoBehaviour
{

    /// <summary>
    /// ////////////////////////////VARIABLES////////////////////////////
    /// </summary>

    private Ray ray;// variable raycast: rajaton lineaarinen komponentti
    private RaycastHit hit; // variable raycast hit
    private float NextFire = 0f;// the firing time
    private Text TxtAmmo;// text
    private Text TxtLoader;// text
    private bool CanShoot; // voit ampua
    private Text TxtScore;
    private Text TxtInfos;
    private Image ImView;
    private int Score = 0;

    public GameObject BulletHolePrefab;// impact prefab
    public AudioClip SoundShoot;// sound
    public float FireRate = 0.1f;// interval of fire
    public AudioClip SoundEmpty;// sound
    public int Loader = 3;
    public int Ammo;
    public int MaxAmmo = 20;// ammo max of loader
    public GameObject Sparks;// spark
    public AudioClip SoundExplosion;// sound
    public GameObject ZoneExplosion;
    public int DistanceShoot = 80;


    void Start()
    {
        Ammo = MaxAmmo;//Nb the ammo max
        TxtAmmo = GameObject.Find("TxtAmmo").GetComponent<Text>();
        TxtLoader = GameObject.Find("TxtLoader").GetComponent<Text>();
        TxtScore = GameObject.Find("TxtScore").GetComponent<Text>();
        ImView = GameObject.Find("ImView").GetComponent<Image>();
        TxtInfos = GameObject.Find("TxtInfos").GetComponent<Text>();

        //ifos mission
        TxtInfos.enabled = true;
        TxtInfos.text = "Mission : Eliminate all enemies...";
        StartCoroutine("Pause");
    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(3f);
        TxtInfos.enabled = false;
    }
    IEnumerator PauseSniperDeath()
    {
        yield return new WaitForSeconds(1f);
        TxtInfos.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 ScreenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2); //Center of screen
        ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);//Ray definition

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))//Raycast
        {
            if (hit.distance <= DistanceShoot && hit.transform.gameObject.tag == "Enemy" || hit.distance <= DistanceShoot && hit.transform.gameObject.tag == "OilDrum" || hit.distance <= DistanceShoot && hit.transform.gameObject.tag == "Swat")
            {
                ImView.color = Color.green;
                CanShoot = true;
            }
            else
            {
                ImView.color = Color.red;
                CanShoot = false;
            }
        }
        //Recovery of components
        TxtAmmo.text = "Ammo:" + Ammo + "/" + MaxAmmo;
        TxtLoader.text = "Loader:" + Loader;

        //Distance checking

        if (Input.GetButton("Fire1") && Time.time > NextFire && !Input.GetKey(KeyCode.LeftAlt) && Ammo > 0)
        {
            Ammo -= 1;// removal of ammo
            NextFire = Time.time + FireRate;// Update of the firing interval
            GetComponent<AudioSource>().PlayOneShot(SoundShoot);// Plays the sound of shooting

            //Vector2 ScreenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2); //Center of screen
            ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);//Ray definition

            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))//Raycast
            {
                //touch tag Swat
                if (hit.transform.gameObject.tag == "Swat" && CanShoot)
                {
                    Score += 100;
                    TxtScore.text = "Score : " + Score;
                    GameObject.Find(hit.transform.name).GetComponent<AiSwat>().SwatDead();// Sniper Dead

                    // checking BScore (Best Score)
                    if (Score > PlayerPrefs.GetInt("BstScore"))
                    {
                        PlayerPrefs.SetInt("BstScore", Score);
                    }
                    TxtInfos.enabled = true;
                    TxtInfos.text = "+100Pts";
                    StartCoroutine("PauseSniperDeath");
                }

                //touch tag enemy
                if (hit.transform.gameObject.tag == "Enemy" && CanShoot)
                {
                    Score += 100;
                    TxtScore.text = "Score : " + Score;
                    GameObject.Find(hit.transform.name).GetComponent<AiSniper>().SniperDead();// Sniper Dead

                    // checking BScore (Best Score)
                    if (Score > PlayerPrefs.GetInt("BstScore"))
                    {
                        PlayerPrefs.SetInt("BstScore", Score);
                    }
                    TxtInfos.enabled = true;
                    TxtInfos.text = "+100Pts";
                    StartCoroutine("PauseSniperDeath");
                }
                //touch tag wall
                if (hit.transform.gameObject.tag == "Wall")
                {
                    GameObject Go = Instantiate(BulletHolePrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                    Destroy(Go, 60f);

                }
                //touch tag OilDrum 
                if (hit.transform.gameObject.tag == "OilDrum" && CanShoot)
                {
                    Debug.Log("Oil");
                    GetComponent<AudioSource>().PlayOneShot(SoundExplosion);
                    GameObject Ze = Instantiate(ZoneExplosion, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                    Destroy(Ze, 2f);
                    Destroy(hit.transform.gameObject);// OilDrum disappear
                }
                //touch tag not a enemy
                if (hit.collider && hit.transform.gameObject.tag != "Enemy")   // sparks not show in enemy
                {
                    GameObject Obj = Instantiate(Sparks, hit.point, Quaternion.identity) as GameObject;
                    Obj.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                    Destroy(Obj, 0.1f);
                }


            }
        }
        //Reload if empty
        if (Input.GetKeyDown(KeyCode.R) && Ammo == 0 && Loader > 0)
        {
            Loader -= 1;
            StartCoroutine("Recharge");
            //Ammo = MaxAmmo;
        }
        if (Ammo == 0 & Input.GetButton("Fire1") && Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            GetComponent<AudioSource>().PlayOneShot(SoundEmpty);
        }
    }
    //update of ammo after pause
    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(0.2f);
        Ammo = MaxAmmo;
    }
}
