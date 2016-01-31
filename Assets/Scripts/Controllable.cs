using UnityEngine;
using System.Collections;
using System;

public class Controllable : MonoBehaviour {
	[Header("Controllable")]
	public AttackPlane.HitMask hitMask;
	public float movementSpeed = 1;
	public float jumpHeight = 1;
	public float jumpSpeed = 1;
	public Collider bounds;

	float movementDamp = 0.8f;
    private float slowTimeOut = 0f;


	public GameObject entity;
	private Vector3 origoPos;
	private Vector3 origoScale;

	public GameObject shadow;
	private Vector3 shadowOrigoScale;

	protected float dir = 1;

	[SerializeField]
	AnimationCurve curve;
	float curveTimer;
	Boolean jumping;

	protected Controller ctrl = new NonController();

	protected int health;

	protected Animator animator;

    public bool Disabled = false;

	protected void Start() {
        if (!entity) {
            entity = gameObject;
        }

		var p = transform.position;
		p.z = p.y;
		transform.position = p;
		origoPos = entity.transform.localPosition;
		origoScale = entity.transform.localScale;
        if (shadow) {
            shadowOrigoScale = shadow.transform.localScale;
        }

		animator = GetComponentInChildren<Animator> ();
        if (!bounds) {
            bounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<BoxCollider>();
        }
	}
	
	// Update is called once per frame
	protected void Update () {
        if (Disabled) {
            return;
        }
		ctrl.Update ();

		jumping = ctrl.Jump () && curveTimer == 0;
		if(jumping || curveTimer > 0 && curveTimer < 1) {
			curveTimer += Time.deltaTime * jumpSpeed; 
			var curveEval = curve.Evaluate (curveTimer);
			var t = Vector2.up * curveEval * jumpHeight;
			entity.transform.localPosition = origoPos + new Vector3(t.x, t.y, 0);
			shadow.transform.localScale = shadowOrigoScale + shadowOrigoScale * -curveEval;
		} else {
			entity.transform.localPosition = origoPos;
            if (shadow) {
                shadow.transform.localScale = shadowOrigoScale;
            }
			curveTimer = 0;
		}

		var m = ctrl.Movement ();
		var movement = new Vector2(m.x, m.y * movementDamp);

		var scaledMovement = movement * Time.deltaTime * movementSpeed;
		var desiredPos = transform.position + new Vector3 (scaledMovement.x, scaledMovement.y, scaledMovement.y);
		var nextPosition = bounds.bounds.ClosestPoint (desiredPos);
		nextPosition.y = nextPosition.z;

		if (Mathf.Abs (movement.x) > 0.01f)
			dir = Mathf.Sign (movement.x);

		entity.transform.localScale = new Vector3(dir * origoScale.x, origoScale.y, origoScale.z);

		transform.position = bounds.bounds.ClosestPoint (nextPosition);

		if(animator != null)
			animator.SetBool ("Moving", movement.magnitude > 0);
	}
    public void Restart() {
        Start();
        Disabled = false;
    }

	public void AddHealth (int amount) {
		health += amount;
	}
    public virtual void GotHit() {
    }
	public int Health () {
		return health;
	}

    public void ChangeMoveSpeed(float amount) {
        if (Time.time > slowTimeOut) {
            movementSpeed = movementSpeed * amount;
            slowTimeOut = Time.time + 3f;
        }

    }
    public void UpsideDown(float amount) {
        print("wut");
        origoScale = new Vector3(origoScale.x, -origoScale.y, origoScale.z); ;
        SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        foreach (var item in spr) {
            print("hej " + item.name);
            item.transform.localScale = new Vector3(item.transform.localScale.x, -item.transform.localScale.y, item.transform.localScale.z);
        }
    }

    public void AddEffect2(int lol) {
        //SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        SkinnedMeshRenderer[] spr = GetComponentsInChildren<SkinnedMeshRenderer>();
        int something = UnityEngine.Random.Range(0, 66);
        if (something == 0) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_4Gradients>(); } } else if (something == 1) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Additive>(); } } else if (something == 2) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_BlackHole>(); } } else if (something == 3) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Blood>(); } } else if (something == 4) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Blur>(); } } else if (something == 5) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_BurningFX>(); } } else if (something == 6) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Cartoon>(); } } else if (something == 7) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_CircleFade>(); } } else if (something == 8) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Clipping>(); } } else if (something == 9) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Color>(); } } else if (something == 10) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_ColorChange>(); } } else if (something == 11) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_ColorRGB>(); } } else if (something == 12) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_CompressionFX>(); } } else if (something == 13) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_DesintegrationFX>(); } } else if (something == 14) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_DestroyedFX>(); } } else if (something == 15) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_EdgeColor>(); } } else if (something == 16) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_EnergyBar>(); } } else if (something == 17) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Fire>(); } } else if (something == 18) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_FireAdditive>(); } } else if (something == 19) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Ghost>(); } } else if (something == 20) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_GoldenFX>(); } } else if (something == 21) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_GoldFX>(); } } else if (something == 22) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_GrassFX>(); } } else if (something == 23) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_GrassMultiFX>(); } } else if (something == 24) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_GrayScale>(); } } else if (something == 25) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Heat>(); } } else if (something == 26) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Hologram>(); } } else if (something == 27) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Hologram2>(); } } else if (something == 28) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Hologram3>(); } } else if (something == 29) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_HSV>(); } } else if (something == 30) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Ice>(); } } else if (something == 31) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_IcedFX>(); } } else if (something == 32) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Jelly>(); } } else if (something == 33) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_JellyAutoMove>(); } } else if (something == 34) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Lightning>(); } } else if (something == 35) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_LightningBolt>(); } } else if (something == 36) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Liquid>(); } } else if (something == 37) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Liquify>(); } } else if (something == 38) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_MetalFX>(); } } else if (something == 39) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Negative>(); } } else if (something == 40) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Noise>(); } } else if (something == 41) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_NoiseAnimated>(); } } else if (something == 42) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Outline>(); } } else if (something == 43) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Pattern>(); } } else if (something == 44) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_PatternAdditive>(); } } else if (something == 45) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Pixel>(); } } else if (something == 46) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Pixel8bitsBW>(); } } else if (something == 47) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Pixel8bitsC64>(); } } else if (something == 48) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Pixel8bitsGB>(); } } else if (something == 49) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_PlasmaRainbow>(); } } else if (something == 50) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_PlasmaShield>(); } } else if (something == 51) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Posterize>(); } } else if (something == 52) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_SandFX>(); } } else if (something == 53) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Sepia>(); } } else if (something == 54) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Sharpen>(); } } else if (something == 55) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Shiny_Reflect>(); } } else if (something == 56) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_SkyCloud>(); } } else if (something == 57) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Slim>(); } } else if (something == 58) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_StoneFX>(); } } else if (something == 59) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Teleportation>(); } } else if (something == 60) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Threshold>(); } } else if (something == 61) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Twist>(); } } else if (something == 62) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_WaterAndBackground>(); } } else if (something == 63) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_WaterAndBackgroundDeluxe>(); } } else if (something == 64) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_Wave>(); } } else if (something == 65) { foreach (var item in spr) { item.gameObject.AddComponent<_2dxFX_WoodFX>(); } }
    }

    public void AddEffect(int lol) {
        //SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        SkinnedMeshRenderer[] spr = GetComponentsInChildren<SkinnedMeshRenderer>();
        int something = UnityEngine.Random.Range(0, 66);
        foreach (var item in spr) {
            item.gameObject.AddComponent<UVTextureAnimator>();
        }
    }
    
}
