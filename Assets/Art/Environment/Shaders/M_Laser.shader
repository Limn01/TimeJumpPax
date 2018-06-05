// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "M_Laser"
{
	Properties
	{
		_Emissive("Emissive", 2D) = "white" {}
		_EmissivePower("Emissive Power", Float) = 2
		_Speed("Speed", Vector) = (0.1,0,0,0)
		_T_Laser_pan_rotated("T_Laser_pan_rotated", 2D) = "white" {}
		_PanStrength("Pan Strength", Float) = 0
		_EmissiveDistanceMultiplier("Emissive Distance Multiplier", Float) = 0
		_Falloff("Falloff", Float) = 3
		_EmissiveDistance("Emissive Distance", Float) = 10
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Overlay"  "Queue" = "Overlay+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float customSurfaceDepth30;
		};

		uniform sampler2D _Emissive;
		uniform float4 _Emissive_ST;
		uniform sampler2D _T_Laser_pan_rotated;
		uniform float2 _Speed;
		uniform float _PanStrength;
		uniform float _EmissivePower;
		uniform float _EmissiveDistanceMultiplier;
		uniform float _Falloff;
		uniform float _EmissiveDistance;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 customSurfaceDepth30 = ase_vertex3Pos;
			o.customSurfaceDepth30 = -UnityObjectToViewPos( customSurfaceDepth30 ).z;
		}

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Emissive = i.uv_texcoord * _Emissive_ST.xy + _Emissive_ST.zw;
			float mulTime21 = _Time.y * 1;
			float2 panner1 = ( uv_Emissive + mulTime21 * _Speed);
			float4 lerpResult24 = lerp( tex2D( _Emissive, uv_Emissive ) , tex2D( _T_Laser_pan_rotated, panner1 ) , _PanStrength);
			float cameraDepthFade30 = (( i.customSurfaceDepth30 -_ProjectionParams.y - _EmissiveDistance ) / _Falloff);
			float clampResult39 = clamp( cameraDepthFade30 , 0 , 1 );
			float lerpResult26 = lerp( _EmissivePower , ( _EmissivePower + _EmissiveDistanceMultiplier ) , clampResult39);
			o.Emission = ( lerpResult24 * lerpResult26 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15201
1927;53;1666;974;2642.804;1088.791;1.994073;True;True
Node;AmplifyShaderEditor.PosVertexDataNode;35;-1021.402,228.5429;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;32;-865.402,510.5429;Float;False;Property;_EmissiveDistance;Emissive Distance;8;0;Create;True;0;0;False;0;10;157.44;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-861.402,412.5429;Float;False;Property;_Falloff;Falloff;7;0;Create;True;0;0;False;0;3;1615.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-1003.402,87.54291;Float;False;Property;_EmissiveDistanceMultiplier;Emissive Distance Multiplier;6;0;Create;True;0;0;False;0;0;30;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CameraDepthFade;30;-605.402,253.5429;Float;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-753.7699,-117.3516;Float;False;Property;_EmissivePower;Emissive Power;2;0;Create;True;0;0;False;0;2;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;39;-321.4019,260.5429;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;21;-1722.706,77.78021;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-555.402,38.54291;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;19;-1726.971,-86.86758;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0.1,0;-1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-1852.971,-254.8676;Float;False;0;2;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;26;-314.7966,-110.1392;Float;False;3;0;FLOAT;0;False;1;FLOAT;20;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;1;-1498.093,-185.2506;Float;True;3;0;FLOAT2;10,10;False;2;FLOAT2;1,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-1109.693,-432.4507;Float;True;Property;_Emissive;Emissive;1;0;Create;True;0;0;False;0;9435828bf74190f43b48761503a03147;9435828bf74190f43b48761503a03147;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;23;-1112.306,-213.4198;Float;True;Property;_T_Laser_pan_rotated;T_Laser_pan_rotated;4;0;Create;True;0;0;False;0;a68fd8e7d6d74ef4dbcde9f3ad2f8fa4;a68fd8e7d6d74ef4dbcde9f3ad2f8fa4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;25;-746.4876,-213.2599;Float;False;Property;_PanStrength;Pan Strength;5;0;Create;True;0;0;False;0;0;0.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;37;-59.40198,-143.4571;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;24;-656.2309,-430.5656;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;38;-445.402,-247.4571;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-439.6927,-430.0507;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;90.8,-479.6;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;M_Laser;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Custom;0.5;True;True;0;True;Overlay;;Overlay;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;30;2;35;0
WireConnection;30;0;34;0
WireConnection;30;1;32;0
WireConnection;39;0;30;0
WireConnection;36;0;8;0
WireConnection;36;1;29;0
WireConnection;26;0;8;0
WireConnection;26;1;36;0
WireConnection;26;2;39;0
WireConnection;1;0;17;0
WireConnection;1;2;19;0
WireConnection;1;1;21;0
WireConnection;23;1;1;0
WireConnection;37;0;26;0
WireConnection;24;0;2;0
WireConnection;24;1;23;0
WireConnection;24;2;25;0
WireConnection;38;0;37;0
WireConnection;10;0;24;0
WireConnection;10;1;38;0
WireConnection;0;2;10;0
ASEEND*/
//CHKSM=68311AF82D29C23AB80F19A80666A3827CCC8E5C