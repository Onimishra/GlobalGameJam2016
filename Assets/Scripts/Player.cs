using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using DG.Tweening;


public class Player : Controllable, IAttacker {
	[Header("Player specific")]
	public Transform attackOrigo;
	public AttackPlane normalAttackPlane;

	static readonly float baseAttackCooldown = 0.15f;
	private float attackCooldown = baseAttackCooldown;

	float baseMovementSpeed;

    public Transform HatAttachmentPoint;
    private List<Pickup> _attachedHats = new List<Pickup>();
    private int baseHatLayer = 1;
    private float _previousHatRotation = 0f;
    public HatHolder HatHolderObject;

    [SerializeField]
    public AnimationCurve HatCurve;
     
	List<AttackModifier> modifiers = new List<AttackModifier>() { new NormalDamage(3), new NormalDamage(3), new KnockBack(2) };
	public List<AttackModifier> Modifiers() {
		return modifiers;
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
		health = 100;
		baseMovementSpeed = movementSpeed;
		ctrl = new PlayerController ();
        HatHolderObject = GameObject.Find("HatHolder").GetComponent<HatHolder>();
	}
	
	// Update is called once per frame
	new protected void Update() {
		base.Update ();

		if (attackCooldown < baseAttackCooldown) {
			movementSpeed = baseMovementSpeed / 2f;
		} else {
			movementSpeed = baseMovementSpeed;
		}

		if(ctrl.Attack() && attackCooldown >= baseAttackCooldown) {
			attackCooldown = 0;
			Attack ();
		}
		attackCooldown = Mathf.Min (attackCooldown + Time.deltaTime, 1f);
	}

	public void Attack() {
		var plane = GameObject.Instantiate<AttackPlane> (normalAttackPlane);
		plane.transform.position = attackOrigo.position;
		var s = plane.transform.localScale;
		s.x = s.x * Mathf.Sign (ctrl.Movement ().x);
		plane.transform.localScale = s;
		plane.Mask = AttackPlane.HitMask.Evils;
		plane.Owner = this;
		plane.Direction = ctrl.Movement ().x < 0 ? AttackPlane.AttackDirection.Left : AttackPlane.AttackDirection.Right;
		plane.LifeSpan = 0.1f;
		plane.MovementSpeed = 10;
		animator.SetTrigger ("Attacking");

	}

    public void AddHat(Pickup hat) {
        Transform parent = HatHolderObject.GetParentForHat();
        HatHolderObject.Hats.Add(hat);
        hat.gameObject.transform.parent = parent;
        hat.transform.localPosition = -hat.AttachBot.transform.localPosition;

        //Rotate a bit
        //hat.transform.RotateAround(hat.AttachBot.position, Vector3.forward, _previousHatRotation + Random.Range(-10f, 10f));

        _attachedHats.Add(hat);
        hat.HatSprite.sortingOrder = baseHatLayer + _attachedHats.Count;


        //Add effect 
        modifiers.Add(hat.EffectObject.Modifier);
    }

    override public void GotHit() {
        if (_attachedHats.Count > 0) {
            //Drop hats
            StartCoroutine(DropHats());
        } else {
            //Die
            print("YOU DEAD");
        }
    }

    private IEnumerator DropHats() {
        List<Pickup> droppedHats = new List<Pickup>();
        foreach (var hat in _attachedHats) {
            modifiers.Remove(hat.EffectObject.Modifier);
            droppedHats.Add(hat);
        }
        _attachedHats.Clear();
        yield return new WaitForSeconds(0.05f);
        for (int i = droppedHats.Count - 1; i >= 0; i--) {
            var hat = droppedHats[i];
            hat.transform.SetParent(null);
            HatHolderObject.Hats.Remove(hat);
            //Find a position for the hat
            Vector3 target = new Vector3(Random.Range(bounds.bounds.min.x, bounds.bounds.max.x), Random.Range(bounds.bounds.min.y, bounds.bounds.max.y), bounds.bounds.center.z);
            Vector3 midTarget = transform.position + (target - transform.position) / 2f;
            midTarget.y = transform.position.y + 8f;
            //hat.transform.DOMove(target, 0.9f);
            //hat.transform.DOMove(target, 0.9f).SetEase(Ease.InOutCubic);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(hat.transform.DOJump(target, 6f, 1, 1f));
        }

        yield return new WaitForSeconds(1.2f);


        foreach (var hat in droppedHats) {
            hat.EnablePickup();
        }
        
    }

    

	new public GameObject entity () {
		return gameObject;
	}

	public void GotKill (Controllable victim) {
		var scoreBoard = FindObjectOfType<ScoreBoard> ();

		var enemy = victim.GetComponent<Enemy> ();
		if (enemy == null)
			return;
		
		scoreBoard.AddScore (enemy.pointsWorth);
	}
}
