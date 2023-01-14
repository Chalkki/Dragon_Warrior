using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorEffect
{
    public enum EffectType
    {
        HealEffect,
        DamagedEffect,
    }

    public EffectType effectType;

    public Color GetColor()
    {
        switch (effectType)
        {
            default:
            case EffectType.HealEffect:     return new Color(0.07f, 0.6f, 0.05f);
            case EffectType.DamagedEffect:  return new Color(1f, 0f, 0f);
        }
    }
}
