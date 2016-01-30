[System.Serializable]
public class Effect {
    public EffectType Type;
    public float Amount;
    public AttackModifier Modifier;

    public void CreateModifierInstance() {
        switch (Type) {
            case EffectType.AddDamage:
                Modifier = new NormalDamage((int)Amount);
                break;
            case EffectType.KnockBack:
                Modifier = new KnockBack(Amount);
                break;
            case EffectType.MoveFaster:
                Modifier = new MoveFaster(Amount);
                break;
            case EffectType.MoveSlower:
                Modifier = new MoveSlower(Amount);
                break;
            case EffectType.UpsideDown:
                Modifier = new UpsideDown((int)Amount);
                break;
            case EffectType.Fear:
                Modifier = new Fear((int)Amount);
                break;
            case EffectType.AddEffect:
                Modifier = new AddEffect((int)Amount);
                break;
            default:
                break;
        }
    }
}

public enum EffectType {
    AddDamage,
    KnockBack,
    MoveFaster,
    MoveSlower,
    UpsideDown, 
    Fear,
    AddEffect
}