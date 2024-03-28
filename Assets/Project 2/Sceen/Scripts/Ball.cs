using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    Vector3 startPosition;

    public Rigidbody rb;

    public float startSpeed = 40f;

    private List<GameObject> _pins = new();

    private readonly Dictionary<GameObject, Transform> _pinsDefaultTransform = new();

    private TextMeshProUGUI feedBack;

    public int Point { get; set; }

    private bool _ballMoving;

    private Transform _arrow;

    private Transform _startPosition;
    

    private void Start()
    {

        rb = GetComponent<Rigidbody>();

        Application.targetFrameRate = 60;

        _arrow = GameObject.FindGameObjectWithTag("Arrow").transform;

        _pins = GameObject.FindGameObjectsWithTag("Pin").ToList();

        foreach (var pin in _pins)
        {
            _pinsDefaultTransform.Add(pin, pin.transform);
        }

        feedBack = GameObject.FindGameObjectWithTag("FeedBack").GetComponent<TextMeshProUGUI>();
    }
         


    // Update is called once per frame
    void Update()
    {
      if ((transform.position - startPosition).magnitude > 50f)
      transform.position = startPosition;

      if (_ballMoving)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {

         _ballMoving = true;
         _arrow.gameObject.SetActive(false);
         rb.isKinematic = false;

         Vector3 forceVector = _arrow.forward * (startSpeed * _arrow.transform.localScale.z);

         Vector3 forcePosition = transform.position + (transform.right * 0.5f);

         rb.AddForceAtPosition(forceVector, forcePosition, ForceMode.Impulse);

         yield return new WaitForSecondsRealtime(5);

         _ballMoving = false;

         GenerateFeedBack();

         yield return new WaitForSecondsRealtime(2);

         ResetGame();
    }

    private static void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GenerateFeedBack()
    {
        feedBack.text = Point switch
        {
            0 => "Nothing!",
            > 0 and < 3 => "You are learning Now!",
            >= 3 and < 6 => "It was close!",
            >= 6 and < 10 => "It was nice!",
            _ => "Perfect! You are a master!"
        };
    }
}