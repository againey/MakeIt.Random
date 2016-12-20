Shader "Custom/Histogram Shader"
{
	Properties
	{
		_MainTex ("Sample Count", 2D) = "black" {}
	}
	SubShader
	{
		Pass
		{
			Tags { "RenderType"="Opaque" }
		
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;

			fixed4 frag(v2f_img i) : SV_Target
			{
				float s = tex2D(_MainTex, i.uv).r;
				float c = 1 - step(s, i.uv.y);
				return fixed4(c, c, c, 1);
			}
			ENDCG
		}
	}
}
