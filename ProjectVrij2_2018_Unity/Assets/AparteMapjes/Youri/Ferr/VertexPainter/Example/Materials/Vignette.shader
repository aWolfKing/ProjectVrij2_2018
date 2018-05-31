Shader "Ferr/Example/Vignette" {
	Properties {
		_MainTex   ("Noise",       2D   ) = "white" {}
		_NoiseScale("Noise Scale", Float) = 0.01
		_Color1 ("Color 1", Color) = (1,1,1,1)
		_Color2 ("Color 2", Color) = (1,1,1,1)
		_Size   ("Size",    Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float2 noiseUV : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4    _MainTex_ST;
			float4    _Color1;
			float4    _Color2;
			float     _Size;
			float     _NoiseScale;
			
			v2f vert (appdata v) {
				v2f o;
				o.vertex  = UnityObjectToClipPos(v.vertex);
				o.uv      = v.uv;
				o.noiseUV = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
				// add in some noise to reduce banding
				float  noise = tex2D(_MainTex, i.noiseUV) * _NoiseScale;
				// get a lerp value based on distance from center of UVs
				float  d     = distance(i.uv, float2(.5,.5)) / _Size + noise;
				return lerp(_Color1, _Color2, d);
			}
			ENDCG
		}
	}
}
