using System;
using UnityEngine;

public class AirBubble : MonoBehaviour
{
    [SerializeField] GameObject airBubble;
    [SerializeField] float airBubbleSpeed;
    private GameObject _airBubble;
    private Rigidbody _rigidbodyAirBubble;

    private void Start()
    {
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
           _airBubble = Instantiate(airBubble, transform.position, airBubble.transform.rotation);
            _rigidbodyAirBubble = _airBubble.GetComponent<Rigidbody>();
        }
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            _airBubble.transform.position = Vector3.Lerp(_airBubble.transform.position, hit.transform.position, airBubbleSpeed * Time.deltaTime);
            _airBubble.transform.position = new Vector3(_airBubble.transform.position.x,0, _airBubble.transform.position.z);
            //_rigidbodyAirBubble.AddForce((hit.point - _airBubble.transform.position).normalized * airBubbleSpeed, ForceMode.VelocityChange);
        }
        Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
    }
}
