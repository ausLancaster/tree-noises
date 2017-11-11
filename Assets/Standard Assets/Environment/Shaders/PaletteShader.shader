Shader "Custom/Palette" {
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color00("Color00", Color) = (0, 0, 0, 1)
		_Color01("Color01", Color) = (0, 0, 0, 1)
		_Color02("Color02", Color) = (0, 0, 0, 1)
		_Color03("Color03", Color) = (0, 0, 0, 1)
		_Color04("Color04", Color) = (0, 0, 0, 1)
		_Color05("Color05", Color) = (0, 0, 0, 1)
		_Color06("Color06", Color) = (0, 0, 0, 1)
		_Color07("Color07", Color) = (0, 0, 0, 1)
		_Color08("Color08", Color) = (0, 0, 0, 1)
		_Color09("Color09", Color) = (0, 0, 0, 1)
		_Color10("Color10", Color) = (0, 0, 0, 1)
		_Color11("Color11", Color) = (0, 0, 0, 1)
		_Color12("Color12", Color) = (0, 0, 0, 1)
		_Color13("Color13", Color) = (0, 0, 0, 1)
		_Color14("Color14", Color) = (0, 0, 0, 1)
		_Color15("Color15", Color) = (0, 0, 0, 1)
		_Color16("Color16", Color) = (0, 0, 0, 1)
		_Color17("Color17", Color) = (0, 0, 0, 1)
		_Color18("Color18", Color) = (0, 0, 0, 1)
		_Color19("Color19", Color) = (0, 0, 0, 1)

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
	float4 _Color00;

	float4 frag(v2f i) : SV_Target // takes v2f and returns a color (float4)
	{
		//float4 color = lerp(tex2D(_MainTex, i.uv * 2), tex2D(_SecondTex, i.uv * 2), _Tween);
		float4 color;
		if (i.uv.x > 0.0f) {
			color = _Color00;
		}
		else if (i.uv.x > 0.0f) {
			color = _Color00;
		}
		return color;
	}
		ENDCG
	}
	}
}