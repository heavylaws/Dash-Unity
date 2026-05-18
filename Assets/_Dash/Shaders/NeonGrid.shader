Shader "Custom/NeonGrid"
{
    Properties
    {
        _GridColor ("Grid Color", Color) = (1,0,1,1)
        _LineWeight ("Line Weight", Range(0, 0.2)) = 0.02
        _Tiling ("Tiling", Float) = 20
        _Emission ("Emission Intensity", Float) = 2.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _GridColor;
            float _LineWeight;
            float _Tiling;
            float _Emission;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float2 uv = input.uv * _Tiling;
                float2 grid = abs(frac(uv - 0.5) - 0.5);
                float lineX = smoothstep(_LineWeight, 0, grid.x);
                float lineY = smoothstep(_LineWeight, 0, grid.y);
                float mask = max(lineX, lineY);
                
                return _GridColor * mask * _Emission;
            }
            ENDHLSL
        }
    }
}
