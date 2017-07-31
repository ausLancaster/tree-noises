Shader "Custom/Test1" {
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SecondTex("Second Texture", 2D) = "white" {}
		_Tween("Tween", Range(0, 1)) = 0
		_Color ("Color", Color) = (0.25, 0.65, 0.45, 0.5)
	}
	SubShader{
		Tags
	{
		"Queue" = "Transparent"
}

		Pass
		{
			Blend One One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata // defines what information we are getting from each vertex
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f // defines what information we are passing into the fragment function
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v) // takes a vert and returns a v2f
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _SecondTex;
			float4 _Color;
			float _Tween;

			float4 frag(v2f i) : SV_Target // takes v2f and returns a color (float4)
			{
				float4 color = lerp(tex2D(_MainTex, i.uv*2), tex2D(_SecondTex, i.uv*2), _Tween);
				return color;
			}
				ENDCG
		}
	}
}