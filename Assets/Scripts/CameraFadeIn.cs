using UnityEngine;
using System.Collections;

public class CameraFadeIn : MonoBehaviour {
    public Transform FaceTarget;

    private Vector3 InitPosition;
    public float InitSize = 0.01f;
    private Vector3 _offsetTarget;

    public Vector3 EndLocalPosition = new Vector3(0f, 0f, -10f);
    public float EndSize = 5f;

    private Vector3 _currentTargetPosition;
    Camera cam;

    [SerializeField]
    AnimationCurve curve;
    float timer;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        transform.localPosition = new Vector3(-5.325f, 0.076f, -10f);
        cam.orthographicSize = 0.01f;
        InitPosition = transform.position;
        
        InitSize = cam.orthographicSize;
        _offsetTarget = FaceTarget.transform.position - InitPosition;
        timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - timer < 5f) {
            _currentTargetPosition = FaceTarget.transform.position - _offsetTarget;
            transform.position = Vector3.Lerp(transform.position, _currentTargetPosition, Time.deltaTime * 8f);
            cam.orthographicSize = Mathf.Lerp(InitSize, EndSize, curve.Evaluate(0.2f * (Time.time - timer)));
        } else {
            transform.localPosition = Vector3.Lerp(transform.localPosition, EndLocalPosition, Time.deltaTime * 8f);
        }
        
	}
}
