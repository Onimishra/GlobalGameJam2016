using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    [SerializeField]
    public Effect EffectObject;

    public Transform AttachBot;
    public Transform AttachTop;
    public bool Attached;
    public SpriteRenderer HatSprite;
    public Player player;

    private string _p = "Player";
    private Collider _collider;

    private Vector3 _previousPosition;
    private float _accumulatedRotation;
    Quaternion _baseRotation;

    void Start() {
        AttachBot = transform.Find("BotAttachment");
        AttachTop = transform.Find("TopAttachment");
        HatSprite = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider>();
        EffectObject.CreateModifierInstance();

        var p = transform.position;
        p.z = p.y;
        transform.position = p;
    }

    void Update() {
       /* if (Attached) {
            float diff = Mathf.Abs((player.gameObject.transform.position - _previousPosition).x);
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            if (diff > 0.001f) {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 0.05f);
            } else {
                transform.rotation = Quaternion.Lerp(transform.rotation, _baseRotation, Time.deltaTime * 0.85f);
            }
            _previousPosition = player.gameObject.transform.position;

            /*
            //Return to base
            if (diff < 0.001f) {
                float returnPower = Mathf.Lerp(0, _accumulatedRotation, Time.deltaTime * 1f);
                _accumulatedRotation -= returnPower;
                print(_accumulatedRotation);
                //transform.RotateAround(AttachBot.position, Vector3.forward, -returnPower);
                transform.Rotate(0f, 0f, -returnPower);

            } else if (_accumulatedRotation + diff < 20f) {
                _accumulatedRotation += diff;
                //float f = Mathf.PingPong(Time.time * 0.5f, 1f);
                //transform.RotateAround(AttachBot.position, Vector3.forward, diff);
                transform.Rotate(0f, 0f, diff);
            }
            
            
        }*/
    }

    void OnTriggerEnter(Collider col) {
        if (Attached) {
            return;
        }
        if (col.tag == _p) {
            print("hi player ");
            player = col.transform.parent.GetComponent<Player>();
            player.AddHat(this);
            _baseRotation = transform.rotation;
            DisablePickup();
            //Destroy(gameObject);
        }
    }


    public void DisablePickup() {
        Attached = true;
        _collider.enabled = false;
    }

    public void EnablePickup() {
        Attached = false;
        _collider.enabled = true;
        var p = transform.position;
        p.z = p.y;
        transform.position = p;
    }

}



