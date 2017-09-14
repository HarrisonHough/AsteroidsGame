// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Amplify/Toon Ramp Specular"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_ColorTint("Color Tint", Color) = (0,0,0,0)
		_Diffuse("Diffuse", 2D) = "white" {}
		_DiffuseRamp("Diffuse Ramp", 2D) = "white" {}
		_SpecularRamp("Specular Ramp", 2D) = "white" {}
		_Gloss("Gloss", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityCG.cginc"
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
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldPos;
			float3 viewDir;
		};

		uniform half4 _ColorTint;
		uniform sampler2D _Diffuse;
		uniform float4 _Diffuse_ST;
		uniform sampler2D _DiffuseRamp;
		uniform sampler2D _SpecularRamp;
		uniform float _Gloss;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Diffuse = i.uv_texcoord * _Diffuse_ST.xy + _Diffuse_ST.zw;
			float3 worldNormal = i.worldNormal;
			float3 temp_output_21_0 = UnityWorldSpaceLightDir( i.worldPos );
			float temp_output_24_0 = ( 0.5 + ( dot( worldNormal , temp_output_21_0 ) * 0.5 ) );
			float4 appendResult19 = float4( temp_output_24_0 , temp_output_24_0 , 0 , 0 );
			float4 tex2DNode18 = tex2D( _DiffuseRamp,appendResult19.xy);
			float temp_output_36_0 = pow( dot( worldNormal , normalize( ( temp_output_21_0 + i.viewDir ) ) ) , ( _Gloss * 80.0 ) );
			float4 appendResult15 = float4( temp_output_36_0 , temp_output_36_0 , 0 , 0 );
			o.Albedo = ( ( ( _ColorTint * tex2D( _Diffuse,uv_Diffuse) ) * tex2DNode18 ) + saturate( ( tex2D( _SpecularRamp,appendResult15.xy) * temp_output_36_0 ) ) ).rgb;
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
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 )
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
				surfIN.viewDir = worldViewDir;
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
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
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=6001
7;29;1906;1004;2409.301;1962.107;2.87234;True;True
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;26;-1009.455,148.1878;Float;False;World;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;21;-1066.872,39.4945;Float;False;0;FLOAT;0.0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-722.6353,153.5023;Float;False;0;FLOAT3;0,0,0;False;1;FLOAT3;0.0;False;FLOAT3
Node;AmplifyShaderEditor.NormalizeNode;30;-542.2358,158.9022;Float;False;0;FLOAT3;0,0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.RangedFloatNode;32;-876.1057,355.5616;Float;False;Property;_Gloss;Gloss;4;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;42;-634.812,530.0635;Fixed;False;Constant;_Float3;Float 3;2;0;80;0;0;FLOAT
Node;AmplifyShaderEditor.WorldNormalVector;25;-715.0773,-535.5828;Float;False;0;FLOAT3;0,0,0;False;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.DotProductOpNode;9;-609.8161,-232.4832;Float;False;0;FLOAT3;0.0;False;1;FLOAT3;0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-402.5134,390.8638;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.DotProductOpNode;14;-295.9948,113.7976;Float;False;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;23;-638.8315,8.511444;Fixed;False;Constant;_Float0;Float 0;2;0;0.5;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-421.3763,-167.2247;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.PowerNode;36;-162.4671,282.426;Float;False;0;FLOAT;0,0,0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-254.435,-162.4672;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.AppendNode;15;-79.80552,63.62337;Float;False;FLOAT4;0;0;0;0;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;FLOAT4
Node;AmplifyShaderEditor.AppendNode;19;-57.52714,-255.5871;Float;False;FLOAT4;0;0;0;0;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;FLOAT4
Node;AmplifyShaderEditor.SamplerNode;17;187.2006,-35.52123;Float;True;Property;_SpecularRamp;Specular Ramp;3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;50;384.1355,-1060.377;Half;False;Property;_ColorTint;Color Tint;0;0;0,0,0,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;48;65.51643,-626.6913;Float;True;Property;_Diffuse;Diffuse;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;549.7882,9.463778;Float;False;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.SamplerNode;18;291.1134,-362.3445;Float;True;Property;_DiffuseRamp;Diffuse Ramp;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;870.3546,-672.6798;Float;False;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;958.4563,-453.9344;Float;False;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.SaturateNode;47;770.4943,-19.54153;Float;False;0;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.NormalVertexDataNode;11;-1030.273,-302.0663;Float;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;27;1145.217,-264.1129;Float;False;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.ObjSpaceLightDirHlpNode;10;-1072.851,-46.6854;Float;False;0;FLOAT;0.0;False;FLOAT3
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1416.469,-342.866;Float;False;True;2;Float;ASEMaterialInspector;Standard;Custom/Amplify/Toon Ramp Specular;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;31;0;21;0
WireConnection;31;1;26;0
WireConnection;30;0;31;0
WireConnection;9;0;25;0
WireConnection;9;1;21;0
WireConnection;44;0;32;0
WireConnection;44;1;42;0
WireConnection;14;0;25;0
WireConnection;14;1;30;0
WireConnection;22;0;9;0
WireConnection;22;1;23;0
WireConnection;36;0;14;0
WireConnection;36;1;44;0
WireConnection;24;0;23;0
WireConnection;24;1;22;0
WireConnection;15;0;36;0
WireConnection;15;1;36;0
WireConnection;19;0;24;0
WireConnection;19;1;24;0
WireConnection;17;1;15;0
WireConnection;39;0;17;0
WireConnection;39;1;36;0
WireConnection;18;1;19;0
WireConnection;49;0;50;0
WireConnection;49;1;48;0
WireConnection;52;0;49;0
WireConnection;52;1;18;0
WireConnection;47;0;39;0
WireConnection;27;0;52;0
WireConnection;27;1;47;0
WireConnection;0;0;27;0
ASEEND*/
//CHKSM=A4B2EDE615D983DCF8DF88ED0D25C6194A1196B1