using UnityEngine;

public class ExampleClass : MonoBehaviour {
    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    [SerializeField] PlayerMovementMIO moveLogic;
    [SerializeField] GameObject player;
    void Start() {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }
    void Update() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        if (transform.localPosition == endMarker.localPosition)
        {
            moveLogic.enabled = true;
            player.transform.SetParent(null);
        }
    }
}