using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    
    public GameObject playerPrefab;
    private CarController carController;
    public float speed = 10f;
    float currentTime = 0f;
    
    public Slider gasSlider;
    public TextMeshProUGUI timeText;
    public GameObject background;
    public GameObject win;
    public GameObject lose;
    public GameObject button;

    public bool isLive = true;
    
    void Start()
    {
       GameObject player = Instantiate(playerPrefab) as GameObject;
       player.transform.position = new Vector3(0, 0, -8);
       carController = player.GetComponent<CarController>();

       gasSlider.maxValue = carController.gasMaxGauge;
       gasSlider.value = carController.gasMaxGauge;
    }

    void Update()
    {
        if (isLive)
        {
            gasSlider.value = carController.gasGauge;
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 viewportPos = Camera.main.ScreenToViewportPoint(mousePos);

                if (viewportPos.x >= 0.5f)
                {
                    carController.clicked = 1;
                }
                else if (viewportPos.x < 0.5f)
                {
                    carController.clicked = -1;
                }
            }
            else
            {
                carController.clicked = 0;
            }
        }
    }

    void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        timeText.text = currentTime.ToString("0.00");
        if (isLive)
        {
            
            if (currentTime >= 180)
            {
                background.SetActive(true);
                win.SetActive(true);
                button.SetActive(true);
                isLive = false;
            }

            if (carController.gasGauge < 0)
            {
                background.SetActive(true);
                lose.SetActive(true);
                button.SetActive(true);
                isLive = false;
            }
        }
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
