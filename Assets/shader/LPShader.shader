Shader "Unlit/LPShader"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_FadeOutDistNear("Near fadeout dist", float) = 50
		//_FadeOutDistFar("Far fadeout dist", float) = 10000
		_Multiplier("Multiplier", float) = 1
		_ContractionAmount("Near contraction amount", float) = 5
    }
    SubShader
    {
        Tags { "Queue"="Transparent""RenderType"="Transparent" }
        LOD 100

        Pass
        {
			/*Tags { "LightMode" = "ForwardBase" }

			ZWrite Off*/
			Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

			//sampler2D _MainTex;

			float _FadeOutDistNear;
			float _FadeOutDistFar;
			float _Multiplier;
			float _ContractionAmount;
			fixed4 _Color;

			struct v2f {
				float4	pos	: SV_POSITION;
				float2	uv	: TEXCOORD0;
				fixed4	color : TEXCOORD1;
			};

            v2f vert (appdata_full v)
            {
                v2f o;
				float3 viewPos = mul(UNITY_MATRIX_MV, v.vertex);
				float dist = length(viewPos);
				float nfadeout = saturate(dist / _FadeOutDistNear);

				nfadeout *= nfadeout;
				nfadeout *= nfadeout;
				o.color.rgb = nfadeout * v.color.rgb * _Multiplier;
				o.color.a = nfadeout * 0.7 * v.color.a;
                o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy;
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //return tex2D(_MainTex, i.uv.xy)*i.color;
				return i.color * _Color;
            }
            ENDCG
        }
    }
}
