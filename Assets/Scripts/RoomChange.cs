using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomChange : MonoBehaviour
{
    [SerializeField]public Vector2 cameraChange;
    [SerializeField]public Vector3 playerChange;
    private Vector2 playerPos;
    private CameraSmoothing cam;
    [SerializeField] public bool needText;
    [SerializeField] public GameObject text;
    [SerializeField] public Text placeText;
    [SerializeField] public string placeName;
    private static bool crBusy;
    public static bool crStop;
    public IEnumerator fadeIn;
    //public IEnumerator fadeOutExit;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraSmoothing>();
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

   /* private void FadeIn()
    {
        StartCoroutine(PlaceNameCo(0, 1, 3f));
    }

    private void FadeOut()
    {
        StartCoroutine(PlaceNameCo(1, 0, 3f));
    }
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            cam.minPos += cameraChange;
            cam.maxPos += cameraChange;
            
            //playerPos = other.transform.position;
            other.transform.position += playerChange;
            fadeIn = FadeIn();
           // fadeOutExit = FadeOutExit();

            if (needText)
            {
                crStop = false;
                // StopCoroutine(fadeOutExit);
                StartCoroutine(fadeIn);
                //StartCoroutine(FadeOut());
                //FadeOut();
            }


            
            //crStop = true;
        }
    }

    /*private IEnumerator PlaceNameCo(float startAlpha, float endAlpha, float lerpTime = 0.5f)
    {
        //bool fadeIn = false;
       // crBusy = true;
        crStop = false;
        text.SetActive(true);
        placeText.text = placeName;
        float timeStartedLerp = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerp;
        float percentageComplete = timeSinceStarted / lerpTime;
        Color tmpColor = text.GetComponent<Text>().color;
        while (true)
        {
         
            timeSinceStarted = Time.time - timeStartedLerp;
            percentageComplete = timeSinceStarted / lerpTime;

           // float currentValue = Mathf.Lerp(startAlpha, endAlpha, percentageComplete);
          
            text.GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAlpha, endAlpha, percentageComplete));


            //text.SetActive(true);
            
            //yield return new WaitForSeconds(4f);
           /* if (percentageComplete >= 1 && !fadeIn)
            {
                yield return new WaitForSeconds(2f);
                fadeIn = true;
                timeStartedLerp = Time.time;
                timeSinceStarted = Time.time - timeStartedLerp;
                percentageComplete = timeSinceStarted / lerpTime;
                startAlpha = 1;
                endAlpha = 0;

            }
            if (percentageComplete >= 1)// && fadeIn)
            {
               
                text.GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 1);
                text.SetActive(false);
               // crBusy = false;
                break;
                
            }

            yield return new WaitForEndOfFrame();
            //text.SetActive(false);
        }
        crBusy = false;
    }
    */
    public IEnumerator FadeIn(float t = 0.5f)
    {
        crBusy = true;
        text.SetActive(true);
        placeText.color = new Color(placeText.color.r, placeText.color.g, placeText.color.b, 0);
        while (placeText.color.a < 1.0f)
        {
            placeText.color = new Color(placeText.color.r, placeText.color.g, placeText.color.b, placeText.color.a + (Time.deltaTime / t));
            yield return null;
        }
        
       
     // if(!crStop)
            yield return new WaitForSeconds(1f);
            crBusy = false;
            StartCoroutine(FadeOut());
      //  }
    }
    public IEnumerator FadeOut(float t = 0.5f)
    {
        placeText.color = new Color(placeText.color.r, placeText.color.g, placeText.color.b, placeText.color.a);
        while (placeText.color.a > 0.0f)
        {
            placeText.color = new Color(placeText.color.r, placeText.color.g, placeText.color.b, placeText.color.a - (Time.deltaTime / t));
            yield return null;
        }
        text.SetActive(false);
    }
    /*public IEnumerator FadeOutExit(float t = 0.5f)
    {
        placeText.color = new Color(placeText.color.r, placeText.color.g, placeText.color.b, placeText.color.a-(Time.deltaTime/t));
        while (placeText.color.a > 0.0f)
        {
            placeText.color = new Color(placeText.color.r, placeText.color.g, placeText.color.b, placeText.color.a - (Time.deltaTime / t));
            yield return null;
        }
        text.SetActive(false);
    }*/
}
