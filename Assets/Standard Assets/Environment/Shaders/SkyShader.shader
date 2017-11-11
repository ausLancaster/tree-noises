// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/SkyShader" {
	Properties {
		_Color1("Color1", Color) = (0, 0, 0, 1)
		_Color2("Color2", Color) = (0.5, 0.5, 0.5, 1)
		_Color3("Color3", Color) = (0.8, 0.8, 0.8, 1)
		_Color4("Color4", Color) = (1, 1, 1, 1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_CloudSpeed("Cloud Speed", Range(0, 20)) = 2

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		//Tags{ "LightMode" = "Vertex" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		#include "UnityCG.cginc"

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float2 hash(float2 x) // Noise functions by inigo quilez
		{
			const float2 k = float2(0.3183099, 0.3678794);
			x = x*k + k.yx;
			return -1.0 + 2.0*frac(16.0 * k*frac(x.x*x.y*(x.x + x.y)));
		}

		float noise(in float2 p)
		{
			float2 i = floor(p);
			float2 f = frac(p);

			float2 u = f*f*(3.0 - 2.0*f);

			return lerp(lerp(dot(hash(i + float2(0.0, 0.0)), f - float2(0.0, 0.0)),
				dot(hash(i + float2(1.0, 0.0)), f - float2(1.0, 0.0)), u.x),
				lerp(dot(hash(i + float2(0.0, 1.0)), f - float2(0.0, 1.0)),
					dot(hash(i + float2(1.0, 1.0)), f - float2(1.0, 1.0)), u.x), u.y);
		}

		float fbm(float2 p, float t) {
			float persistence = 1 / 2.0f;
			int octaves = 4;
			float total = 0;
			float2 speeds[4] = { float2(0, 1), float2(1, 0), float2(0, 1), float2(1, 0) };

			for (int i = 0; i < octaves; ++i) {
				float frequency = pow(2.0, i);
				float amplitude = pow(persistence, i);

				total += amplitude  * noise(p*frequency + t*frequency*speeds[i]);
			}

			return total;
		}

		float _Wave1Freq;
		float _Wave2Freq;
		float _Wave1Speed;
		float _Wave2Speed;
		float4 _WaveDirs;
		float _Step1;
		float _Step2;
		float _Step3;
		float3 _Color1;
		float3 _Color2;
		float3 _Color3;
		float3 _Color4;

		float3 getColor(Input IN) {
			float2 distortion;
			distortion.x = fbm(float2(IN.worldPos.x*(1 / 100.0), IN.worldPos.z*(1 / 100.0)), _Time.y*(1 / 10.0));
			distortion.y = fbm(float2(IN.worldPos.x*(1 / 100.0), IN.worldPos.z*(1 / 100.0)) + float2(1, 1), _Time.y*(1 / 10.0));
			float noise = fbm(float2(IN.worldPos.x*(1 / 100.0), IN.worldPos.z*(1 / 100.0)) + distortion, _Time.y*(1/20.0)) + 0.5;
			float3 skyColor = float3(1, 1, 0);
			float3 result;
			float greyColor = noise * float3(1, 1, 1);
			if (noise > 0.6) {
				return lerp(greyColor, skyColor, noise-0.6);
			}
			else {
				return greyColor;
			}
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = getColor(IN);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
