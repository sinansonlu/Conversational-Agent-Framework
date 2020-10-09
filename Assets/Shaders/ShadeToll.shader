Shader "Custom/ShadeToll" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_OcTex ("Occulusion (RGB)", 2D) = "white" {}
		_RoughTex ("Roughness (RGB)", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_NF1 ("Normal Factor 1", Range(0,1)) = 0.0
		_NF2 ("Normal Factor 2", Range(0,1)) = 0.0
		_NF3 ("Normal Factor 3", Range(0,1)) = 0.0
		_NF4 ("Normal Factor 4", Range(0,1)) = 0.0
		_BF1 ("Blush Factor 1", Range(0,1)) = 0.0
		_BF2 ("Blush Factor 2", Range(0,1)) = 0.0
		_NormalMap1("Normal Map 1", 2D) = "bump" {}
		_NormalMap2("Normal Map 2", 2D) = "bump" {}
		_NormalMap3("Normal Map 3", 2D) = "bump" {}
		_NormalMap4("Normal Map 4", 2D) = "bump" {}
		_BlushTex1("Blush (RGB)", 2D) = "white" {}
		_BlushTex2("Blush (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _OcTex;
		sampler2D _RoughTex;
		sampler2D _BlushTex1;
		sampler2D _BlushTex2;
		sampler2D _NormalMap;
		sampler2D _NormalMap1;
		sampler2D _NormalMap2;
		sampler2D _NormalMap3;
		sampler2D _NormalMap4;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float3 viewDir;
		};

		float _NF1;
		float _NF2;
		float _NF3;
		float _NF4;
		float _BF1;
		float _BF2;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float4 c = tex2D (_MainTex, IN.uv_MainTex);
			float4 blushZero = float4(1, 1, 1, 1);
			float4 cblush1 = tex2D (_BlushTex1, IN.uv_MainTex);
			float4 cblush2 = tex2D (_BlushTex2, IN.uv_MainTex);
			float3 occ = tex2D(_OcTex, IN.uv_MainTex);
			
			o.Albedo = c.rgb * lerp(blushZero,cblush1.rgb,_BF1) * lerp(blushZero, cblush2.rgb, _BF2) * occ;
			o.Occlusion = 1;
			o.Metallic = 0;
			o.Smoothness = tex2D(_RoughTex, IN.uv_MainTex);
			o.Alpha = c.a;
			
			float3 N0 = float3(0, 0, 1);
			float3 NM = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			float3 NV1 = lerp(N0,UnpackNormal(tex2D(_NormalMap1, IN.uv_NormalMap)),_NF1);
			float3 NV2 = lerp(N0,UnpackNormal(tex2D(_NormalMap2, IN.uv_NormalMap)),_NF2);
			float3 NV3 = lerp(N0,UnpackNormal(tex2D(_NormalMap3, IN.uv_NormalMap)),_NF3);
			float3 NV4 = lerp(N0,UnpackNormal(tex2D(_NormalMap4, IN.uv_NormalMap)),_NF4);
			float3 K = normalize(float3(NM.rg + NV1.rg + NV2.rg + NV3.rg + NV4.rg, NM.b * NV1.b * NV2.b * NV3.b * NV4.b));

			o.Normal = K;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
