using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pin : MonoBehaviour
{

    public bool isFallen;

    public float pinFallAccuracy = 30f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private Rigidbody pinRb;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        pinRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if pin has fallen
        if (gameObject.activeSelf)
        {
            isFallen = Quaternion.Angle(startRotation, transform.localRotation) > pinFallAccuracy;
        }
        
       
    }

    public void ResetPin()
    {
        pinRb.velocity = Vector3.zero;
        pinRb.isKinematic = true;
        transform.position = startPosition;
        transform.rotation = startRotation;
        gameObject.SetActive(true);
        isFallen = false;
        pinRb.isKinematic=false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pit"))
        {
            isFallen = true;
        }
    }
}
