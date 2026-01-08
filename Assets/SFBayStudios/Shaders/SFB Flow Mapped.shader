// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:Standard,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:33003,y:33180,varname:node_2865,prsc:2|diff-6343-OUT,spec-4652-OUT,gloss-220-OUT,normal-475-OUT,emission-6902-OUT,difocc-3424-OUT,alpha-6462-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:32298,y:32819,varname:node_6343,prsc:2|A-4543-OUT,B-6665-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:32091,y:32724,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:358,x:31421,y:32191,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:_Metallic,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:31411,y:32308,ptovrint:False,ptlb:Roughness,ptin:_Roughness,varname:_Gloss,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8,max:1;n:type:ShaderForge.SFN_Tex2dAsset,id:4037,x:31411,y:32420,ptovrint:False,ptlb:AlbedoOpacity1,ptin:_AlbedoOpacity1,varname:_AlbedoOpacity1,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8bd6005f9bdc84c0c9feac733f094b02,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8407,x:31669,y:32566,varname:AlbedoOpacity1_1,prsc:2,tex:8bd6005f9bdc84c0c9feac733f094b02,ntxv:0,isnm:False|UVIN-3768-OUT,TEX-4037-TEX;n:type:ShaderForge.SFN_Time,id:1664,x:30351,y:33394,varname:node_1664,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:9628,x:30351,y:33621,ptovrint:False,ptlb:Flow Speed,ptin:_FlowSpeed,varname:_FlowSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:1977,x:30568,y:33453,varname:node_1977,prsc:2|A-1664-TSL,B-9628-OUT;n:type:ShaderForge.SFN_Add,id:9866,x:30846,y:33511,varname:node_9866,prsc:2|A-1977-OUT,B-5965-OUT;n:type:ShaderForge.SFN_Frac,id:9963,x:30779,y:33340,varname:node_9963,prsc:2|IN-1977-OUT;n:type:ShaderForge.SFN_Frac,id:4067,x:31020,y:33511,varname:node_4067,prsc:2|IN-9866-OUT;n:type:ShaderForge.SFN_TexCoord,id:2320,x:30422,y:33089,varname:node_2320,prsc:2,uv:2;n:type:ShaderForge.SFN_RemapRange,id:6904,x:30803,y:33089,varname:node_6904,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-4795-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4795,x:30588,y:33089,varname:node_4795,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2320-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:6928,x:30588,y:33000,ptovrint:False,ptlb:Flow Power,ptin:_FlowPower,varname:_FlowPower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:6845,x:30803,y:32945,varname:node_6845,prsc:2|A-3787-OUT,B-6928-OUT;n:type:ShaderForge.SFN_Multiply,id:3357,x:30982,y:33070,varname:node_3357,prsc:2|A-6845-OUT,B-6904-OUT;n:type:ShaderForge.SFN_Multiply,id:7702,x:31192,y:33208,varname:node_7702,prsc:2|A-3357-OUT,B-4067-OUT;n:type:ShaderForge.SFN_Multiply,id:385,x:31192,y:33070,varname:node_385,prsc:2|A-3357-OUT,B-9963-OUT;n:type:ShaderForge.SFN_TexCoord,id:5624,x:30939,y:32695,varname:node_5624,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:3768,x:31439,y:33045,varname:node_3768,prsc:2|A-5624-UVOUT,B-385-OUT;n:type:ShaderForge.SFN_Add,id:6669,x:31439,y:33208,varname:node_6669,prsc:2|A-5624-UVOUT,B-7702-OUT;n:type:ShaderForge.SFN_Tex2d,id:6246,x:31707,y:32743,varname:AlbedoOpacity1_2,prsc:2,tex:8bd6005f9bdc84c0c9feac733f094b02,ntxv:0,isnm:False|UVIN-6669-OUT,TEX-4037-TEX;n:type:ShaderForge.SFN_Lerp,id:4543,x:32091,y:32868,varname:node_4543,prsc:2|A-8407-RGB,B-6246-RGB,T-7750-OUT;n:type:ShaderForge.SFN_Subtract,id:1172,x:30681,y:33718,varname:node_1172,prsc:2|A-8239-OUT,B-9963-OUT;n:type:ShaderForge.SFN_Divide,id:1807,x:30848,y:33718,varname:node_1807,prsc:2|A-1172-OUT,B-8239-OUT;n:type:ShaderForge.SFN_Abs,id:7750,x:31005,y:33732,varname:node_7750,prsc:2|IN-1807-OUT;n:type:ShaderForge.SFN_Vector1,id:8239,x:30390,y:33774,varname:node_8239,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:3787,x:30588,y:32918,varname:node_3787,prsc:2,v1:-1;n:type:ShaderForge.SFN_Vector1,id:5965,x:30640,y:33619,varname:node_5965,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2dAsset,id:9666,x:31411,y:32599,ptovrint:False,ptlb:Normal Map 1,ptin:_NormalMap1,varname:node_9666,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:3917,x:31726,y:32919,varname:node_3917,prsc:2,ntxv:0,isnm:False|UVIN-3768-OUT,TEX-9666-TEX;n:type:ShaderForge.SFN_Tex2d,id:8189,x:31726,y:33058,varname:node_8189,prsc:2,ntxv:0,isnm:False|UVIN-6669-OUT,TEX-9666-TEX;n:type:ShaderForge.SFN_Lerp,id:475,x:32091,y:33015,varname:node_475,prsc:2|A-3917-RGB,B-8189-RGB,T-7750-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9187,x:31393,y:32789,ptovrint:False,ptlb:MetalAOHeightRough,ptin:_MetalAOHeightRough,varname:node_9187,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3576,x:31726,y:33219,varname:node_3576,prsc:2,ntxv:0,isnm:False|TEX-9187-TEX;n:type:ShaderForge.SFN_Tex2d,id:9816,x:31763,y:33385,varname:node_9816,prsc:2,ntxv:0,isnm:False|TEX-9187-TEX;n:type:ShaderForge.SFN_Lerp,id:9234,x:32091,y:33162,varname:node_9234,prsc:2|A-3576-R,B-9816-R,T-7750-OUT;n:type:ShaderForge.SFN_Lerp,id:3424,x:32091,y:33287,varname:node_3424,prsc:2|A-3576-G,B-9816-G,T-7750-OUT;n:type:ShaderForge.SFN_Lerp,id:1469,x:32091,y:33415,varname:node_1469,prsc:2|A-3576-B,B-9816-B,T-7750-OUT;n:type:ShaderForge.SFN_Lerp,id:1674,x:32091,y:33550,varname:node_1674,prsc:2|A-3576-A,B-9816-A,T-7750-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:4652,x:32303,y:33162,ptovrint:False,ptlb:Metallic Map 1?,ptin:_MetallicMap1,varname:node_4652,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-358-OUT,B-9234-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:220,x:32304,y:33550,ptovrint:False,ptlb:Roughness Map 1?,ptin:_RoughnessMap1,varname:node_220,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-5422-OUT,B-1674-OUT;n:type:ShaderForge.SFN_OneMinus,id:5422,x:31771,y:32288,varname:node_5422,prsc:2|IN-1813-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:5582,x:31454,y:33886,ptovrint:False,ptlb:Emissive 1,ptin:_Emissive1,varname:node_5582,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:968,x:31740,y:33547,varname:node_968,prsc:2,ntxv:0,isnm:False|UVIN-3768-OUT,TEX-5582-TEX;n:type:ShaderForge.SFN_Tex2d,id:5676,x:31740,y:33688,varname:node_5676,prsc:2,ntxv:0,isnm:False|UVIN-6669-OUT,TEX-5582-TEX;n:type:ShaderForge.SFN_Lerp,id:6895,x:32095,y:33767,varname:node_6895,prsc:2|A-968-RGB,B-5676-RGB,T-7750-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:6902,x:32217,y:33948,ptovrint:False,ptlb:Emissive Map 1?,ptin:_EmissiveMap1,varname:node_6902,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-5623-RGB,B-6895-OUT;n:type:ShaderForge.SFN_Color,id:5623,x:31815,y:34190,ptovrint:False,ptlb:Emissive Color 1,ptin:_EmissiveColor1,varname:node_5623,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:6462,x:32091,y:32564,varname:node_6462,prsc:2|A-8407-A,B-6246-A,T-7750-OUT;proporder:4037-6665-9666-9187-4652-220-358-1813-5582-6902-5623-9628-6928;pass:END;sub:END;*/

Shader "SFBayStudios/SFB Flow Mapped" {
    Properties {
        _AlbedoOpacity1 ("AlbedoOpacity1", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _NormalMap1 ("Normal Map 1", 2D) = "bump" {}
        _MetalAOHeightRough ("MetalAOHeightRough", 2D) = "white" {}
        [MaterialToggle] _MetallicMap1 ("Metallic Map 1?", Float ) = 0
        [MaterialToggle] _RoughnessMap1 ("Roughness Map 1?", Float ) = 0.2
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Roughness ("Roughness", Range(0, 1)) = 0.8
        _Emissive1 ("Emissive 1", 2D) = "black" {}
        [MaterialToggle] _EmissiveMap1 ("Emissive Map 1?", Float ) = 0
        _EmissiveColor1 ("Emissive Color 1", Color) = (0,0,0,1)
        _FlowSpeed ("Flow Speed", Float ) = 1
        _FlowPower ("Flow Power", Float ) = 0.5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _Metallic;
            uniform float _Roughness;
            uniform sampler2D _AlbedoOpacity1; uniform float4 _AlbedoOpacity1_ST;
            uniform float _FlowSpeed;
            uniform float _FlowPower;
            uniform sampler2D _NormalMap1; uniform float4 _NormalMap1_ST;
            uniform sampler2D _MetalAOHeightRough; uniform float4 _MetalAOHeightRough_ST;
            uniform fixed _MetallicMap1;
            uniform fixed _RoughnessMap1;
            uniform sampler2D _Emissive1; uniform float4 _Emissive1_ST;
            uniform fixed _EmissiveMap1;
            uniform float4 _EmissiveColor1;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                UNITY_FOG_COORDS(7)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD8;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_3357 = (((-1.0)*_FlowPower)*(i.uv2.rg*1.0+-0.5));
                float4 node_1664 = _Time + _TimeEditor;
                float node_1977 = (node_1664.r*_FlowSpeed);
                float node_9963 = frac(node_1977);
                float2 node_3768 = (i.uv0+(node_3357*node_9963));
                float3 node_3917 = UnpackNormal(tex2D(_NormalMap1,TRANSFORM_TEX(node_3768, _NormalMap1)));
                float2 node_6669 = (i.uv0+(node_3357*frac((node_1977+0.5))));
                float3 node_8189 = UnpackNormal(tex2D(_NormalMap1,TRANSFORM_TEX(node_6669, _NormalMap1)));
                float node_8239 = 0.5;
                float node_7750 = abs(((node_8239-node_9963)/node_8239));
                float3 normalLocal = lerp(node_3917.rgb,node_8189.rgb,node_7750);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 node_3576 = tex2D(_MetalAOHeightRough,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough));
                float4 node_9816 = tex2D(_MetalAOHeightRough,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough));
                float gloss = lerp( (1.0 - _Roughness), lerp(node_3576.a,node_9816.a,node_7750), _RoughnessMap1 );
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = lerp( _Metallic, lerp(node_3576.r,node_9816.r,node_7750), _MetallicMap1 );
                float specularMonochrome;
                float4 AlbedoOpacity1_1 = tex2D(_AlbedoOpacity1,TRANSFORM_TEX(node_3768, _AlbedoOpacity1));
                float4 AlbedoOpacity1_2 = tex2D(_AlbedoOpacity1,TRANSFORM_TEX(node_6669, _AlbedoOpacity1));
                float3 diffuseColor = (lerp(AlbedoOpacity1_1.rgb,AlbedoOpacity1_2.rgb,node_7750)*_Color.rgb); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, GGXTerm(NdotH, 1.0-gloss));
                float specularPBL = (NdotL*visTerm*normTerm) * (UNITY_PI / 4);
                if (IsGammaSpace())
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                specularPBL = max(0, specularPBL * NdotL);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz)*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                indirectDiffuse *= lerp(node_3576.g,node_9816.g,node_7750); // Diffuse AO
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_968 = tex2D(_Emissive1,TRANSFORM_TEX(node_3768, _Emissive1));
                float4 node_5676 = tex2D(_Emissive1,TRANSFORM_TEX(node_6669, _Emissive1));
                float3 emissive = lerp( _EmissiveColor1.rgb, lerp(node_968.rgb,node_5676.rgb,node_7750), _EmissiveMap1 );
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,lerp(AlbedoOpacity1_1.a,AlbedoOpacity1_2.a,node_7750));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _Metallic;
            uniform float _Roughness;
            uniform sampler2D _AlbedoOpacity1; uniform float4 _AlbedoOpacity1_ST;
            uniform float _FlowSpeed;
            uniform float _FlowPower;
            uniform sampler2D _NormalMap1; uniform float4 _NormalMap1_ST;
            uniform sampler2D _MetalAOHeightRough; uniform float4 _MetalAOHeightRough_ST;
            uniform fixed _MetallicMap1;
            uniform fixed _RoughnessMap1;
            uniform sampler2D _Emissive1; uniform float4 _Emissive1_ST;
            uniform fixed _EmissiveMap1;
            uniform float4 _EmissiveColor1;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_3357 = (((-1.0)*_FlowPower)*(i.uv2.rg*1.0+-0.5));
                float4 node_1664 = _Time + _TimeEditor;
                float node_1977 = (node_1664.r*_FlowSpeed);
                float node_9963 = frac(node_1977);
                float2 node_3768 = (i.uv0+(node_3357*node_9963));
                float3 node_3917 = UnpackNormal(tex2D(_NormalMap1,TRANSFORM_TEX(node_3768, _NormalMap1)));
                float2 node_6669 = (i.uv0+(node_3357*frac((node_1977+0.5))));
                float3 node_8189 = UnpackNormal(tex2D(_NormalMap1,TRANSFORM_TEX(node_6669, _NormalMap1)));
                float node_8239 = 0.5;
                float node_7750 = abs(((node_8239-node_9963)/node_8239));
                float3 normalLocal = lerp(node_3917.rgb,node_8189.rgb,node_7750);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 node_3576 = tex2D(_MetalAOHeightRough,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough));
                float4 node_9816 = tex2D(_MetalAOHeightRough,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough));
                float gloss = lerp( (1.0 - _Roughness), lerp(node_3576.a,node_9816.a,node_7750), _RoughnessMap1 );
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = lerp( _Metallic, lerp(node_3576.r,node_9816.r,node_7750), _MetallicMap1 );
                float specularMonochrome;
                float4 AlbedoOpacity1_1 = tex2D(_AlbedoOpacity1,TRANSFORM_TEX(node_3768, _AlbedoOpacity1));
                float4 AlbedoOpacity1_2 = tex2D(_AlbedoOpacity1,TRANSFORM_TEX(node_6669, _AlbedoOpacity1));
                float3 diffuseColor = (lerp(AlbedoOpacity1_1.rgb,AlbedoOpacity1_2.rgb,node_7750)*_Color.rgb); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, GGXTerm(NdotH, 1.0-gloss));
                float specularPBL = (NdotL*visTerm*normTerm) * (UNITY_PI / 4);
                if (IsGammaSpace())
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                specularPBL = max(0, specularPBL * NdotL);
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * lerp(AlbedoOpacity1_1.a,AlbedoOpacity1_2.a,node_7750),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _Metallic;
            uniform float _Roughness;
            uniform sampler2D _AlbedoOpacity1; uniform float4 _AlbedoOpacity1_ST;
            uniform float _FlowSpeed;
            uniform float _FlowPower;
            uniform sampler2D _MetalAOHeightRough; uniform float4 _MetalAOHeightRough_ST;
            uniform fixed _MetallicMap1;
            uniform fixed _RoughnessMap1;
            uniform sampler2D _Emissive1; uniform float4 _Emissive1_ST;
            uniform fixed _EmissiveMap1;
            uniform float4 _EmissiveColor1;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float2 node_3357 = (((-1.0)*_FlowPower)*(i.uv2.rg*1.0+-0.5));
                float4 node_1664 = _Time + _TimeEditor;
                float node_1977 = (node_1664.r*_FlowSpeed);
                float node_9963 = frac(node_1977);
                float2 node_3768 = (i.uv0+(node_3357*node_9963));
                float4 node_968 = tex2D(_Emissive1,TRANSFORM_TEX(node_3768, _Emissive1));
                float2 node_6669 = (i.uv0+(node_3357*frac((node_1977+0.5))));
                float4 node_5676 = tex2D(_Emissive1,TRANSFORM_TEX(node_6669, _Emissive1));
                float node_8239 = 0.5;
                float node_7750 = abs(((node_8239-node_9963)/node_8239));
                o.Emission = lerp( _EmissiveColor1.rgb, lerp(node_968.rgb,node_5676.rgb,node_7750), _EmissiveMap1 );
                
                float4 AlbedoOpacity1_1 = tex2D(_AlbedoOpacity1,TRANSFORM_TEX(node_3768, _AlbedoOpacity1));
                float4 AlbedoOpacity1_2 = tex2D(_AlbedoOpacity1,TRANSFORM_TEX(node_6669, _AlbedoOpacity1));
                float3 diffColor = (lerp(AlbedoOpacity1_1.rgb,AlbedoOpacity1_2.rgb,node_7750)*_Color.rgb);
                float specularMonochrome;
                float3 specColor;
                float4 node_3576 = tex2D(_MetalAOHeightRough,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough));
                float4 node_9816 = tex2D(_MetalAOHeightRough,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough));
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, lerp( _Metallic, lerp(node_3576.r,node_9816.r,node_7750), _MetallicMap1 ), specColor, specularMonochrome );
                float roughness = 1.0 - lerp( (1.0 - _Roughness), lerp(node_3576.a,node_9816.a,node_7750), _RoughnessMap1 );
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Standard"
    CustomEditor "ShaderForgeMaterialInspector"
}
