Shader "Aubergine/Objects/Surf/Toony/Diffuse" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		//Absolute path
		#include "Assets/Aubergines Toon Shaders/Shaders/Includes/Aubergine_Lights.cginc"
		//Or you can use relative path as below, whatever suits you
		//#include "../../../Includes/Aubergine_Lights.cginc"
		#pragma surface surf Aub_Toon

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}