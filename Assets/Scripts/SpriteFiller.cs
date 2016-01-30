using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteFiller : MonoBehaviour {
    public List<GameObject> SpritePrefabs;
    public Transform[] AttachmentPoints;

	// Use this for initialization
	void Start () {
        if (SpritePrefabs.Count < AttachmentPoints.Length) {
            int count = SpritePrefabs.Count;
            int i = 0;
            while (SpritePrefabs.Count < AttachmentPoints.Length) {
                SpritePrefabs.Add(SpritePrefabs[(i) % count]);
                i++;
            }
        }
        List<int> itemsToSpawn = new List<int>();
        for (int i = 0; i < AttachmentPoints.Length; i++) {
            itemsToSpawn.Add(i);
        }
        //print("spawning " + itemsToSpawn.Count + " items");
        for (int i = 0; itemsToSpawn.Count > 0; i++) {
            //print("spawning " + i);
            int nextIndex = Random.Range(0, itemsToSpawn.Count);
            //print("nextIndex " + nextIndex);
            int spriteIndex = itemsToSpawn[nextIndex];
            //print("spriteIndex " + spriteIndex);
            GameObject go = Instantiate(SpritePrefabs[spriteIndex]) as GameObject;
            itemsToSpawn.RemoveAt(nextIndex);
            //print("itemsToSpawn.Count " + itemsToSpawn.Count);
            go.transform.parent = AttachmentPoints[i].transform;
            go.transform.localPosition = Vector3.zero;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
