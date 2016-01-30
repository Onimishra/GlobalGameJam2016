using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    [SerializeField]
    public Effect EffectObject;

    public Transform AttachBot;
    public Transform AttachTop;
    public bool Attached;
    public SpriteRenderer HatSprite;

    private string _p = "Player";
    private Collider _collider;


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

    void OnTriggerEnter(Collider col) {
        if (Attached) {
            return;
        }
        if (col.tag == _p) {
            print("hi player ");

            col.transform.parent.GetComponent<Player>().AddHat(this);
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



