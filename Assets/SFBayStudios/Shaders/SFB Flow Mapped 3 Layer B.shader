// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:Standard,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:34616,y:32108,varname:node_2865,prsc:2|diff-6790-OUT,spec-517-OUT,gloss-189-OUT,normal-9775-OUT,emission-6516-OUT,difocc-855-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:30676,y:34367,varname:node_6343,prsc:2|A-9733-RGB,B-6665-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:30676,y:34536,ptovrint:False,ptlb:Color 1,ptin:_Color1,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2dAsset,id:4037,x:29831,y:34422,ptovrint:False,ptlb:Albedo Opacity 1,ptin:_AlbedoOpacity1,varname:_AlbedoOpacity1,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8bd6005f9bdc84c0c9feac733f094b02,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8407,x:31743,y:30869,varname:AlbedoOpacity1_1,prsc:2,ntxv:0,isnm:False|UVIN-3768-OUT,TEX-9509-TEX;n:type:ShaderForge.SFN_Time,id:1664,x:29488,y:32252,varname:node_1664,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:9628,x:29488,y:32479,ptovrint:False,ptlb:Flow Speed,ptin:_FlowSpeed,varname:_FlowSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:1977,x:29705,y:32311,varname:node_1977,prsc:2|A-1664-T,B-9628-OUT;n:type:ShaderForge.SFN_Add,id:9866,x:29983,y:32369,varname:node_9866,prsc:2|A-1977-OUT,B-5965-OUT;n:type:ShaderForge.SFN_Frac,id:9963,x:29916,y:32198,varname:node_9963,prsc:2|IN-1977-OUT;n:type:ShaderForge.SFN_Frac,id:4067,x:30157,y:32369,varname:node_4067,prsc:2|IN-9866-OUT;n:type:ShaderForge.SFN_TexCoord,id:2320,x:29559,y:31947,varname:node_2320,prsc:2,uv:2,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:6904,x:29940,y:31947,varname:node_6904,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-4795-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4795,x:29725,y:31947,varname:node_4795,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2320-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:6928,x:29725,y:31858,ptovrint:False,ptlb:Flow Power,ptin:_FlowPower,varname:_FlowPower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:6845,x:29940,y:31803,varname:node_6845,prsc:2|A-3787-OUT,B-6928-OUT;n:type:ShaderForge.SFN_Multiply,id:3357,x:30119,y:31928,varname:node_3357,prsc:2|A-6845-OUT,B-6904-OUT;n:type:ShaderForge.SFN_Multiply,id:7702,x:30329,y:32066,varname:node_7702,prsc:2|A-3357-OUT,B-4067-OUT;n:type:ShaderForge.SFN_Multiply,id:385,x:30329,y:31928,varname:node_385,prsc:2|A-3357-OUT,B-9963-OUT;n:type:ShaderForge.SFN_TexCoord,id:5624,x:30306,y:31587,varname:node_5624,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:3768,x:30664,y:31761,varname:node_3768,prsc:2|A-5624-UVOUT,B-385-OUT;n:type:ShaderForge.SFN_Add,id:6669,x:30664,y:31914,varname:node_6669,prsc:2|A-5624-UVOUT,B-7702-OUT;n:type:ShaderForge.SFN_Tex2d,id:6246,x:31743,y:30997,varname:AlbedoOpacity1_2,prsc:2,ntxv:0,isnm:False|UVIN-6669-OUT,TEX-9509-TEX;n:type:ShaderForge.SFN_Lerp,id:4543,x:32048,y:31030,varname:node_4543,prsc:2|A-8407-RGB,B-6246-RGB,T-7750-OUT;n:type:ShaderForge.SFN_Subtract,id:1172,x:29818,y:32576,varname:node_1172,prsc:2|A-8239-OUT,B-9963-OUT;n:type:ShaderForge.SFN_Divide,id:1807,x:29985,y:32576,varname:node_1807,prsc:2|A-1172-OUT,B-8239-OUT;n:type:ShaderForge.SFN_Abs,id:7750,x:30142,y:32590,varname:node_7750,prsc:2|IN-1807-OUT;n:type:ShaderForge.SFN_Vector1,id:8239,x:29527,y:32632,varname:node_8239,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:3787,x:29725,y:31776,varname:node_3787,prsc:2,v1:-1;n:type:ShaderForge.SFN_Vector1,id:5965,x:29777,y:32477,varname:node_5965,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2dAsset,id:9666,x:29847,y:34656,ptovrint:False,ptlb:Normal Map 1,ptin:_NormalMap1,varname:node_9666,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:3917,x:31743,y:31133,varname:node_3917,prsc:2,ntxv:0,isnm:False|UVIN-3768-OUT,TEX-1943-TEX;n:type:ShaderForge.SFN_Tex2d,id:8189,x:31743,y:31264,varname:node_8189,prsc:2,ntxv:0,isnm:False|UVIN-6669-OUT,TEX-1943-TEX;n:type:ShaderForge.SFN_Lerp,id:475,x:32048,y:31164,varname:node_475,prsc:2|A-3917-RGB,B-8189-RGB,T-7750-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9187,x:29860,y:34953,ptovrint:False,ptlb:Metal AO Height Rough 1,ptin:_MetalAOHeightRough1,varname:node_9187,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3576,x:31743,y:31397,varname:node_3576,prsc:2,ntxv:0,isnm:False|TEX-3772-TEX;n:type:ShaderForge.SFN_Tex2d,id:9816,x:31743,y:31529,varname:node_9816,prsc:2,ntxv:0,isnm:False|TEX-3772-TEX;n:type:ShaderForge.SFN_Lerp,id:9234,x:32048,y:31294,varname:node_9234,prsc:2|A-3576-R,B-9816-R,T-7750-OUT;n:type:ShaderForge.SFN_Lerp,id:3424,x:32048,y:31420,varname:node_3424,prsc:2|A-3576-G,B-9816-G,T-7750-OUT;n:type:ShaderForge.SFN_Lerp,id:1469,x:32048,y:31552,varname:node_1469,prsc:2|A-3576-B,B-9816-B,T-7750-OUT;n:type:ShaderForge.SFN_Lerp,id:1674,x:32048,y:31676,varname:node_1674,prsc:2|A-3576-A,B-9816-A,T-7750-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:5582,x:29867,y:35358,ptovrint:False,ptlb:Emissive 1,ptin:_Emissive1,varname:node_5582,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:968,x:31743,y:31666,varname:node_968,prsc:2,ntxv:0,isnm:False|UVIN-3768-OUT,TEX-2838-TEX;n:type:ShaderForge.SFN_Tex2d,id:5676,x:31743,y:31800,varname:node_5676,prsc:2,ntxv:0,isnm:False|UVIN-6669-OUT,TEX-2838-TEX;n:type:ShaderForge.SFN_Lerp,id:6895,x:32048,y:31802,varname:node_6895,prsc:2|A-968-RGB,B-5676-RGB,T-7750-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9754,x:31026,y:33059,ptovrint:False,ptlb:Albedo Opacity 2,ptin:_AlbedoOpacity2,varname:node_9754,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:965,x:31026,y:33240,ptovrint:False,ptlb:Normal Map 2,ptin:_NormalMap2,varname:node_965,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2dAsset,id:6165,x:31026,y:33425,ptovrint:False,ptlb:Metal AO Height Rough 2,ptin:_MetalAOHeightRough2,varname:node_6165,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:8709,x:31026,y:33610,ptovrint:False,ptlb:Emissive 2,ptin:_Emissive2,varname:node_8709,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:2638,x:31314,y:32299,varname:node_2638,prsc:2;n:type:ShaderForge.SFN_Blend,id:2397,x:31916,y:32346,varname:node_2397,prsc:2,blmd:2,clmp:True|SRC-9556-OUT,DST-3565-OUT;n:type:ShaderForge.SFN_Blend,id:7319,x:31922,y:32665,varname:node_7319,prsc:2,blmd:13,clmp:True|SRC-9556-OUT,DST-3565-OUT;n:type:ShaderForge.SFN_Blend,id:3330,x:32119,y:32387,varname:node_3330,prsc:2,blmd:6,clmp:True|SRC-2397-OUT,DST-2455-OUT;n:type:ShaderForge.SFN_Lerp,id:555,x:32862,y:33016,varname:node_555,prsc:2|A-6343-OUT,B-2857-OUT,T-3330-OUT;n:type:ShaderForge.SFN_Lerp,id:2175,x:32862,y:33140,varname:node_2175,prsc:2|A-8113-RGB,B-12-RGB,T-3330-OUT;n:type:ShaderForge.SFN_Lerp,id:773,x:32862,y:33509,varname:node_773,prsc:2|A-1348-A,B-5729-OUT,T-3330-OUT;n:type:ShaderForge.SFN_Lerp,id:7852,x:32862,y:33636,varname:node_7852,prsc:2|A-3898-RGB,B-3823-RGB,T-3330-OUT;n:type:ShaderForge.SFN_Lerp,id:3458,x:32862,y:33270,varname:node_3458,prsc:2|A-1348-R,B-635-OUT,T-3330-OUT;n:type:ShaderForge.SFN_Lerp,id:4087,x:32862,y:33387,varname:node_4087,prsc:2|A-1348-G,B-332-G,T-3330-OUT;n:type:ShaderForge.SFN_Parallax,id:7521,x:29313,y:34575,varname:node_7521,prsc:2|UVIN-3346-UVOUT,HEI-5690-B,REF-5632-OUT;n:type:ShaderForge.SFN_TexCoord,id:3346,x:28131,y:31887,varname:node_3346,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:5632,x:28857,y:34899,ptovrint:False,ptlb:Parallax 1,ptin:_Parallax1,varname:node_5632,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:-1,max:1;n:type:ShaderForge.SFN_SwitchProperty,id:8396,x:29594,y:34519,ptovrint:False,ptlb:Use Parallax 1?,ptin:_UseParallax1,varname:node_8396,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-3346-UVOUT,B-7521-UVOUT;n:type:ShaderForge.SFN_Tex2dAsset,id:1943,x:31227,y:31400,ptovrint:False,ptlb:Normal Map 3,ptin:_NormalMap3,varname:node_1943,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2dAsset,id:3772,x:31227,y:31578,ptovrint:False,ptlb:Metal AO Height Rough 3,ptin:_MetalAOHeightRough3,varname:node_3772,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:2838,x:31227,y:31765,ptovrint:False,ptlb:Emissive 3,ptin:_Emissive3,varname:node_2838,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:9509,x:31227,y:31221,ptovrint:False,ptlb:Albedo Opacity 3,ptin:_AlbedoOpacity3,varname:node_9509,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:332,x:31732,y:33474,varname:node_332,prsc:2,ntxv:0,isnm:False|UVIN-1114-OUT,TEX-6165-TEX;n:type:ShaderForge.SFN_Tex2d,id:12,x:31732,y:33296,varname:node_12,prsc:2,ntxv:0,isnm:False|UVIN-1114-OUT,TEX-965-TEX;n:type:ShaderForge.SFN_Tex2d,id:3511,x:31732,y:33112,varname:node_3511,prsc:2,ntxv:0,isnm:False|UVIN-1114-OUT,TEX-9754-TEX;n:type:ShaderForge.SFN_Tex2d,id:3823,x:31732,y:33641,varname:node_3823,prsc:2,ntxv:0,isnm:False|UVIN-1114-OUT,TEX-8709-TEX;n:type:ShaderForge.SFN_Tex2d,id:370,x:29724,y:33766,varname:node_370,prsc:2,ntxv:0,isnm:False|TEX-6165-TEX;n:type:ShaderForge.SFN_Parallax,id:6446,x:30036,y:33606,varname:node_6446,prsc:2|UVIN-3346-UVOUT,HEI-370-B,REF-514-OUT;n:type:ShaderForge.SFN_Slider,id:514,x:29785,y:33922,ptovrint:False,ptlb:Parallax 2,ptin:_Parallax2,varname:node_514,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:6790,x:33788,y:31827,varname:node_6790,prsc:2|A-555-OUT,B-8696-OUT,T-6959-OUT;n:type:ShaderForge.SFN_Lerp,id:9775,x:33788,y:31978,varname:node_9775,prsc:2|A-2175-OUT,B-475-OUT,T-6959-OUT;n:type:ShaderForge.SFN_Lerp,id:189,x:33788,y:32102,varname:node_189,prsc:2|A-773-OUT,B-2750-OUT,T-6959-OUT;n:type:ShaderForge.SFN_Lerp,id:517,x:33788,y:32244,varname:node_517,prsc:2|A-3458-OUT,B-636-OUT,T-6959-OUT;n:type:ShaderForge.SFN_Lerp,id:6516,x:33788,y:32369,varname:node_6516,prsc:2|A-7852-OUT,B-6895-OUT,T-6959-OUT;n:type:ShaderForge.SFN_Lerp,id:855,x:33788,y:32514,varname:node_855,prsc:2|A-4087-OUT,B-3424-OUT,T-6959-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1114,x:30290,y:33616,ptovrint:False,ptlb:Use Parallax 2?,ptin:_UseParallax2,varname:node_1114,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-3346-UVOUT,B-6446-UVOUT;n:type:ShaderForge.SFN_Slider,id:930,x:31205,y:32476,ptovrint:False,ptlb:R Vertex Shift,ptin:_RVertexShift,varname:node_930,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:3565,x:31625,y:32424,varname:node_3565,prsc:2|A-930-OUT,B-2638-R;n:type:ShaderForge.SFN_Slider,id:8648,x:31236,y:32111,ptovrint:False,ptlb:G Vertex Shift,ptin:_GVertexShift,varname:node_8648,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:442,x:31632,y:32093,varname:node_442,prsc:2|A-8648-OUT,B-2638-G;n:type:ShaderForge.SFN_OneMinus,id:9556,x:31625,y:32591,varname:node_9556,prsc:2|IN-1348-B;n:type:ShaderForge.SFN_OneMinus,id:3277,x:32565,y:31080,varname:node_3277,prsc:2|IN-7845-OUT;n:type:ShaderForge.SFN_Blend,id:6598,x:32921,y:30829,varname:node_6598,prsc:2,blmd:2,clmp:True|SRC-3277-OUT,DST-442-OUT;n:type:ShaderForge.SFN_Blend,id:6019,x:32941,y:31068,varname:node_6019,prsc:2,blmd:13,clmp:True|SRC-3277-OUT,DST-442-OUT;n:type:ShaderForge.SFN_Blend,id:6959,x:33399,y:31558,varname:node_6959,prsc:2,blmd:6,clmp:True|SRC-6598-OUT,DST-9655-OUT;n:type:ShaderForge.SFN_Add,id:9655,x:33152,y:31018,varname:node_9655,prsc:2|A-1891-OUT,B-6019-OUT;n:type:ShaderForge.SFN_Slider,id:1891,x:32744,y:30998,ptovrint:False,ptlb:G Contrast,ptin:_GContrast,varname:node_1891,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:0.5;n:type:ShaderForge.SFN_Tex2d,id:9733,x:30152,y:34413,varname:node_9733,prsc:2,tex:8bd6005f9bdc84c0c9feac733f094b02,ntxv:0,isnm:False|UVIN-8396-OUT,TEX-4037-TEX;n:type:ShaderForge.SFN_Tex2d,id:8113,x:30141,y:34620,varname:node_8113,prsc:2,ntxv:0,isnm:False|UVIN-8396-OUT,TEX-9666-TEX;n:type:ShaderForge.SFN_Tex2d,id:1348,x:30145,y:34940,varname:node_1348,prsc:2,ntxv:0,isnm:False|UVIN-8396-OUT,TEX-9187-TEX;n:type:ShaderForge.SFN_Tex2d,id:3898,x:30125,y:35397,varname:node_3898,prsc:2,ntxv:0,isnm:False|UVIN-8396-OUT,TEX-5582-TEX;n:type:ShaderForge.SFN_Tex2d,id:5690,x:28949,y:34563,varname:node_5690,prsc:2,ntxv:0,isnm:False|TEX-9187-TEX;n:type:ShaderForge.SFN_Lerp,id:7845,x:32862,y:33775,varname:node_7845,prsc:2|A-1348-B,B-332-B,T-3330-OUT;n:type:ShaderForge.SFN_Lerp,id:8696,x:32346,y:30900,varname:node_8696,prsc:2|A-555-OUT,B-7508-OUT,T-2763-OUT;n:type:ShaderForge.SFN_Lerp,id:2857,x:32157,y:32993,varname:node_2857,prsc:2|A-6343-OUT,B-5668-OUT,T-5290-OUT;n:type:ShaderForge.SFN_Slider,id:5352,x:31865,y:30723,ptovrint:False,ptlb:Opacity 3 Shift,ptin:_Opacity3Shift,varname:node_5352,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:5052,x:32185,y:30761,varname:node_5052,prsc:2|A-5352-OUT,B-8407-A;n:type:ShaderForge.SFN_Clamp01,id:2763,x:32361,y:30741,varname:node_2763,prsc:2|IN-5052-OUT;n:type:ShaderForge.SFN_Slider,id:9832,x:31335,y:32860,ptovrint:False,ptlb:Opacity 2 Shift,ptin:_Opacity2Shift,varname:node_9832,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:1633,x:31697,y:32915,varname:node_1633,prsc:2|A-9832-OUT,B-3511-A;n:type:ShaderForge.SFN_Clamp01,id:5290,x:31899,y:32873,varname:node_5290,prsc:2|IN-1633-OUT;n:type:ShaderForge.SFN_Multiply,id:5668,x:32008,y:33094,varname:node_5668,prsc:2|A-3511-RGB,B-524-RGB;n:type:ShaderForge.SFN_Color,id:524,x:31864,y:33012,ptovrint:False,ptlb:Color 2,ptin:_Color2,varname:node_524,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:3246,x:32328,y:31253,ptovrint:False,ptlb:Color 3,ptin:_Color3,varname:node_3246,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:7508,x:32328,y:31098,varname:node_7508,prsc:2|A-4543-OUT,B-3246-RGB;n:type:ShaderForge.SFN_Slider,id:6767,x:31963,y:33349,ptovrint:False,ptlb:Metallic 2,ptin:_Metallic2,varname:node_6767,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1756,x:31980,y:33512,ptovrint:False,ptlb:Roughness 2,ptin:_Roughness2,varname:node_1756,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_OneMinus,id:3718,x:32350,y:33507,varname:node_3718,prsc:2|IN-1756-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:635,x:32387,y:33373,ptovrint:False,ptlb:Metallic Map 2?,ptin:_MetallicMap2,varname:node_635,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-6767-OUT,B-332-R;n:type:ShaderForge.SFN_SwitchProperty,id:5729,x:32524,y:33593,ptovrint:False,ptlb:Roughness Map 2?,ptin:_RoughnessMap2,varname:node_5729,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-3718-OUT,B-332-A;n:type:ShaderForge.SFN_Slider,id:6749,x:32274,y:31600,ptovrint:False,ptlb:Metallic 3,ptin:_Metallic3,varname:node_6749,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:5534,x:32201,y:31960,ptovrint:False,ptlb:Roughness 3,ptin:_Roughness3,varname:node_5534,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_OneMinus,id:4474,x:32528,y:31923,varname:node_4474,prsc:2|IN-5534-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:636,x:32620,y:31636,ptovrint:False,ptlb:Metallic Map 3?,ptin:_MetallicMap3,varname:node_636,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-6749-OUT,B-9234-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:2750,x:32747,y:31849,ptovrint:False,ptlb:Roughness Map 3?,ptin:_RoughnessMap3,varname:node_2750,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-4474-OUT,B-1674-OUT;n:type:ShaderForge.SFN_Power,id:1442,x:32798,y:31221,varname:node_1442,prsc:2;n:type:ShaderForge.SFN_Slider,id:7104,x:31717,y:32542,ptovrint:False,ptlb:R Contrast,ptin:_RContrast,varname:node_7104,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:0.5;n:type:ShaderForge.SFN_Add,id:2455,x:32119,y:32593,varname:node_2455,prsc:2|A-7104-OUT,B-7319-OUT;proporder:4037-6665-9666-9187-5582-8396-5632-9754-524-9832-965-6165-635-5729-6767-1756-8709-1114-514-930-7104-9509-3246-5352-1943-3772-636-2750-6749-5534-2838-8648-1891-9628-6928;pass:END;sub:END;*/

Shader "SFBayStudios/SFB Flow Mapped 3 Layer B" {
    Properties {
        _AlbedoOpacity1 ("Albedo Opacity 1", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1,1,1,1)
        _NormalMap1 ("Normal Map 1", 2D) = "bump" {}
        _MetalAOHeightRough1 ("Metal AO Height Rough 1", 2D) = "white" {}
        _Emissive1 ("Emissive 1", 2D) = "black" {}
        [MaterialToggle] _UseParallax1 ("Use Parallax 1?", Float ) = -0.05
        _Parallax1 ("Parallax 1", Range(-1, 1)) = -1
        _AlbedoOpacity2 ("Albedo Opacity 2", 2D) = "white" {}
        _Color2 ("Color 2", Color) = (1,1,1,1)
        _Opacity2Shift ("Opacity 2 Shift", Range(-1, 1)) = 0
        _NormalMap2 ("Normal Map 2", 2D) = "bump" {}
        _MetalAOHeightRough2 ("Metal AO Height Rough 2", 2D) = "white" {}
        [MaterialToggle] _MetallicMap2 ("Metallic Map 2?", Float ) = 0
        [MaterialToggle] _RoughnessMap2 ("Roughness Map 2?", Float ) = 0
        _Metallic2 ("Metallic 2", Range(0, 1)) = 0
        _Roughness2 ("Roughness 2", Range(0, 1)) = 0
        _Emissive2 ("Emissive 2", 2D) = "black" {}
        [MaterialToggle] _UseParallax2 ("Use Parallax 2?", Float ) = 0
        _Parallax2 ("Parallax 2", Range(-1, 1)) = 0
        _RVertexShift ("R Vertex Shift", Range(-1, 1)) = 0
        _RContrast ("R Contrast", Range(-10, 0.5)) = 0
        _AlbedoOpacity3 ("Albedo Opacity 3", 2D) = "black" {}
        _Color3 ("Color 3", Color) = (1,1,1,1)
        _Opacity3Shift ("Opacity 3 Shift", Range(-1, 1)) = 0
        _NormalMap3 ("Normal Map 3", 2D) = "bump" {}
        _MetalAOHeightRough3 ("Metal AO Height Rough 3", 2D) = "white" {}
        [MaterialToggle] _MetallicMap3 ("Metallic Map 3?", Float ) = 0
        [MaterialToggle] _RoughnessMap3 ("Roughness Map 3?", Float ) = 0
        _Metallic3 ("Metallic 3", Range(0, 1)) = 0
        _Roughness3 ("Roughness 3", Range(0, 1)) = 0
        _Emissive3 ("Emissive 3", 2D) = "black" {}
        _GVertexShift ("G Vertex Shift", Range(-1, 1)) = 0
        _GContrast ("G Contrast", Range(-10, 0.5)) = 0
        _FlowSpeed ("Flow Speed", Float ) = 1
        _FlowPower ("Flow Power", Float ) = 0.5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal n3ds wiiu 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color1;
            uniform sampler2D _AlbedoOpacity1; uniform float4 _AlbedoOpacity1_ST;
            uniform float _FlowSpeed;
            uniform float _FlowPower;
            uniform sampler2D _NormalMap1; uniform float4 _NormalMap1_ST;
            uniform sampler2D _MetalAOHeightRough1; uniform float4 _MetalAOHeightRough1_ST;
            uniform sampler2D _Emissive1; uniform float4 _Emissive1_ST;
            uniform sampler2D _AlbedoOpacity2; uniform float4 _AlbedoOpacity2_ST;
            uniform sampler2D _NormalMap2; uniform float4 _NormalMap2_ST;
            uniform sampler2D _MetalAOHeightRough2; uniform float4 _MetalAOHeightRough2_ST;
            uniform sampler2D _Emissive2; uniform float4 _Emissive2_ST;
            uniform float _Parallax1;
            uniform fixed _UseParallax1;
            uniform sampler2D _NormalMap3; uniform float4 _NormalMap3_ST;
            uniform sampler2D _MetalAOHeightRough3; uniform float4 _MetalAOHeightRough3_ST;
            uniform sampler2D _Emissive3; uniform float4 _Emissive3_ST;
            uniform sampler2D _AlbedoOpacity3; uniform float4 _AlbedoOpacity3_ST;
            uniform float _Parallax2;
            uniform fixed _UseParallax2;
            uniform float _RVertexShift;
            uniform float _GVertexShift;
            uniform float _GContrast;
            uniform float _Opacity3Shift;
            uniform float _Opacity2Shift;
            uniform float4 _Color2;
            uniform float4 _Color3;
            uniform float _Metallic2;
            uniform float _Roughness2;
            uniform fixed _MetallicMap2;
            uniform fixed _RoughnessMap2;
            uniform float _Metallic3;
            uniform float _Roughness3;
            uniform fixed _MetallicMap3;
            uniform fixed _RoughnessMap3;
            uniform float _RContrast;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
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
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
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
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_5690 = tex2D(_MetalAOHeightRough1,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough1));
                float2 _UseParallax1_var = lerp( i.uv0, (0.05*(node_5690.b - _Parallax1)*mul(tangentTransform, viewDirection).xy + i.uv0).rg, _UseParallax1 );
                float3 node_8113 = UnpackNormal(tex2D(_NormalMap1,TRANSFORM_TEX(_UseParallax1_var, _NormalMap1)));
                float4 node_370 = tex2D(_MetalAOHeightRough2,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough2));
                float2 _UseParallax2_var = lerp( i.uv0, (0.05*(node_370.b - _Parallax2)*mul(tangentTransform, viewDirection).xy + i.uv0).rg, _UseParallax2 );
                float3 node_12 = UnpackNormal(tex2D(_NormalMap2,TRANSFORM_TEX(_UseParallax2_var, _NormalMap2)));
                float4 node_1348 = tex2D(_MetalAOHeightRough1,TRANSFORM_TEX(_UseParallax1_var, _MetalAOHeightRough1));
                float node_9556 = (1.0 - node_1348.b);
                float node_3565 = (_RVertexShift+i.vertexColor.r);
                float node_3330 = saturate((1.0-(1.0-saturate((1.0-((1.0-node_3565)/node_9556))))*(1.0-(_RContrast+saturate(( node_9556 > 0.5 ? (node_3565/((1.0-node_9556)*2.0)) : (1.0-(((1.0-node_3565)*0.5)/node_9556))))))));
                float2 node_3357 = (((-1.0)*_FlowPower)*(i.uv2.rg*1.0+-0.5));
                float4 node_1664 = _Time + _TimeEditor;
                float node_1977 = (node_1664.g*_FlowSpeed);
                float node_9963 = frac(node_1977);
                float2 node_3768 = (i.uv0+(node_3357*node_9963));
                float3 node_3917 = UnpackNormal(tex2D(_NormalMap3,TRANSFORM_TEX(node_3768, _NormalMap3)));
                float2 node_6669 = (i.uv0+(node_3357*frac((node_1977+0.5))));
                float3 node_8189 = UnpackNormal(tex2D(_NormalMap3,TRANSFORM_TEX(node_6669, _NormalMap3)));
                float node_8239 = 0.5;
                float node_7750 = abs(((node_8239-node_9963)/node_8239));
                float4 node_332 = tex2D(_MetalAOHeightRough2,TRANSFORM_TEX(_UseParallax2_var, _MetalAOHeightRough2));
                float node_3277 = (1.0 - lerp(node_1348.b,node_332.b,node_3330));
                float node_442 = (_GVertexShift+i.vertexColor.g);
                float node_6959 = saturate((1.0-(1.0-saturate((1.0-((1.0-node_442)/node_3277))))*(1.0-(_GContrast+saturate(( node_3277 > 0.5 ? (node_442/((1.0-node_3277)*2.0)) : (1.0-(((1.0-node_442)*0.5)/node_3277))))))));
                float3 normalLocal = lerp(lerp(node_8113.rgb,node_12.rgb,node_3330),lerp(node_3917.rgb,node_8189.rgb,node_7750),node_6959);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 node_3576 = tex2D(_MetalAOHeightRough3,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough3));
                float4 node_9816 = tex2D(_MetalAOHeightRough3,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough3));
                float gloss = lerp(lerp(node_1348.a,lerp( (1.0 - _Roughness2), node_332.a, _RoughnessMap2 ),node_3330),lerp( (1.0 - _Roughness3), lerp(node_3576.a,node_9816.a,node_7750), _RoughnessMap3 ),node_6959);
                float perceptualRoughness = 1.0 - lerp(lerp(node_1348.a,lerp( (1.0 - _Roughness2), node_332.a, _RoughnessMap2 ),node_3330),lerp( (1.0 - _Roughness3), lerp(node_3576.a,node_9816.a,node_7750), _RoughnessMap3 ),node_6959);
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
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
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = lerp(lerp(node_1348.r,lerp( _Metallic2, node_332.r, _MetallicMap2 ),node_3330),lerp( _Metallic3, lerp(node_3576.r,node_9816.r,node_7750), _MetallicMap3 ),node_6959);
                float specularMonochrome;
                float4 node_9733 = tex2D(_AlbedoOpacity1,TRANSFORM_TEX(_UseParallax1_var, _AlbedoOpacity1));
                float3 node_6343 = (node_9733.rgb*_Color1.rgb);
                float4 node_3511 = tex2D(_AlbedoOpacity2,TRANSFORM_TEX(_UseParallax2_var, _AlbedoOpacity2));
                float3 node_555 = lerp(node_6343,lerp(node_6343,(node_3511.rgb*_Color2.rgb),saturate((_Opacity2Shift+node_3511.a))),node_3330);
                float4 AlbedoOpacity1_1 = tex2D(_AlbedoOpacity3,TRANSFORM_TEX(node_3768, _AlbedoOpacity3));
                float4 AlbedoOpacity1_2 = tex2D(_AlbedoOpacity3,TRANSFORM_TEX(node_6669, _AlbedoOpacity3));
                float3 diffuseColor = lerp(node_555,lerp(node_555,(lerp(AlbedoOpacity1_1.rgb,AlbedoOpacity1_2.rgb,node_7750)*_Color3.rgb),saturate((_Opacity3Shift+AlbedoOpacity1_1.a))),node_6959); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                indirectDiffuse *= lerp(lerp(node_1348.g,node_332.g,node_3330),lerp(node_3576.g,node_9816.g,node_7750),node_6959); // Diffuse AO
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_3898 = tex2D(_Emissive1,TRANSFORM_TEX(_UseParallax1_var, _Emissive1));
                float4 node_3823 = tex2D(_Emissive2,TRANSFORM_TEX(_UseParallax2_var, _Emissive2));
                float4 node_968 = tex2D(_Emissive3,TRANSFORM_TEX(node_3768, _Emissive3));
                float4 node_5676 = tex2D(_Emissive3,TRANSFORM_TEX(node_6669, _Emissive3));
                float3 emissive = lerp(lerp(node_3898.rgb,node_3823.rgb,node_3330),lerp(node_968.rgb,node_5676.rgb,node_7750),node_6959);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal n3ds wiiu 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color1;
            uniform sampler2D _AlbedoOpacity1; uniform float4 _AlbedoOpacity1_ST;
            uniform float _FlowSpeed;
            uniform float _FlowPower;
            uniform sampler2D _NormalMap1; uniform float4 _NormalMap1_ST;
            uniform sampler2D _MetalAOHeightRough1; uniform float4 _MetalAOHeightRough1_ST;
            uniform sampler2D _Emissive1; uniform float4 _Emissive1_ST;
            uniform sampler2D _AlbedoOpacity2; uniform float4 _AlbedoOpacity2_ST;
            uniform sampler2D _NormalMap2; uniform float4 _NormalMap2_ST;
            uniform sampler2D _MetalAOHeightRough2; uniform float4 _MetalAOHeightRough2_ST;
            uniform sampler2D _Emissive2; uniform float4 _Emissive2_ST;
            uniform float _Parallax1;
            uniform fixed _UseParallax1;
            uniform sampler2D _NormalMap3; uniform float4 _NormalMap3_ST;
            uniform sampler2D _MetalAOHeightRough3; uniform float4 _MetalAOHeightRough3_ST;
            uniform sampler2D _Emissive3; uniform float4 _Emissive3_ST;
            uniform sampler2D _AlbedoOpacity3; uniform float4 _AlbedoOpacity3_ST;
            uniform float _Parallax2;
            uniform fixed _UseParallax2;
            uniform float _RVertexShift;
            uniform float _GVertexShift;
            uniform float _GContrast;
            uniform float _Opacity3Shift;
            uniform float _Opacity2Shift;
            uniform float4 _Color2;
            uniform float4 _Color3;
            uniform float _Metallic2;
            uniform float _Roughness2;
            uniform fixed _MetallicMap2;
            uniform fixed _RoughnessMap2;
            uniform float _Metallic3;
            uniform float _Roughness3;
            uniform fixed _MetallicMap3;
            uniform fixed _RoughnessMap3;
            uniform float _RContrast;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
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
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
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
                float4 node_5690 = tex2D(_MetalAOHeightRough1,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough1));
                float2 _UseParallax1_var = lerp( i.uv0, (0.05*(node_5690.b - _Parallax1)*mul(tangentTransform, viewDirection).xy + i.uv0).rg, _UseParallax1 );
                float3 node_8113 = UnpackNormal(tex2D(_NormalMap1,TRANSFORM_TEX(_UseParallax1_var, _NormalMap1)));
                float4 node_370 = tex2D(_MetalAOHeightRough2,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough2));
                float2 _UseParallax2_var = lerp( i.uv0, (0.05*(node_370.b - _Parallax2)*mul(tangentTransform, viewDirection).xy + i.uv0).rg, _UseParallax2 );
                float3 node_12 = UnpackNormal(tex2D(_NormalMap2,TRANSFORM_TEX(_UseParallax2_var, _NormalMap2)));
                float4 node_1348 = tex2D(_MetalAOHeightRough1,TRANSFORM_TEX(_UseParallax1_var, _MetalAOHeightRough1));
                float node_9556 = (1.0 - node_1348.b);
                float node_3565 = (_RVertexShift+i.vertexColor.r);
                float node_3330 = saturate((1.0-(1.0-saturate((1.0-((1.0-node_3565)/node_9556))))*(1.0-(_RContrast+saturate(( node_9556 > 0.5 ? (node_3565/((1.0-node_9556)*2.0)) : (1.0-(((1.0-node_3565)*0.5)/node_9556))))))));
                float2 node_3357 = (((-1.0)*_FlowPower)*(i.uv2.rg*1.0+-0.5));
                float4 node_1664 = _Time + _TimeEditor;
                float node_1977 = (node_1664.g*_FlowSpeed);
                float node_9963 = frac(node_1977);
                float2 node_3768 = (i.uv0+(node_3357*node_9963));
                float3 node_3917 = UnpackNormal(tex2D(_NormalMap3,TRANSFORM_TEX(node_3768, _NormalMap3)));
                float2 node_6669 = (i.uv0+(node_3357*frac((node_1977+0.5))));
                float3 node_8189 = UnpackNormal(tex2D(_NormalMap3,TRANSFORM_TEX(node_6669, _NormalMap3)));
                float node_8239 = 0.5;
                float node_7750 = abs(((node_8239-node_9963)/node_8239));
                float4 node_332 = tex2D(_MetalAOHeightRough2,TRANSFORM_TEX(_UseParallax2_var, _MetalAOHeightRough2));
                float node_3277 = (1.0 - lerp(node_1348.b,node_332.b,node_3330));
                float node_442 = (_GVertexShift+i.vertexColor.g);
                float node_6959 = saturate((1.0-(1.0-saturate((1.0-((1.0-node_442)/node_3277))))*(1.0-(_GContrast+saturate(( node_3277 > 0.5 ? (node_442/((1.0-node_3277)*2.0)) : (1.0-(((1.0-node_442)*0.5)/node_3277))))))));
                float3 normalLocal = lerp(lerp(node_8113.rgb,node_12.rgb,node_3330),lerp(node_3917.rgb,node_8189.rgb,node_7750),node_6959);
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
                float4 node_3576 = tex2D(_MetalAOHeightRough3,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough3));
                float4 node_9816 = tex2D(_MetalAOHeightRough3,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough3));
                float gloss = lerp(lerp(node_1348.a,lerp( (1.0 - _Roughness2), node_332.a, _RoughnessMap2 ),node_3330),lerp( (1.0 - _Roughness3), lerp(node_3576.a,node_9816.a,node_7750), _RoughnessMap3 ),node_6959);
                float perceptualRoughness = 1.0 - lerp(lerp(node_1348.a,lerp( (1.0 - _Roughness2), node_332.a, _RoughnessMap2 ),node_3330),lerp( (1.0 - _Roughness3), lerp(node_3576.a,node_9816.a,node_7750), _RoughnessMap3 ),node_6959);
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = lerp(lerp(node_1348.r,lerp( _Metallic2, node_332.r, _MetallicMap2 ),node_3330),lerp( _Metallic3, lerp(node_3576.r,node_9816.r,node_7750), _MetallicMap3 ),node_6959);
                float specularMonochrome;
                float4 node_9733 = tex2D(_AlbedoOpacity1,TRANSFORM_TEX(_UseParallax1_var, _AlbedoOpacity1));
                float3 node_6343 = (node_9733.rgb*_Color1.rgb);
                float4 node_3511 = tex2D(_AlbedoOpacity2,TRANSFORM_TEX(_UseParallax2_var, _AlbedoOpacity2));
                float3 node_555 = lerp(node_6343,lerp(node_6343,(node_3511.rgb*_Color2.rgb),saturate((_Opacity2Shift+node_3511.a))),node_3330);
                float4 AlbedoOpacity1_1 = tex2D(_AlbedoOpacity3,TRANSFORM_TEX(node_3768, _AlbedoOpacity3));
                float4 AlbedoOpacity1_2 = tex2D(_AlbedoOpacity3,TRANSFORM_TEX(node_6669, _AlbedoOpacity3));
                float3 diffuseColor = lerp(node_555,lerp(node_555,(lerp(AlbedoOpacity1_1.rgb,AlbedoOpacity1_2.rgb,node_7750)*_Color3.rgb),saturate((_Opacity3Shift+AlbedoOpacity1_1.a))),node_6959); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
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
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal n3ds wiiu 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color1;
            uniform sampler2D _AlbedoOpacity1; uniform float4 _AlbedoOpacity1_ST;
            uniform float _FlowSpeed;
            uniform float _FlowPower;
            uniform sampler2D _MetalAOHeightRough1; uniform float4 _MetalAOHeightRough1_ST;
            uniform sampler2D _Emissive1; uniform float4 _Emissive1_ST;
            uniform sampler2D _AlbedoOpacity2; uniform float4 _AlbedoOpacity2_ST;
            uniform sampler2D _MetalAOHeightRough2; uniform float4 _MetalAOHeightRough2_ST;
            uniform sampler2D _Emissive2; uniform float4 _Emissive2_ST;
            uniform float _Parallax1;
            uniform fixed _UseParallax1;
            uniform sampler2D _MetalAOHeightRough3; uniform float4 _MetalAOHeightRough3_ST;
            uniform sampler2D _Emissive3; uniform float4 _Emissive3_ST;
            uniform sampler2D _AlbedoOpacity3; uniform float4 _AlbedoOpacity3_ST;
            uniform float _Parallax2;
            uniform fixed _UseParallax2;
            uniform float _RVertexShift;
            uniform float _GVertexShift;
            uniform float _GContrast;
            uniform float _Opacity3Shift;
            uniform float _Opacity2Shift;
            uniform float4 _Color2;
            uniform float4 _Color3;
            uniform float _Metallic2;
            uniform float _Roughness2;
            uniform fixed _MetallicMap2;
            uniform fixed _RoughnessMap2;
            uniform float _Metallic3;
            uniform float _Roughness3;
            uniform fixed _MetallicMap3;
            uniform fixed _RoughnessMap3;
            uniform float _RContrast;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
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
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_5690 = tex2D(_MetalAOHeightRough1,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough1));
                float2 _UseParallax1_var = lerp( i.uv0, (0.05*(node_5690.b - _Parallax1)*mul(tangentTransform, viewDirection).xy + i.uv0).rg, _UseParallax1 );
                float4 node_3898 = tex2D(_Emissive1,TRANSFORM_TEX(_UseParallax1_var, _Emissive1));
                float4 node_370 = tex2D(_MetalAOHeightRough2,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough2));
                float2 _UseParallax2_var = lerp( i.uv0, (0.05*(node_370.b - _Parallax2)*mul(tangentTransform, viewDirection).xy + i.uv0).rg, _UseParallax2 );
                float4 node_3823 = tex2D(_Emissive2,TRANSFORM_TEX(_UseParallax2_var, _Emissive2));
                float4 node_1348 = tex2D(_MetalAOHeightRough1,TRANSFORM_TEX(_UseParallax1_var, _MetalAOHeightRough1));
                float node_9556 = (1.0 - node_1348.b);
                float node_3565 = (_RVertexShift+i.vertexColor.r);
                float node_3330 = saturate((1.0-(1.0-saturate((1.0-((1.0-node_3565)/node_9556))))*(1.0-(_RContrast+saturate(( node_9556 > 0.5 ? (node_3565/((1.0-node_9556)*2.0)) : (1.0-(((1.0-node_3565)*0.5)/node_9556))))))));
                float2 node_3357 = (((-1.0)*_FlowPower)*(i.uv2.rg*1.0+-0.5));
                float4 node_1664 = _Time + _TimeEditor;
                float node_1977 = (node_1664.g*_FlowSpeed);
                float node_9963 = frac(node_1977);
                float2 node_3768 = (i.uv0+(node_3357*node_9963));
                float4 node_968 = tex2D(_Emissive3,TRANSFORM_TEX(node_3768, _Emissive3));
                float2 node_6669 = (i.uv0+(node_3357*frac((node_1977+0.5))));
                float4 node_5676 = tex2D(_Emissive3,TRANSFORM_TEX(node_6669, _Emissive3));
                float node_8239 = 0.5;
                float node_7750 = abs(((node_8239-node_9963)/node_8239));
                float4 node_332 = tex2D(_MetalAOHeightRough2,TRANSFORM_TEX(_UseParallax2_var, _MetalAOHeightRough2));
                float node_3277 = (1.0 - lerp(node_1348.b,node_332.b,node_3330));
                float node_442 = (_GVertexShift+i.vertexColor.g);
                float node_6959 = saturate((1.0-(1.0-saturate((1.0-((1.0-node_442)/node_3277))))*(1.0-(_GContrast+saturate(( node_3277 > 0.5 ? (node_442/((1.0-node_3277)*2.0)) : (1.0-(((1.0-node_442)*0.5)/node_3277))))))));
                o.Emission = lerp(lerp(node_3898.rgb,node_3823.rgb,node_3330),lerp(node_968.rgb,node_5676.rgb,node_7750),node_6959);
                
                float4 node_9733 = tex2D(_AlbedoOpacity1,TRANSFORM_TEX(_UseParallax1_var, _AlbedoOpacity1));
                float3 node_6343 = (node_9733.rgb*_Color1.rgb);
                float4 node_3511 = tex2D(_AlbedoOpacity2,TRANSFORM_TEX(_UseParallax2_var, _AlbedoOpacity2));
                float3 node_555 = lerp(node_6343,lerp(node_6343,(node_3511.rgb*_Color2.rgb),saturate((_Opacity2Shift+node_3511.a))),node_3330);
                float4 AlbedoOpacity1_1 = tex2D(_AlbedoOpacity3,TRANSFORM_TEX(node_3768, _AlbedoOpacity3));
                float4 AlbedoOpacity1_2 = tex2D(_AlbedoOpacity3,TRANSFORM_TEX(node_6669, _AlbedoOpacity3));
                float3 diffColor = lerp(node_555,lerp(node_555,(lerp(AlbedoOpacity1_1.rgb,AlbedoOpacity1_2.rgb,node_7750)*_Color3.rgb),saturate((_Opacity3Shift+AlbedoOpacity1_1.a))),node_6959);
                float specularMonochrome;
                float3 specColor;
                float4 node_3576 = tex2D(_MetalAOHeightRough3,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough3));
                float4 node_9816 = tex2D(_MetalAOHeightRough3,TRANSFORM_TEX(i.uv0, _MetalAOHeightRough3));
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, lerp(lerp(node_1348.r,lerp( _Metallic2, node_332.r, _MetallicMap2 ),node_3330),lerp( _Metallic3, lerp(node_3576.r,node_9816.r,node_7750), _MetallicMap3 ),node_6959), specColor, specularMonochrome );
                float roughness = 1.0 - lerp(lerp(node_1348.a,lerp( (1.0 - _Roughness2), node_332.a, _RoughnessMap2 ),node_3330),lerp( (1.0 - _Roughness3), lerp(node_3576.a,node_9816.a,node_7750), _RoughnessMap3 ),node_6959);
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Standard"
    CustomEditor "ShaderForgeMaterialInspector"
}
