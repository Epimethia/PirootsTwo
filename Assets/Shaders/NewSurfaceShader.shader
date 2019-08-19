Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Strength("Strength", Range(0,2)) = 1.0
        _Speed("Speed", Range(-200, 200)) = 100.0
    }
    SubShader
    {
        Tags { "RenderType"="transparent" }
        Pass
        Cull Off
        CGPROGRAM

        #pragma vertex vertexFunc
        #pragma fragment fragFunc

        float4 _Color;
        float _Strength;
        float _Speed;

        struct vertexInput {
            float4 vertex : POSITION;
        }

        struct vertexOutput{
            float4 pos : SV_Position;
        }

        vertexOutput vertexFunc(vertexInout IN)
    }
}
