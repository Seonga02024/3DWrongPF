using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public float rotateSpeed = 0.1f;
    public GameObject answerObj;
    public GameObject wrongObj;
    public GameObject cubes;
    private Vector2 touchBeganPos;
    private Vector2 touchEndedPos;
    private Vector2 touchDif;
    private float swipeSensitivity = 15;
    private float checkTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe1();
        checkTime += Time.deltaTime;
        if (Input.GetMouseButton(0)) // 클릭한 경우
        {

        }
    }

    IEnumerator Turn(float duration, Vector3 vector)
    {
        var runTime = 0.0f;

        while (runTime < duration)
        {
            runTime += Time.deltaTime;
            answerObj.transform.Rotate(vector * Time.deltaTime * 60, Space.World);
            wrongObj.transform.Rotate(vector * Time.deltaTime * 60, Space.World); // runTime / duration

            yield return null;
        }
    }

    IEnumerator Wait(float duration, Vector3 vector)
    {
        yield return StartCoroutine(Turn(duration, vector));
    }

    //스와이프와 터치
    public void Swipe1()
    {
        if (Input.touchCount > 0 && checkTime > 1f)
        {
            //Debug.Log("touch");
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                touchBeganPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchEndedPos = touch.position;
                touchDif = (touchEndedPos - touchBeganPos);

                //스와이프. 터치의 x이동거리나 y이동거리가 민감도보다 크면
                if (Mathf.Abs(touchDif.y) > swipeSensitivity || Mathf.Abs(touchDif.x) > swipeSensitivity)
                {
                    if (touchDif.y > 0 && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("up");
                        StartCoroutine(Wait(1f, new Vector3(1, 0, 0)));
                    }
                    else if (touchDif.y < 0 && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("down");
                        StartCoroutine(Wait(1f, new Vector3(-1, 0, 0)));
                    }
                    else if (touchDif.x > 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("right");
                        StartCoroutine(Wait(1f, new Vector3(0,-1,0)));
                    }
                    else if (touchDif.x < 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("Left");
                        StartCoroutine(Wait(1f, new Vector3(0, 1, 0)));
                    }
                    checkTime = 0;
                }
                //터치.
                else
                {
                    Debug.Log("touch");
                    checkTime = 0;
                }
            }
        }
    }
    
}

