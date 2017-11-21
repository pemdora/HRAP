// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AVP/Velvet"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_AlbedoColor("Albedo Color", Color) = (1,1,1,1)
		_Albedo("Albedo", 2D) = "white" {}
		_Metallic("Metallic", 2D) = "black" {}
		_Normals("Normals", 2D) = "bump" {}
		_NormalPower("Normal Power", Range( 0 , 4)) = 0
		_Occlusion("Occlusion", 2D) = "white" {}
		_OcclusionPower("Occlusion Power", Range( 0 , 1)) = 1
		_RimColor("RimColor", Color) = (0,0,0,0)
		_RimPower("RimPower", Range( 0 , 10)) = 0
		_Noise("Noise", 2D) = "white" {}
		_Emission("Emission", 2D) = "black" {}
		_Emission_Power("Emission_Power", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		ZTest LEqual
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 viewDir;
			INTERNAL_DATA
		};

		uniform fixed _NormalPower;
		uniform sampler2D _Normals;
		uniform float4 _Normals_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform fixed4 _AlbedoColor;
		uniform fixed _RimPower;
		uniform fixed4 _RimColor;
		uniform sampler2D _Noise;
		uniform float4 _Noise_ST;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform fixed _Emission_Power;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform sampler2D _Occlusion;
		uniform float4 _Occlusion_ST;
		uniform fixed _OcclusionPower;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normals = i.uv_texcoord * _Normals_ST.xy + _Normals_ST.zw;
			fixed3 tex2DNode3 = UnpackScaleNormal( tex2D( _Normals,uv_Normals) ,_NormalPower );
			o.Normal = tex2DNode3;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float2 uv_Noise = i.uv_texcoord * _Noise_ST.xy + _Noise_ST.zw;
			o.Albedo = ( ( tex2D( _Albedo,uv_Albedo) * _AlbedoColor ) + ( ( pow( ( 1.0 - saturate( dot( tex2DNode3 , normalize( i.viewDir ) ) ) ) , _RimPower ) * _RimColor ) * tex2D( _Noise,uv_Noise) ) ).rgb;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			o.Emission = lerp( fixed4(0,0,0,0) , tex2D( _Emission,uv_Emission) , _Emission_Power ).rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			fixed4 tex2DNode2 = tex2D( _Metallic,uv_Metallic);
			o.Metallic = tex2DNode2.x;
			o.Smoothness = tex2DNode2.a;
			float2 uv_Occlusion = i.uv_texcoord * _Occlusion_ST.xy + _Occlusion_ST.zw;
			o.Occlusion = lerp( fixed4(1,1,1,0) , tex2D( _Occlusion,uv_Occlusion) , _OcclusionPower ).x;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD6;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				float4 texcoords01 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.texcoords01.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = IN.tSpace0.xyz * worldViewDir.x + IN.tSpace1.xyz * worldViewDir.y + IN.tSpace2.xyz * worldViewDir.z;
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=7003
-1673;29;1666;974;952.9553;589.2944;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;51;-2192.446,8.454414;Float;False;Property;_NormalPower;Normal Power;4;0;0;0;4;FLOAT
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;22;-1869.102,246.4996;Float;False;Tangent;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;3;-1875.501,-39.00019;Float;True;Property;_Normals;Normals;3;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;0,0;False;1;FLOAT2;1.0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.NormalizeNode;23;-1674.703,272.1001;Float;False;0;FLOAT3;0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.DotProductOpNode;21;-1483.003,193.6997;Float;False;0;FLOAT3;0.0,0,0;False;1;FLOAT3;0.0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SaturateNode;20;-1307.804,169.1996;Float;False;0;FLOAT;1.23;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;28;-1235.402,326.8994;Float;False;Property;_RimPower;RimPower;8;0;0;0;10;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;5;-1138.801,211.4989;Float;False;0;FLOAT;0;False;FLOAT
Node;AmplifyShaderEditor.ColorNode;25;-952.2028,414.6985;Float;False;Property;_RimColor;RimColor;7;0;0,0,0,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.PowerNode;26;-946.0033,234.8994;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.ColorNode;41;-464.7307,-91.04295;Float;False;Property;_AlbedoColor;Albedo Color;0;0;1,1,1,1;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-543.2214,-303.3404;Float;True;Property;_Albedo;Albedo;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;38;-724.3411,554.2241;Float;True;Property;_Noise;Noise;9;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-703.603,250.0991;Float;False;0;FLOAT;0;False;1;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-194.6758,-187.9299;Float;False;0;FLOAT4;0.0;False;1;COLOR;0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;62;143.6449,1389.406;Float;False;Property;_Emission_Power;Emission_Power;12;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.SamplerNode;4;-208.9012,673.0024;Float;True;Property;_Occlusion;Occlusion;5;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;60;-156.655,1386.105;Float;False;Constant;_Color1;Color 1;12;0;0,0,0,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;58;-235.155,1190.106;Float;True;Property;_Emission;Emission;11;0;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;57;-126.0552,860.2061;Float;False;Constant;_Color0;Color 0;11;0;1,1,1,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-410.2298,312.0233;Float;False;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;54;-192.0552,1041.206;Float;False;Property;_OcclusionPower;Occlusion Power;6;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;2;-218.5002,369.4001;Float;True;Property;_Metallic;Metallic;2;0;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;56;143.9448,805.2061;Float;False;0;COLOR;0.0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0.0;False;FLOAT4
Node;AmplifyShaderEditor.SimpleAddOpNode;39;95.1964,6.456824;Float;False;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.LerpOp;61;154.6449,1197.005;Float;False;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;147.2,339.6;Fixed;False;True;2;Fixed;;0;Standard;AVP/Velvet;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;3;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0;False;2;FLOAT3;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0;False;10;OBJECT;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;3;5;51;0
WireConnection;23;0;22;0
WireConnection;21;0;3;0
WireConnection;21;1;23;0
WireConnection;20;0;21;0
WireConnection;5;0;20;0
WireConnection;26;0;5;0
WireConnection;26;1;28;0
WireConnection;27;0;26;0
WireConnection;27;1;25;0
WireConnection;40;0;1;0
WireConnection;40;1;41;0
WireConnection;37;0;27;0
WireConnection;37;1;38;0
WireConnection;56;0;57;0
WireConnection;56;1;4;0
WireConnection;56;2;54;0
WireConnection;39;0;40;0
WireConnection;39;1;37;0
WireConnection;61;0;60;0
WireConnection;61;1;58;0
WireConnection;61;2;62;0
WireConnection;0;0;39;0
WireConnection;0;1;3;0
WireConnection;0;2;61;0
WireConnection;0;3;2;0
WireConnection;0;4;2;4
WireConnection;0;5;56;0
ASEEND*/
//CHKSM=86EA1FC3203462795C8225B0BF49CE9299A90C76