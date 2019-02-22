Shader "Custom/Clay"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _NormalPower ("Normal Power", Float) = 1.0
        _Smoothness ("Smoothness", 2D) = "black" {}
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Distortion ("Distortion", Float) = 0.0
        _Power ("Power", Float) = 0.0
        _Scale ("Scale", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        
        #pragma surface surf StandardTranslucent fullforwardshadows
        // #pragma vertex vert
        #pragma target 3.0


        sampler2D _MainTex;
        sampler2D _Normal;
        sampler2D _Smoothness;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_Normal;
            float2 uv_Smoothness;
        };

        // half _Smoothness;
        half _Metallic;
        float _Distortion;
        float _Power;
        float _Scale;
        float _NormalPower;
        fixed4 _Color;

        #include "UnityPBSLighting.cginc"
        inline fixed4 LightingStandardTranslucent(SurfaceOutputStandard s, fixed3 viewDir, UnityGI gi)
        {
            // Original colour
            fixed4 pbr = LightingStandard(s, viewDir, gi);
            float3 L = gi.light.dir;
            float3 V = viewDir;
            float3 N = s.Normal;
            
            float3 H = normalize(L + N * _Distortion);
            float I = pow(saturate(dot(V, -H)), _Power) * _Scale;
            pbr.rgb = pbr.rgb + gi.light.color * I;
            return pbr;
        }
        
        void LightingStandardTranslucent_GI(SurfaceOutputStandard s, UnityGIInput data, inout UnityGI gi)
        {
            LightingStandard_GI(s, data, gi); 
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        
        // float rand(float3 myVector)  {
        //     return frac(sin( dot(myVector ,float3(12.9898,78.233,45.5432) )) * 43758.5453);
        // }
        
        // void vert(inout appdata_full v) {
        //     v.vertex.xyz += v.normal * rand(float3(v.texcoord1.xy, 0)) * 0.1;
        // }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Normal = UnpackScaleNormal(tex2D(_Normal, IN.uv_Normal), _NormalPower);
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = 1 - tex2D (_Smoothness, IN.uv_MainTex);
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
