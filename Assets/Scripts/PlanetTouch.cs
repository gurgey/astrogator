using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetTouch : MonoBehaviour 
{
    public RectTransform arrows;
    public PlayerController player;
    public bool friendly = false;
    RectTransform myArrow;

    void Start()
    {
        //arrows.transform.chi
       // RectTransform tr = arrows.GetComponent<RectTransform>();
        //tr.a
        GameObject arrow = new GameObject();//CreateArrow();
        myArrow = arrow.AddComponent<RectTransform>();
        myArrow.parent = arrows;
        //myArrow.addcompon
        Text txt = myArrow.gameObject.AddComponent<Text>();
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        myArrow.gameObject.layer = LayerMask.NameToLayer("UI");
        myArrow.localScale = new Vector3(1, 1, 1);
        myArrow.gameObject.name = name + " Arrow";
        //arrows.transform.parent = arrows.
    }

    void OnTriggerEnter(Collider other)
    { 
        if (other.tag == "Player")
        {
            if (!friendly)
                other.GetComponent<PlayerController>().GameOver();
            else
                other.GetComponent<PlayerController>().GameWin();
        }
    }

    void OnBecameVisible()
    {
        myArrow.gameObject.SetActive(false);
    }
    void OnBecameInvisible()
    {
        if (myArrow)
            myArrow.gameObject.SetActive(true);
    }
    
    void Update()
    {
        if (myArrow.gameObject.activeSelf)
        {
            Vector3 vec = transform.position - player.transform.position;
            myArrow.GetComponent<Text>().text = vec.magnitude.ToString();
            float frameRatio = arrows.rect.width/arrows.rect.height;
            Vector3 lVec = arrows.worldToLocalMatrix * vec;
            if (lVec.x*lVec.x > lVec.y*lVec.y*frameRatio*frameRatio)
            {
                float ratio = lVec.y/lVec.x; //div by 0?
                if (lVec.x > 0)
                    lVec.x = arrows.rect.width ;
                else
                    lVec.x = -arrows.rect.width ;
                lVec.y = ratio * lVec.x;
            }
            else
            {
                float ratio = lVec.x / lVec.y; //div by 0?
                if (lVec.y > 0)
                    lVec.y = arrows.rect.height ;
                else
                    lVec.y = -arrows.rect.height ;
                lVec.x = ratio * lVec.y;
            }
         
            myArrow.localPosition = lVec;
        }
    }

    GameObject CreateArrow()
    {
        GameObject ret = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        return ret;
    }
}
