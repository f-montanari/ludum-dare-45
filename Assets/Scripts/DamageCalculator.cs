public static class DamageCalculator
{
    public static float CalculatePhisicalDamage(Entity sourceEntity, Entity destinationEntity)
    {
        // TODO: Level, crit chance, etc. influences.
        float baseDamage = sourceEntity.Stats.BaseDamage;
        float enemyArmor = destinationEntity.Stats.Armor;

        return baseDamage * (10f / (10f + enemyArmor));
    }

    public static float CalculateMagicDamage(Entity sourceEntity, Entity destinationEntity)
    {
        // TODO: Reference spell magic damage and source entity
        float magDamage = sourceEntity.Stats.BaseDamage;
        float enemyMagicResist = destinationEntity.Stats.MagicResist;

        return magDamage * (10f / (10f + magDamage));
    }
}
