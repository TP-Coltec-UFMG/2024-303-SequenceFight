using UnityEngine;

namespace ColorblindFilter.Scripts
{
    [AddComponentMenu("Scripts/Color Blindness Filter")]
    public sealed class ColorblindFilter : MonoBehaviour
    {
        // private const string ShaderName = "ColorBlindnessShader";

        [SerializeField] private BlindnessType _blindnessType;
        [SerializeField] private bool _useFilter = true;

        private static readonly int BlindType = Shader.PropertyToID("_BlindType");

        public Shader _shader;
        private Material _mat;

        public bool UseFilter => _useFilter;

        private void Awake()
        {
            // _shader = LoadShader();
            _mat = new Material(_shader);
        }

        // private Shader LoadShader() 
        // {
        //     return Shader.Find(ShaderName);
        // }

        private void Update() {
            _mat.SetInt(BlindType, (int) _blindnessType);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            if (_useFilter)
            {
                Graphics.Blit(src, dst, _mat);
            }
            else
            {
                Graphics.Blit(src, dst);
            }
        }

        public void ChangeBlindType(BlindnessType blindType) 
        {
            _blindnessType = blindType;
        }

        public void SetUseFilter(bool useFilter) {
            _useFilter = useFilter;
        }
    }
}