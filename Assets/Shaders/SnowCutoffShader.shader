Shader "Custom/SnowCutoffShader"
{
    Properties {
		_Tess("Tessellation", Range(1,64)) = 4
		_Displacement("Displacement", Range(0, 1.0)) = 0.3
		_Splatmap("Splatmap", 2D) = "black" {}
		_Color      ("Color"       , Color      ) = (1, 1, 1, 1)
		_MainTex    ("Albedo (RGB)", 2D         ) = "white" {}
		_Glossiness ("Smoothness"  , Range(0, 1)) = 0.5
		_Metallic   ("Metallic"    , Range(0, 1)) = 0.0
		_Cutoff     ("Cutoff"      , Range(0, 1)) = 0.5
	}

	SubShader {
		Tags {
			"Queue"      = "AlphaTest"
			"RenderType" = "TransparentCutout"
		}

		LOD 200
		
		Cull Off
		
		CGPROGRAM
			#pragma target 4.6
			#pragma surface surf Standard fullforwardshadows alphatest:_Cutoff vertex:disp tessellate:tessDistance

			#include "Tessellation.cginc"

			sampler2D _Splatmap;

			fixed4 _Color;
			sampler2D _MainTex;
			half _Glossiness;
			half _Metallic;

			struct appdata {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			float _Tess;

			float4 tessDistance(appdata v0, appdata v1, appdata v2) {
				float minDist = 100.0;
				float maxDist = 250.0;
				return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
			}
			float _Displacement;

			void disp(inout appdata v)
			{
				float d = tex2Dlod(_Splatmap, float4(v.texcoord.xy, 0, 0)).r * _Displacement;
				v.vertex.xyz -= v.normal * d;
				v.vertex.xyz += v.normal * _Displacement;
			}

			struct Input {
				float2 uv_MainTex;
				float2 uv_Splatmap;
			};

			void surf (Input IN, inout SurfaceOutputStandard o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				fixed4 s = tex2D(_Splatmap, IN.uv_Splatmap);

				o.Albedo     = c.rgb;
				o.Metallic   = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha      = 1-s.r;
			}
		ENDCG
	}

	FallBack "Transparent/Cutout/Diffuse"
}
