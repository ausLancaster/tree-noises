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
	float4 _Color01;
	float4 _Color02;
	float4 _Color03;
	float4 _Color04;
	float4 _Color05;
	float4 _Color06;
	float4 _Color07;
	float4 _Color08;
	float4 _Color09;
	float4 _Color10;
	float4 _Color11;
	float4 _Color12;
	float4 _Color13;
	float4 _Color14;
	float4 _Color15;
	float4 _Color16;
	float4 _Color17;
	float4 _Color18;
	float4 _Color19;




	float4 frag(v2f i) : SV_Target // takes v2f and returns a color (float4)
	{
		//float4 color = lerp(tex2D(_MainTex, i.uv * 2), tex2D(_SecondTex, i.uv * 2), _Tween);
		float4 color;
		if (i.uv.x > 0.00f) {
			color = _Color00;
		}
		if (i.uv.x > 0.05f) {
			color = _Color01;
		}
		if (i.uv.x > 0.1f) {
			color = _Color02;
		}
		if (i.uv.x > 0.15f) {
			color = _Color03;
		}
		if (i.uv.x > 0.2f) {
			color = _Color04;
		}
		if (i.uv.x > 0.25f) {
			color = _Color05;
		}
		if (i.uv.x > 0.3f) {
			color = _Color06;
		}
		if (i.uv.x > 0.35f) {
			color = _Color07;
		}
		if (i.uv.x > 0.4f) {
			color = _Color08;
		}
		if (i.uv.x > 0.45f) {
			color = _Color09;
		}
		if (i.uv.x > 0.5f) {
			color = _Color10;
		}
		if (i.uv.x > 0.55f) {
			color = _Color11;
		}
		if (i.uv.x > 0.6f) {
			color = _Color12;
		}
		if (i.uv.x > 0.65f) {
			color = _Color13;
		}
		if (i.uv.x > 0.7f) {
			color = _Color14;
		}
		if (i.uv.x > 0.75f) {
			color = _Color15;
		}
		if (i.uv.x > 0.8f) {
			color = _Color16;
		}
		if (i.uv.x > 0.85f) {
			color = _Color17;
		}
		if (i.uv.x > 0.9f) {
			color = _Color18;
		}
		if (i.uv.x > 0.95f) {
			color = _Color19;
		}
		return color;
	}
		ENDCG
	}
	}
}