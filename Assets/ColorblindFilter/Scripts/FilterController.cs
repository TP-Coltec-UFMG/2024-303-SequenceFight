using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ColorblindFilter.Scripts
{
    public class FilterController : MonoBehaviour
    {
        [SerializeField] private ColorblindFilter colorblindFilter;
        [SerializeField] private Toggle toggle;
        [SerializeField] private TMP_Dropdown dropdown;

        public void UseFilter() =>
            colorblindFilter.SetUseFilter(toggle.isOn);

        public void ChangeBlindType()
        {
            colorblindFilter.ChangeBlindType((BlindnessType) dropdown.value);
        }
    }
}