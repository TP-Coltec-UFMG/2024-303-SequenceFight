Shader "ColorBlindnessShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            uniform int _BlindType;

            float3 protanopia(float3 c)
            {
                float3x3 m = float3x3(
                    float3(0.567, 0.558, 0.000),
                    float3(0.433, 0.442, 0.242),
                    float3(0.000, 0.000, 0.758)
                );
                
                return mul(m, c);
            }

            float3 protanomaly(float3 c)
            {
                float3x3 m = float3x3(
                    float3(0.817, 0.333, 0.000),
                    float3(0.333, 0.667, 0.000),
                    float3(0.000, 0.125, 0.875)
                );
                
                return mul(m, c);
            }

            float3 deuteranopia(float3 c)
            {
                float3x3 m = float3x3(
                    float3(0.625, 0.700, 0.000),
                    float3(0.375, 0.300, 0.300),
                    float3(0.000, 0.000, 0.700)
                );
                
                return mul(m, c);
            }

            float3 deuteranomaly(float3 c)
            {
                float3x3 m = float3x3(
                    float3(0.800, 0.258, 0.000),
                    float3(0.200, 0.742, 0.142),
                    float3(0.000, 0.000, 0.858)
                );
                
                return mul(m, c);
            }

            float3 tritanopia(float3 c)
            {
                float3x3 m = float3x3(
                    float3(0.950, 0.000, 0.000),
                    float3(0.050, 0.433, 0.475),
                    float3(0.000, 0.567, 0.525)
                );
                
                return mul(m, c);
            }

            float3 tritanomaly(float3 c)
            {
                float3x3 m = float3x3(
                    float3(0.967, 0.000, 0.000),
                    float3(0.033, 0.733, 0.183),
                    float3(0.000, 0.267, 0.817)
                );
                
                return mul(m, c);
            }

            float3 achromatopsia(float3 c)
            {
                float3x3 m = float3x3(
                    float3(0.299, 0.587, 0.114),
                    float3(0.299, 0.587, 0.114),
                    float3(0.299, 0.587, 0.114)
                );
                
                return mul(m, c);
            }

            float3 achromatomaly(float3 c)
            {
                float3x3 m = float3x3(
                    float3(0.618, 0.320, 0.062),
                    float3(0.163, 0.775, 0.062),
                    float3(0.163, 0.320, 0.516)
                );
                
                return mul(m, c);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 in_col = tex2D(_MainTex, i.uv).rgb;

                float3 out_col = in_col;

                if (_BlindType == 0)
                {
                    out_col = protanopia(in_col);
                }
                else if (_BlindType == 1)
                {
                    out_col = protanomaly(in_col);
                }
                else if (_BlindType == 2)
                {
                    out_col = deuteranopia(in_col);
                }
                else if (_BlindType == 3)
                {
                    out_col = deuteranomaly(in_col);
                }
                else if (_BlindType == 4)
                {
                    out_col = tritanopia(in_col);
                }
                else if (_BlindType == 5)
                {
                    out_col = tritanomaly(in_col);
                }
                else if (_BlindType == 6)
                {
                    out_col = achromatopsia(in_col);
                }
                else if (_BlindType == 7)
                {
                    out_col = achromatomaly(in_col);
                }

                return fixed4(out_col, 1.0);
            }
            ENDCG
        }
    }
}