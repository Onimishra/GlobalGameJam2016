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
            default:
                Modifier = null;
                break;
        }
    }
}

public enum EffectType {
    AddDamage,
    KnockBack
}