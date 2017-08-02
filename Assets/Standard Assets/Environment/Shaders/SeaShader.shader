// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/SeaShader" {
	Properties {
		_Color1("Color1", Color) = (0, 0, 0, 1)
		_Color2("Color2", Color) = (0.5, 0.5, 0.5, 1)
		_Color3("Color3", Color) = (0.8, 0.8, 0.8, 1)
		_Color4("Color4", Color) = (1, 1, 1, 1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Wave1Freq("Wave 1 Frequency", Range(0, 5)) = 0.1
		_Wave2Freq("Wave 2 Frequency", Range(0, 5)) = 0.05
		_Wave1Speed("Wave 1 Speed", Range(0, 20)) = 2
		_Wave2Speed("Wave 2 Speed", Range(0, 20)) = 1
		_WaveDirs("Wave 1 & 2 Directions", Vector) = (0.707, 0.707, -0.707, 0.707)
		_Step1("Color Step 1", Range(0, 1)) = 0.3
		_Step2("Color Step 2", Range(0, 1)) = 0.7
		_Step3("Color Step 3", Range(0, 1)) = 0.95
		_SunPosition("Sun Position", Vector) = (0, 0, 0)
		_SlopeWidth("Slope Width", Range(0, 50)) = 10
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

		float3 _SunPosition;
		float _SlopeWidth;

		float sunReflection(Input IN) {
			float cutoffLength = 10;
			
			float m = (_SunPosition.z - _WorldSpaceCameraPos.z) / (_SunPosition.x - _WorldSpaceCameraPos.x);
			float y = m * (IN.worldPos.x - _WorldSpaceCameraPos.x) + _WorldSpaceCameraPos.z;
			float m_perp = (-1 / m);
			float cutoff = m_perp*(IN.worldPos.x - _WorldSpaceCameraPos.x) + _WorldSpaceCameraPos.z;

			if (IN.worldPos.z > cutoff) {
				return 0;
			}
			_SlopeWidth = abs(_SlopeWidth / m_perp);
			if (IN.worldPos.z > y - _SlopeWidth && IN.worldPos.z < y + _SlopeWidth) {
				float slope = abs(y-IN.worldPos.z);
				float slopeValue = ((_SlopeWidth -slope)/ _SlopeWidth) * 0.5;
				if (IN.worldPos.z < cutoff && IN.worldPos.z > cutoff - cutoffLength) {
					return (cutoff - IN.worldPos.z) / cutoffLength * slopeValue;
				}
				return slopeValue;
			}
			return 0;
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
			float noise1 = noise((IN.worldPos.xz + _Time.y*_Wave1Speed*_WaveDirs.xy) * _Wave1Freq) + 0.5;
			float noise2 = noise((IN.worldPos.xz + _Time.y*_Wave2Speed*_WaveDirs.zw) * _Wave2Freq) + 0.5;
			float totalNoise = ((noise1 + noise2) / 2);
			totalNoise += sunReflection(IN);
			if (totalNoise < _Step1) {
				return _Color1;
			}
			else if (totalNoise < _Step2) {
				return _Color2;
			}
			else if (totalNoise < _Step3) {
				return _Color3;
			}
			else {
				return _Color4;
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
