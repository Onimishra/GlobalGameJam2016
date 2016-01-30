using UnityEngine;
using System.Collections;

public class ProgressController : MonoBehaviour {
    public GameObject Player;
    public GameObject[] EnemyPrefabs;
    public GameObject[] Enemies;
    public Transform MoveCameraForwardPoint;
    public BoxCollider BoundingBox;


    public float RangeBetweenEncounter = 10f;
    
    private float _nextEncounterX;
    
    private bool _inCombat;

    private Vector3 _cameraTargetPosition;
    private Vector3 _cameraTargetPositionMax;

    private int _encounterNumber = 0;
    private int _enemyCheckCounter = 0;

	// Use this for initialization
	void Start () {
        _cameraTargetPosition = transform.position;
        UpdateEnemyCache();
        _inCombat = true;
	}
	
	// Update is called once per frame
	void Update () {
        _enemyCheckCounter++;
        if (_enemyCheckCounter > 30) {
            UpdateEnemyCache();
        }

        if (_inCombat) {
            if (Enemies.Length == 0) {
                //Combat over
                _inCombat = false;
                _nextEncounterX = MoveCameraForwardPoint.transform.position.x + RangeBetweenEncounter;
            }
        } else {
            //Not in combat
            if (Player.transform.position.x >= _nextEncounterX - BoundingBox.bounds.extents.x) {
                //Trigger encounter
                CreateEncounter();
            } else {
                //Looking for next encounter
                _cameraTargetPosition = new Vector3(Player.transform.position.x - MoveCameraForwardPoint.localPosition.x, transform.position.y, transform.position.z);
                if (_cameraTargetPosition.x > _cameraTargetPositionMax.x) {
                    _cameraTargetPositionMax = _cameraTargetPosition;
                } else {
                    _cameraTargetPosition = _cameraTargetPositionMax;
                }
            }
        }
        transform.position = Vector3.Lerp(transform.position, _cameraTargetPosition, Time.deltaTime * 4f);
	}

    void CreateEncounter() {
        _inCombat = true;
        _encounterNumber++;
        _cameraTargetPosition = transform.position;
        SpawnEnemies();
        UpdateEnemyCache();

        //StartCoroutine(WaitForEnemies());
    }

    void SpawnEnemies() {
        print("spawning");
        for (int i = 0; i < _encounterNumber; i++) {
            int random = Random.Range(0, EnemyPrefabs.Length);
            Vector3 randomPosition = new Vector3(Random.Range(BoundingBox.bounds.center.x + BoundingBox.bounds.extents.x/2f, BoundingBox.bounds.max.x), Random.Range(BoundingBox.bounds.min.y, BoundingBox.bounds.max.y), 0f);
            GameObject go = Instantiate(EnemyPrefabs[random], randomPosition, Quaternion.identity) as GameObject;
        }
    }

    void UpdateEnemyCache() {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    /*
    IEnumerator WaitForEnemies() {
        _waitingForEnemies = true;
        yield return new WaitForSeconds(2f);
        _waitingForEnemies = false;
    }*/
}
