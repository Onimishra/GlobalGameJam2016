using UnityEngine;
using System.Collections;

public struct SpriteInfo {
    public GameObject InstantiatedObject;
    public Bounds SpriteBounds;

    public SpriteInfo(GameObject instantiatedObject) {
        InstantiatedObject = instantiatedObject;
        Sprite spr = InstantiatedObject.GetComponentInChildren<SpriteRenderer>().sprite;
        SpriteBounds = spr.bounds;
    }
}
