using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum Stat
{
    // Behaviour stats
    [StringValue("Hit Damage")]
    HIT_DAMAGE = 0,
    [StringValue("Speed")]
    SPEED = 1,
    [StringValue("Price")]
    PRICE = 2,
    [StringValue("Expl. Range")]
    EXPLOSION_RANGE = 3,
    [StringValue("Expl. Damage")]
    EXPLOSION_DAMAGE = 4,
    [StringValue("# Mini Balls")]
    NUMBER_OF_MINI_SCATTER_BALLS = 5,
    [StringValue("Money on Collision")]
    MONEY_ON_COLLISION = 6,

    // Demographic stats
    [StringValue("Count")]
    COUNT = 101,
    [StringValue("Total Damage")]
    TOTAL_DAMAGE = 102,
}

public static class EnumExtensions
{
    public static string GetStringValue(this Stat value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        if (field.GetCustomAttribute(typeof(StringValueAttribute)) is StringValueAttribute attribute)
        {
            return attribute.Value;
        }

        return value.ToString();
    }
}
