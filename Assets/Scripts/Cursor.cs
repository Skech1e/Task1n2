using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Cursor : MonoBehaviour
{
    Rigidbody2D rb;
    TrailRenderer tr;
    Vector2 defaultPos;
    CircleCollider2D circleCollider;

    public List<GameObject> circles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        tr.enabled = false;
        circles = new();
        defaultPos = rb.position;
    }
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += FingerDown;
        Touch.onFingerMove += FingerMove;
        Touch.onFingerUp += FingerUp;
    }
    private void Start()
    {
        GameManager.Instance.restart.onClick.AddListener(() => Restart());
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        Touch.onFingerDown -= FingerDown;
        Touch.onFingerMove -= FingerMove;
        Touch.onFingerUp -= FingerUp;
        GameManager.Instance.restart.onClick.RemoveAllListeners();
    }

    private void FingerDown(Finger finger)
    {
        Vector2 touchPos = finger.screenPosition;
        if (InTouchZone(touchPos))
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(touchPos);
            rb.position = cursorPos;
            circleCollider.enabled = true;
        }
    }
    private void FingerMove(Finger finger)
    {
        Vector2 touchPos = finger.screenPosition;
        if (InTouchZone(touchPos))
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(touchPos);
            rb.position = cursorPos;
            tr.enabled = true;
            tr.time = 100;
        }

    }

    bool InTouchZone(Vector2 touchPos) => RectTransformUtility.RectangleContainsScreenPoint(GameManager.Instance.Layout, touchPos, Camera.main);

    private void FingerUp(Finger finger)
    {
        tr.time = -1;
        //tr.Clear();
        //tr.enabled = false;
        circleCollider.enabled = false;
        foreach (GameObject circle in circles)
            circle.SetActive(false);
    }

    void Restart() => circles = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        circles.Add(collision.gameObject);
    }
}
