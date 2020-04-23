// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:6199,x:33195,y:32598,varname:node_6199,prsc:2|emission-5230-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:8194,x:32202,y:32526,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,varname:node_8194,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5484,x:32376,y:32516,varname:node_5484,prsc:2,ntxv:0,isnm:False|TEX-8194-TEX;n:type:ShaderForge.SFN_VertexColor,id:8955,x:32376,y:32701,varname:node_8955,prsc:2;n:type:ShaderForge.SFN_Fresnel,id:2068,x:32413,y:32983,varname:node_2068,prsc:2|EXP-828-OUT;n:type:ShaderForge.SFN_ValueProperty,id:828,x:32237,y:33025,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_828,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Power,id:6580,x:32591,y:32921,varname:node_6580,prsc:2|VAL-2068-OUT,EXP-8624-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8624,x:32413,y:32891,ptovrint:False,ptlb:FresnelPower,ptin:_FresnelPower,varname:node_8624,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:5230,x:32965,y:32673,varname:node_5230,prsc:2|A-4746-RGB,B-4009-OUT;n:type:ShaderForge.SFN_Multiply,id:8503,x:32793,y:32921,varname:node_8503,prsc:2|A-8955-RGB,B-6580-OUT;n:type:ShaderForge.SFN_Color,id:4746,x:32633,y:32490,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4746,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:4009,x:33011,y:32979,varname:node_4009,prsc:2|A-8503-OUT,B-1528-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1528,x:32815,y:33099,ptovrint:False,ptlb:FrenelIntensity,ptin:_FrenelIntensity,varname:node_1528,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.5;proporder:8194-828-8624-4746-1528;pass:END;sub:END;*/

Shader "Custom/FresnelOpaque" {
    Properties {
        _MainTexture ("MainTexture", 2D) = "white" {}
        _Fresnel ("Fresnel", Float ) = 1
        _FresnelPower ("FresnelPower", Float ) = 1
        _Color ("Color", Color) = (0,0,0,1)
        _FrenelIntensity ("FrenelIntensity", Float ) = 1.5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Fresnel;
            uniform float _FresnelPower;
            uniform float4 _Color;
            uniform float _FrenelIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 emissive = (_Color.rgb+((i.vertexColor.rgb*pow(pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel),_FresnelPower))*_FrenelIntensity));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
