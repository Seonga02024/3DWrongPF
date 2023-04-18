using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public GameObject answerObj;
    public GameObject wrongObj;
    private Vector2 touchBeganPos;
    private Vector2 touchEndedPos;
    private Vector2 touchDif;
    private float swipeSensitivity = 15; // ��ġ �ΰ���
    private float checkTime = 1; // ���� ��ġ�� ������ �ɸ��� �ð�
    private float currentTime = 0;
    private float RoSpeedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        RoSpeedTime = 60 / checkTime;
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        currentTime += Time.deltaTime;
        if (Input.GetMouseButton(0)) // Ŭ���� ���
        {

        }
    }

    IEnumerator Turn(float duration, Vector3 vector)
    {
        var runTime = 0.0f;

        while (runTime < duration)
        {
            runTime += Time.deltaTime;
            answerObj.transform.Rotate(vector * Time.deltaTime * RoSpeedTime, Space.World);
            wrongObj.transform.Rotate(vector * Time.deltaTime * RoSpeedTime, Space.World);
            yield return null;
        }
    }

    IEnumerator Wait(float duration, Vector3 vector)
    {
        yield return StartCoroutine(Turn(duration, vector));
    }

    //���������� ��ġ
    public void Swipe()
    {
        if (Input.touchCount > 0 && currentTime > checkTime)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchBeganPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchEndedPos = touch.position;
                touchDif = (touchEndedPos - touchBeganPos);

                //��������. ��ġ�� x�̵��Ÿ��� y�̵��Ÿ��� �ΰ������� ũ��
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
                        StartCoroutine(Wait(1f, new Vector3(0, -1, 0)));
                    }
                    else if (touchDif.x < 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("Left");
                        StartCoroutine(Wait(1f, new Vector3(0, 1, 0)));
                    }
                    currentTime = 0;
                }
                //��ġ
                else
                {
                    Debug.Log("touch");

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log(hit.transform.name);
                        if (hit.transform.tag == "answer")
                        {
                            Destroy(hit.transform.gameObject);
                        }
                    }

                    currentTime = 0;
                }
            }
        }
    }
    
}

