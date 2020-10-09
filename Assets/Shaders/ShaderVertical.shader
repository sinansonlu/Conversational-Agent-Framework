Shader "Custom/ShaderVertical" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_ColorE ("ColorE", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		fixed4 _ColorE;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Alpha = c.a;
			// float ee = frac(pow(IN.uv_MainTex.x - (_Time * 3),2)) / 2.5;
			float ee = (pow(sin((IN.uv_MainTex.x - (_Time * 3)) * 6),50));
			o.Emission = _ColorE * ee;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
