Shader "Hidden/Pixelate"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float2 Density;
			float2 Size;

			fixed4 frag (v2f_img i) : SV_Target
			{
				float2 pixelPos = floor(i.uv * Density);
				float2 pixelCentre = pixelPos * Size + Size * 0.5;

				float4 tex = tex2D(_MainTex, pixelCentre);

				return tex;
			}

			ENDCG
		}
	}
}