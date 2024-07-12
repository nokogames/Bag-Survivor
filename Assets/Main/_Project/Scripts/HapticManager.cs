using Lofelt.NiceVibrations;

public static class HapticManager
{
    public static void PlayHaptic(HapticType hapticType)
    {
        HapticPatterns.PlayPreset((HapticPatterns.PresetType)hapticType);


    }
}

public enum HapticType
{
    Selection = 0,
    Success = 1,
    Warning = 2,
    Failure = 3,
    LightImpact = 4,
    MediumImpact = 5,
    HeavyImpact = 6,
    RigidImpact = 7,
    SoftImpact = 8,
    None = -1

}