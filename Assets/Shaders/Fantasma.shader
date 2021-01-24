Shader "Unlit/Fantasma"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_PlusCor("CorAdicional", Color) = (1,1,1,1)
		_Transparencia("Transparencia", Range(0.0,1)) = 0.50
		_CutOut("Sumir", float) = 1
		_Distancia ("Distancia", Range(0.0,0.25)) = 0.10
		_Velocidade ("Velocidade",float) = 1
		_Quantidade("Quantidade", float) = 1
		_Amplitude("Amplitude", float) = 1
	}
	SubShader
	{
		Tags {"Queue" = "Transparent" "RenderType"="Transparent" }

		LOD 100
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _PlusCor;
			float _Transparencia;
			float _Distancia;
			float _Velocidade;
			float _Quantidade;
			float _Amplitude;
			float _CutOut;
			v2f vert (appdata v)
			{
				v2f o;
				
				v.vertex.y += sin(_Time.y* _Velocidade * v.vertex.y * _Amplitude ) * _Distancia * _Quantidade ; 
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) * _PlusCor;
				col.a = _Transparencia;
				clip(col.g - _CutOut);
				return col;
			}
			ENDCG
		}
	}
}
