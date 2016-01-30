using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HatHolder : MonoBehaviour {
    public List<Pickup> Hats;
    public Transform HeadTarget;

    void Start() {
        HeadTarget = GameObject.Find("HatAttachmentPoint").transform;
    }

    void Update() {
        transform.position = HeadTarget.position;
    }

    public Transform GetParentForHat() {
        if (Hats.Count == 0) {
            return transform;
        } else {
            return Hats[Hats.Count - 1].AttachTop.transform;
        }
    }
}
