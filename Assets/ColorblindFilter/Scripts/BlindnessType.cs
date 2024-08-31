using System;

namespace ColorblindFilter.Scripts
{
    [Serializable]
    public enum BlindnessType
    {
        Protanopia,
        Protanomaly,
        Deuteranopia,
        Deuteranomaly,
        Tritanopia,
        Tritanomaly,
        Achromatopsia,
        Achromatomaly,
    }
}