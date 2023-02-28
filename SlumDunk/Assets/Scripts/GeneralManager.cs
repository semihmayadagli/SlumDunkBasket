using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GeneralManager : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject Basket;
    [SerializeField] private Image[] Duties;
    [SerializeField] private GameObject[] spawnItemLocations;
    [SerializeField] private AudioSource[] Sounds;
    public ParticleSystem[] Effects;
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private Sprite Success;
    [SerializeField] private GameObject SpawnerObject;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] int numberofDuty;
    public GameObject Basket_ScaleUp;
    int numberofBasket;
    float fingerPositionDifferece;
    

    private void Start()
    {
        LevelText.text = SceneManager.GetActiveScene().name;
        for (int i = 0; i < numberofDuty; i++)
        {
            Duties[i].gameObject.SetActive(true);
        }        
        InvokeRepeating("ObjectSpawner", 5f, 15f);
    }


    void Update()
    {
        PlatformMoving();
    }

    private void PlatformMoving() 
    {
        if (Time.timeScale!=0)
        {
            if (Input.touchCount>0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 5));
                
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        fingerPositionDifferece = touchPosition.x - platform.transform.position.x; 
                        break;
                    case TouchPhase.Moved:
                        if (touchPosition.x-fingerPositionDifferece < 1.25f && touchPosition.x - fingerPositionDifferece > -1.25f)
                        {
                            platform.transform.position = Vector3.Lerp(platform.transform.position, new Vector3(touchPosition.x-fingerPositionDifferece,platform.transform.position.y, platform.transform.position.z), 1f);
                        }
                        break;                   
                }
            }
        }        
    }
    public void BasketCondition(Vector3 position) 
    {
        numberofBasket++;
        Duties[numberofBasket - 1].sprite = Success;
        Sounds[4].Play();
        Effects[0].transform.position = position;
        Effects[0].gameObject.SetActive(true);
        GameWin();
    }
    public void GameLost() 
    {        
        Sounds[1].Play();
        Panels[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void GameWin() 
    {
        if (numberofBasket == numberofDuty)
        {
            if (PlayerPrefs.GetInt("Level")+1 > SceneManager.sceneCountInBuildSettings - 1)
            {
                Panels[3].SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Sounds[2].Play();
                Panels[1].SetActive(true);
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                Time.timeScale = 0;
            }                   
        }
    }

    public void ObjectSpawner()
    {
        int RandomLocation = Random.Range(0, spawnItemLocations.Length - 1);
        SpawnerObject.transform.position = spawnItemLocations[RandomLocation].transform.position;
        SpawnerObject.SetActive(true);
        Basket_ScaleUp.GetComponent<Items>().Spawner();        
    }
    public void BasketScaleUp() 
    {
        StartCoroutine(BasketScaleUpTiming());
        Sounds[0].Play();
    }

    IEnumerator BasketScaleUpTiming() 
    {
        Basket.transform.localScale = new Vector3(65f, 65f, 65f);
        yield return new WaitForSeconds(5f);
        Basket.transform.localScale = new Vector3(50f, 50f, 50f);
    }

    public void ButtonPress(string buttonValue) 
    {
        switch (buttonValue)
        {
            case "Pause":
                Panels[0].SetActive(true);
                Time.timeScale = 0;
                break;
            case "Resume":
                Panels[0].SetActive(false);
                Time.timeScale = 1;
                break;
            case "TryAgain":
                Panels[0].SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "Quit":
                Application.Quit();
                break;
            case "Next":
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"), LoadSceneMode.Single);
                Time.timeScale = 1;
                break;
            case "PlayAgain":
                PlayerPrefs.SetInt("Level", 1);
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"),LoadSceneMode.Single);
                Time.timeScale = 1;
                break;
        }

    }   
}
