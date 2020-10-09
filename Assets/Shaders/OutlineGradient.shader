Shader "Custom/OutlinedGradient" {

	Properties{

		// COLOR GRADING SHADER
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Left Color", Color) = (1,1,1,1)
		_Color2("Right Color", Color) = (1,1,1,1)
		_Angle("Angle", Range(0,1)) = 0
		_InverseAngle("Invert the angle (0 off, 1 on)", Range(0,1)) = 0

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15

			// OUTLINED SHADER
			_FirstOutlineColor("Outline color", Color) = (1,0,0,0.5)
			_FirstOutlineWidth("Outlines width", Range(0.0, 2.0)) = 0.15

			_SecondOutlineColor("Outline color", Color) = (0,0,1,1)
			_SecondOutlineWidth("Outlines width", Range(0.0, 2.0)) = 0.025

			_AngleOutline("Switch shader on angle", Range(0.0, 180.0)) = 89
	}

		CGINCLUDE
#include "UnityCG.cginc"

			struct appdata {
			float4 vertex : POSITION;
			float4 normal : NORMAL;
		};

		uniform float4 _FirstOutlineColor;
		uniform float _FirstOutlineWidth;

		uniform float4 _SecondOutlineColor;
		uniform float _SecondOutlineWidth;
		uniform float _AngleOutline;

		ENDCG

			SubShader{

			// FIRST OUTLINE CREATION
			Pass
			{
				Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
				Blend SrcAlpha OneMinusSrcAlpha
				ZWrite Off
				Cull Back
				CGPROGRAM

				struct v2f {
					float4 pos : SV_POSITION;
				};

				#pragma vertex vert
				#pragma fragment frag

				v2f vert(appdata v)
				{
					appdata original = v;
					float3 scaleDir = normalize(v.vertex.xyz - float4(0,0,0,1));
					if (degrees(acos(dot(scaleDir.xyz, v.normal.xyz))) > _AngleOutline)
						v.vertex.xyz += normalize(v.normal.xyz) * _FirstOutlineWidth;
					else
						v.vertex.xyz += scaleDir * _FirstOutlineWidth;
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					return o;
				}
				half4 frag(v2f i) : COLOR{
					return _FirstOutlineColor;
				}
				ENDCG
			}

			// SECOND OUTLINE CREATION 
			Pass
			{
				Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
				Blend SrcAlpha OneMinusSrcAlpha
				ZWrite Off
				Cull Back
				CGPROGRAM

				struct v2f
				{
					float4 pos : SV_POSITION;
				};

				#pragma vertex vert
				#pragma fragment frag

				v2f vert(appdata v)
				{
					appdata original = v;

					float3 scaleDir = normalize(v.vertex.xyz - float4(0,0,0,1));
					if (degrees(acos(dot(scaleDir.xyz, v.normal.xyz))) > _AngleOutline)
						v.vertex.xyz += normalize(v.normal.xyz) * _SecondOutlineWidth;
					else
						v.vertex.xyz += scaleDir * _SecondOutlineWidth;

					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					return o;
				}

				half4 frag(v2f i) : COLOR{
					return _SecondOutlineColor;
				}
				ENDCG
			}

					// GRADIENT SHADER
					Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" "CanUseSpriteAtlas" = "True" }

					Stencil
					{
						Ref[_Stencil]
						Comp[_StencilComp]
						Pass[_StencilOp]
						ReadMask[_StencilReadMask]
						WriteMask[_StencilWriteMask]
					}

					Cull Off
					Lighting Off
					ZWrite Off
					ZTest[unity_GUIZTestMode]
					Fog{ Mode Off }
					Blend SrcAlpha OneMinusSrcAlpha
					ColorMask[_ColorMask]

					Pass
					{
						CGPROGRAM
						#pragma vertex vert
						#pragma fragment frag
						#include "UnityCG.cginc"

						struct appdata_t
						{
							float4 vertex   : POSITION;
							float4 color    : COLOR;
							float2 texcoord : TEXCOORD0;
						};

						struct v2f
						{
							float4 vertex   : SV_POSITION;
							fixed4 color : COLOR;
							half2 texcoord  : TEXCOORD0;
						};

						fixed4 _Color;
						fixed4 _Color2;
						float _Angle;
						int _InverseAngle;

						v2f vert(appdata_t IN)
						{
							v2f OUT;
							OUT.vertex = UnityObjectToClipPos(IN.vertex);
							OUT.texcoord = IN.texcoord;
							#ifdef UNITY_HALF_TEXEL_OFFSET
							OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
							#endif
							float angledLerpX = saturate(lerp(IN.texcoord.x, 0, _Angle));
							float angledLerpY = saturate(lerp(0, IN.texcoord.y, _Angle));
							float angledLerp = (angledLerpX + angledLerpY);
							angledLerp = lerp(angledLerp, 1 - angledLerp, _InverseAngle);

							OUT.color = lerp(_Color, _Color2, angledLerp);
							return OUT;
						}

						sampler2D _MainTex;

						fixed4 frag(v2f i) : COLOR{
							fixed4 c = tex2D(_MainTex, i.texcoord) * i.color;
							clip(c.a - 0.01);
							return c;
						}
						ENDCG
					}
		}
			Fallback "Diffuse"
}