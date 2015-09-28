// Shader created with Shader Forge v1.19 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.19;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:9093,x:32719,y:32712,varname:node_9093,prsc:2|emission-6079-RGB;n:type:ShaderForge.SFN_Tex2d,id:6079,x:32449,y:32724,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_6079,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d68be855d3f054747865fc836a001cb5,ntxv:0,isnm:False|UVIN-9957-OUT;n:type:ShaderForge.SFN_TexCoord,id:79,x:31914,y:32681,varname:node_79,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:1130,x:31914,y:32843,varname:node_1130,prsc:2|A-79-V,B-2032-OUT;n:type:ShaderForge.SFN_Append,id:9957,x:32270,y:32712,varname:node_9957,prsc:2|A-79-U,B-2740-OUT;n:type:ShaderForge.SFN_Fmod,id:2740,x:32208,y:32910,varname:node_2740,prsc:2|A-1130-OUT,B-6242-OUT;n:type:ShaderForge.SFN_Vector1,id:6242,x:31914,y:32979,varname:node_6242,prsc:2,v1:1;n:type:ShaderForge.SFN_Time,id:7817,x:31568,y:32898,varname:node_7817,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8647,x:31568,y:32845,ptovrint:False,ptlb:TimeScale,ptin:_TimeScale,varname:node_8647,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:2032,x:31743,y:32868,varname:node_2032,prsc:2|A-8647-OUT,B-7817-T;proporder:6079-8647;pass:END;sub:END;*/

Shader "Custom/ScrollingBackground" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TimeScale ("TimeScale", Float ) = 0.5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _TimeScale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_7817 = _Time + _TimeEditor;
                float2 node_9957 = float2(i.uv0.r,fmod((i.uv0.g+(_TimeScale*node_7817.g)),1.0));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_9957, _MainTex));
                float3 emissive = _MainTex_var.rgb;
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
