using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1
using System.Threading;

public delegate void MyDelegate(string x);

public class CardViewSystem : MonoBehaviour, IPointerClickHandler
{
    public List<Image> images;
    private int leftImage = 0;
    private int centerImage = 1;
    private int rightImage = 2;
    public float cardWidth = 405;

    private float targetPos = 0;
    private float currentPos = 0;

    private UiAnimation uiAnimation;

    public TouchGesture.GestureSettings GestureSetting;

    public Text debugTextWidget;

    private string dummyVar = "";


    private TouchGesture touch;

    private ImageLoader loader;

    // Use this for initialization
    void Start()
    {

        uiAnimation = GetComponent<UiAnimation>();

        Image img2 = images[0];
        Image img3 = images[1];
        Vector3 v = img3.transform.position - img2.transform.position;
        cardWidth = v.x;


        targetPos = img3.transform.position.x;
        currentPos = targetPos;

        touch = new TouchGesture(this.GestureSetting);
        StartCoroutine(touch.CheckHorizontalSwipes(
            onLeftSwipe: () => { leftAction(); },
            onRightSwipe: () => { rightAction(); },
            onFromLeftEdge: () => { dummyAction(); }
            ));

        loader = new ImageLoader();
        loader.setup(dummy);
//        StartCoroutine(loader.LoadImages(images));
        Thread thread = new Thread(loader.run);
        thread.Start();
    }

    void dummy(string x)
    {
        dummyVar = x;
    }


    void leftAction()
    {

            //moveLeft = cardWidth;
            targetPos += cardWidth;
            Image img = images[leftImage];
            img.transform.position += new Vector3(3*cardWidth,0,0);
            int tmp = leftImage;
            leftImage =  centerImage;
            centerImage = rightImage;
            rightImage = tmp;


    }

    void rightAction()
    {

            //moveRight = cardWidth;
            targetPos -= cardWidth;
            Image img = images[rightImage];
            img.transform.position += new Vector3(-3 * cardWidth, 0, 0);
            int tmp = rightImage;
            rightImage = centerImage;
            centerImage = leftImage;
            leftImage = tmp;

            loader.loadImageIntoFrame("test", 0);


    }

    void dummyAction()
    {
        uiAnimation.showLeftPanel();
    }

    // Update is called once per frame
    void Update()
    {
        print(dummyVar);

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftAction();

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            uiAnimation.showLeftPanel();
            loader.loadImageIntoFrame("test", 1);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            uiAnimation.hideLeftPanel();
            loader.loadImageIntoFrame("test2", 1);

        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightAction();
        }

        if (Mathf.Abs(currentPos - targetPos) > 0.1)
        {
            float delta = 10 * Time.deltaTime * (currentPos - targetPos);
            foreach (Image img in images)
            {
                img.transform.position += new Vector3(1 * delta, 0, 0);

            }
            currentPos = currentPos - delta;

        }
        

    }


    public void OnPointerClick(PointerEventData eventData) // 3
    {
        if (eventData.position.x > 100)
        {
            uiAnimation.hideLeftPanel();
        }
    }

    public void OnDestroy()
    {
        loader.stop();
    }
}
