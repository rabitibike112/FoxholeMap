Shader "Hidden/Test"
{
    Properties
    {
        _Color("Tint", Color) = (0, 0, 0, 0.5)
        _MainTex("Texture", 2D) = "white" {}
    }
        
    SubShader
    {
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent"}

		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite off

		Pass{
			Stencil {
				Ref 2
				Comp NotEqual
				Pass Replace
			}

			 Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;

			fixed4 _Color;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET{
				fixed4 col = tex2D(_MainTex, i.uv);
			if (col.a < 0.9) {
				discard;
			}
				col *= _Color;
				return col;
			}

			ENDCG
		}
    }
}
