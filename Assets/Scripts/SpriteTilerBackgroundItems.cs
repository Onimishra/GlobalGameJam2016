using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class SpriteTilerBackgroundItems : MonoBehaviour {
    public GameObject[] BackgroundSpritePrefabs;
    public int PrefabCopies;
    public Transform FollowTransform;

    public float MinDistance;
    public float MaxDistance;

    public float SpawnAheadDistance;
    public float DespawnBehindDistance;

    private float _lastPositionX;
    private float _nextPositionX;

    private List<SpriteInfo> _itemGameObjects;
    private List<SpriteInfo> _itemGameObjectsReserve;


    // Use this for initialization
    void Start() {
        _itemGameObjects = CreateSpriteInstances();
        _itemGameObjectsReserve = new List<SpriteInfo>();  
    }

    // Update is called once per frame
    void Update() {
        float lastItemX = 0f;
        float firstItemX = 0f;

        if (_itemGameObjects.Count > 0) {
            lastItemX = _itemGameObjects[_itemGameObjects.Count - 1].InstantiatedObject.transform.position.x;
            firstItemX = _itemGameObjects[0].InstantiatedObject.transform.position.x + _itemGameObjects[0].SpriteBounds.max.x;
            
            if (firstItemX + DespawnBehindDistance < FollowTransform.transform.position.x) {
                _itemGameObjectsReserve.Add(_itemGameObjects[0]);
                //_itemGameObjects[0].InstantiatedObject.SetActive(false);
                _itemGameObjects.RemoveAt(0);
            }
        }

        if (_itemGameObjectsReserve.Count > 0) {
            if (_nextPositionX - SpawnAheadDistance < FollowTransform.transform.position.x) {
                //print("moving " + _nextPositionX);
                int randomIndex = Random.Range(0, _itemGameObjectsReserve.Count);
                _itemGameObjects.Add(_itemGameObjectsReserve[randomIndex]);
                float rightX = _nextPositionX + _itemGameObjects[_itemGameObjects.Count - 1].SpriteBounds.size.x;
                _nextPositionX = rightX + Random.Range(MinDistance, MaxDistance);
                _itemGameObjectsReserve[randomIndex].InstantiatedObject.transform.position = new Vector3(rightX - _itemGameObjects[_itemGameObjects.Count - 1].SpriteBounds.extents.x, 0f, 0f);
                //_itemGameObjectsReserve[randomIndex].InstantiatedObject.SetActive(true);

                _itemGameObjectsReserve.RemoveAt(randomIndex);

            }
        }
        
    }
    /*
    void MoveTilesForward() {
        _tileCount++;
        GameObject go = _backgroundSpriteGOs.Dequeue();
        go.transform.position = _initPosition + new Vector3(_spriteWidth * _tileCount, 0f, 0f);
        _nextSwitchX = go.transform.position.x - _spriteWidth * 1f;
        _backgroundSpriteGOs.Enqueue(go);
    }*/

    List<SpriteInfo> CreateSpriteInstances() {
        List<SpriteInfo> itemGameObjects = new List<SpriteInfo>();

        List<int> itemsToSpawn = new List<int>();
        for (int i = 0; i < BackgroundSpritePrefabs.Length; i++) {
            for (int j = 0; j < PrefabCopies; j++) {
                itemsToSpawn.Add(i);
            }
		}
        _nextPositionX = Random.Range(MinDistance, MaxDistance);

        while (itemsToSpawn.Count > 0) {
            int nextIndex = Random.Range(0, itemsToSpawn.Count);
            int nextItem = itemsToSpawn[nextIndex] % (BackgroundSpritePrefabs.Length);
            GameObject go = Instantiate(BackgroundSpritePrefabs[nextItem]) as GameObject;
            go.transform.parent = gameObject.transform;
            SpriteInfo si = new SpriteInfo(go);
            go.transform.position = new Vector3(_nextPositionX + si.SpriteBounds.max.x, 0f, 0f);
            float rightX = go.transform.position.x + si.SpriteBounds.max.x;
            itemGameObjects.Add(si);
            _nextPositionX = rightX + Random.Range(MinDistance, MaxDistance);
            itemsToSpawn.RemoveAt(nextIndex);
        }
        return itemGameObjects;
    }
}
