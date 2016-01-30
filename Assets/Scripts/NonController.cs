using UnityEngine;
using System.Collections;

public class NonController : Controller {
    public override Vector2 Movement() {
        return Vector2.zero;
    }

    public override bool Jump() {
        return false;
    }

    public override bool Attack() {
        return false;
    }

    public override void Update() {
    
    }
}
