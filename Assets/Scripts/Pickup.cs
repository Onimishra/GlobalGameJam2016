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
    }

    void OnTriggerEnter(Collider col) {
        if (Attached) {
            return;
        }
        if (col.tag == _p) {
            print("hi player ");

            col.transform.parent.GetComponent<Player>().AddHat(this);
            GetComponent<Controllable>().enabled = false;
            Attached = true;
            _collider.enabled = false;
            //Destroy(gameObject);
        }
        
    }


}



