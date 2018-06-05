// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "M_Waterflow_Opaque"
{
	Properties
	{
		_Normal("Normal", 2D) = "bump" {}
		_FlowDirection("Flow Direction", Vector) = (0.1,0,0,0)
		_Metallic("Metallic", Float) = 0.2
		_Smoothness("Smoothness", Range( 0 , 1)) = 1
		_SceneCubemap("Scene Cubemap", CUBE) = "white" {}
		_CubemapStrength("Cubemap Strength", Float) = 0.4
		_Color1("Color 1", Color) = (0.2465398,0.566639,0.8823529,0)
		_Diffuse("Diffuse", 2D) = "white" {}
		_DiffuseStrength("Diffuse Strength", Float) = 0
		_T_Lava_emission("T_Lava_emission", 2D) = "white" {}
		_EmissiveStrength("Emissive Strength", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha 
		struct Input
		{
			half2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _Normal;
		uniform half2 _FlowDirection;
		uniform float4 _Normal_ST;
		uniform half4 _Color1;
		uniform sampler2D _Diffuse;
		uniform half _DiffuseStrength;
		uniform sampler2D _T_Lava_emission;
		uniform half _EmissiveStrength;
		uniform samplerCUBE _SceneCubemap;
		uniform half _CubemapStrength;
		uniform half _Metallic;
		uniform half _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime6 = _Time.y * 1;
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float2 panner7 = ( uv_Normal + mulTime6 * _FlowDirection);
			half3 tex2DNode2 = UnpackScaleNormal( tex2D( _Normal, panner7 ) ,0.5 );
			o.Normal = tex2DNode2;
			float4 lerpResult28 = lerp( _Color1 , tex2D( _Diffuse, panner7 ) , _DiffuseStrength);
			o.Albedo = lerpResult28.rgb;
			float3 ase_worldPos = i.worldPos;
			half3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float4 lerpResult10 = lerp( ( tex2D( _T_Lava_emission, panner7 ) * _EmissiveStrength ) , texCUBE( _SceneCubemap, reflect( -ase_worldViewDir , normalize( WorldNormalVector( i , tex2DNode2 ) ) ) ) , _CubemapStrength);
			o.Emission = lerpResult10.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15201
2113;33;1666;974;1592.566;368.3617;1.802027;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-1291.786,-71.56621;Float;False;0;2;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;6;-1161.521,261.0816;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;4;-1227.786,96.43382;Float;False;Property;_FlowDirection;Flow Direction;1;0;Create;True;0;0;False;0;0.1,0;-0.06,0.01;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;7;-936.908,-1.949203;Float;True;3;0;FLOAT2;10,10;False;2;FLOAT2;1,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-558.3617,51.61143;Float;True;Property;_Normal;Normal;0;0;Create;True;0;0;False;0;dd2fd2df93418444c8e280f1d34deeb5;dd2fd2df93418444c8e280f1d34deeb5;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.5;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;17;-391.2748,460.9607;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode;16;-1475.58,515.9986;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode;35;-728.0815,199.5598;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;20;-1540.59,793.5276;Float;False;World;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NegateNode;21;-1354.59,797.5276;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode;34;-1149.162,589.887;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WorldNormalVector;23;-1544.527,969.8511;Float;True;True;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;31;-960.9991,625.072;Float;True;Property;_T_Lava_emission;T_Lava_emission;10;0;Create;True;0;0;False;0;35a597a98320f2d4c9e8eb4f356abe66;29bf3af7b738062459071a37242044e8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ReflectOpNode;19;-944.3856,946.1014;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-883.8843,842.3284;Float;False;Property;_EmissiveStrength;Emissive Strength;11;0;Create;True;0;0;False;0;1;1.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-723.6457,934.8937;Float;True;Property;_SceneCubemap;Scene Cubemap;4;0;Create;True;0;0;False;0;56a68e301a0ff55469ae441c0112d256;4ba2ce13f84c0784b902a14c1e873954;True;0;False;white;Auto;False;Object;-1;Auto;Cube;6;0;SAMPLER2D;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-657.2967,1145.04;Float;False;Property;_CubemapStrength;Cubemap Strength;5;0;Create;True;0;0;False;0;0.4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-263.4924,-72.63696;Float;False;Property;_DiffuseStrength;Diffuse Strength;9;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-564.4234,627.7999;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;29;-375.4924,-294.637;Float;True;Property;_Diffuse;Diffuse;8;0;Create;True;0;0;False;0;29bf3af7b738062459071a37242044e8;29bf3af7b738062459071a37242044e8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;25;-313.2544,-513.7453;Float;False;Property;_Color1;Color 1;6;0;Create;True;0;0;False;0;0.2465398,0.566639,0.8823529,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;41;-234.1718,1018.588;Float;False;Property;_EdgeColor;Edge Color;12;0;Create;True;0;0;False;0;1,0.9724138,0,0;1,0.6,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;47;-254.4027,789.0025;Float;True;Property;_SmoothNoise;SmoothNoise;13;0;Create;True;0;0;False;0;f7e96904e8667e1439548f0f86389447;29bf3af7b738062459071a37242044e8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;28;214.5076,-514.637;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;114.707,878.5643;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;9;517.3443,241.5141;Float;False;Property;_Metallic;Metallic;2;0;Create;True;0;0;False;0;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;10;-263.9012,621.1265;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;50;605.5006,-365.2234;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;8;392.4583,315.9854;Float;False;Property;_Smoothness;Smoothness;3;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-843.5658,1279.959;Float;False;Property;_EdgeDistance;Edge Distance;7;0;Create;True;0;0;False;0;0.1529412;4.47;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;38;-562.9219,1286.204;Float;False;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;43;-76.33495,1285.393;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;40;592.4539,1218.945;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;42;111.665,1288.393;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1034.478,44.23216;Half;False;True;2;Half;ASEMaterialInspector;0;0;Standard;M_Waterflow_Opaque;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Translucent;0.5;True;False;0;False;Opaque;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;5;0
WireConnection;7;2;4;0
WireConnection;7;1;6;0
WireConnection;2;1;7;0
WireConnection;17;0;2;0
WireConnection;16;0;17;0
WireConnection;35;0;7;0
WireConnection;21;0;20;0
WireConnection;34;0;35;0
WireConnection;23;0;16;0
WireConnection;31;1;34;0
WireConnection;19;0;21;0
WireConnection;19;1;23;0
WireConnection;11;1;19;0
WireConnection;32;0;31;0
WireConnection;32;1;33;0
WireConnection;29;1;7;0
WireConnection;28;0;25;0
WireConnection;28;1;29;0
WireConnection;28;2;30;0
WireConnection;51;0;47;0
WireConnection;51;1;41;0
WireConnection;10;0;32;0
WireConnection;10;1;11;0
WireConnection;10;2;13;0
WireConnection;50;0;28;0
WireConnection;50;1;51;0
WireConnection;50;2;42;0
WireConnection;38;0;37;0
WireConnection;43;0;38;0
WireConnection;40;0;10;0
WireConnection;40;1;51;0
WireConnection;40;2;42;0
WireConnection;42;0;43;0
WireConnection;0;0;28;0
WireConnection;0;1;2;0
WireConnection;0;2;10;0
WireConnection;0;3;9;0
WireConnection;0;4;8;0
ASEEND*/
//CHKSM=EA521F6CBC8F8BF002BE1FF7C07CEC90D7AC4394