using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class SpriteTilerBackground : MonoBehaviour {
    public GameObject BackgroundSpriteGO;
    public Transform FollowTransform;

    private Vector3 _initPosition;
    private Sprite _backgroundSprite;
    private float _spriteWidth;
    private float _spriteHeight;

    private Queue<GameObject> _backgroundSpriteGOs;
    private float _nextSwitchX;
    private int _tileCount = 0;

    private int _lastSprite = 0;

	// Use this for initialization
	void Start () {
        _initPosition = BackgroundSpriteGO.transform.position;
        _backgroundSprite = BackgroundSpriteGO.GetComponent<SpriteRenderer>().sprite;
        _spriteWidth = _backgroundSprite.bounds.size.x;
        _spriteHeight = _backgroundSprite.bounds.size.y;
       

        //Create some copies
        _backgroundSpriteGOs = new Queue<GameObject>();
        _backgroundSpriteGOs.Enqueue(BackgroundSpriteGO);
        _nextSwitchX = BackgroundSpriteGO.transform.position.x + _spriteWidth * 1f;
        for (int i = 1; i < 3; i++) {
            GameObject go = Instantiate(BackgroundSpriteGO, _initPosition + new Vector3(_spriteWidth * i, 0f, 0f), Quaternion.identity) as GameObject;
            _backgroundSpriteGOs.Enqueue(go);
            go.transform.parent = gameObject.transform;
            _tileCount = i;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        //Debug move camera
        FollowTransform.transform.position += new Vector3(Time.deltaTime * 5f, 0f, 0f);

        if (FollowTransform.position.x > _nextSwitchX) {
            MoveTilesForward();
        }
	}

    void MoveTilesForward() {
        _tileCount++;
        GameObject go = _backgroundSpriteGOs.Dequeue();
        go.transform.position = _initPosition + new Vector3(_spriteWidth * _tileCount, 0f, 0f);
        _nextSwitchX = go.transform.position.x - _spriteWidth * 1f;
        _backgroundSpriteGOs.Enqueue(go);
    }
}
