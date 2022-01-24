Shader "Custom/ZoneShader"
{
	float4x4 WorldViewProj : WORLDVIEWPROJ;
	float4x4 World   : WORLD;
	float4x4 ViewI   : VIEWI;

	texture2D basetexture : DIFFUSE
		<
		string UIName = "Basetexture";
		int Texcoord = 0;
		int MapChannel = 1;
		> ;


		sampler2D BaseSampler = sampler_state
		{
			texture = <basetexture>;
			MinFilter = Linear;
			MagFilter = Linear;
			MipFilter = Linear;

		};


		texture2D Gradienttexture : GRADIENT
			<
			string UIName = "Gradienttexture";
			int Texcoord = 0;
			int MapChannel = 1;
			> ;


			sampler2D GradientSampler = sampler_state
			{
				AddressU = Clamp;
				AddressV = Clamp;
				texture = <Gradienttexture>;
				MinFilter = Linear;
				MagFilter = Linear;
				MipFilter = Linear;

			};



			void VS
			(
				in float4 iPos : POSITION,
				in float4 iNormal : NORMAL,
				in float2 itex : TEXCOORD0,

				out float4 oPos : POSITION,
				out float2 otex : TEXCOORD0,
				out float3 PreOpacity : TEXCOORD1

			)
			{
				float4 WorldPos = mul(iPos, World);
				float3 oNormal = normalize(mul(iNormal, (float3x3)World));
				float3 ViewDirection = normalize(ViewI[3].xyz - WorldPos.xyz);



				PreOpacity = max(0, dot(ViewDirection, oNormal));
				oPos = mul(iPos, WorldViewProj);
				otex = itex;
			}

			void PS
			(
				in float3 itex : TEXCOORD0,
				in float3 PreOpacity : TEXCOORD1,

				out float4 oColor : COLOR
			)
			{

				float3 otex = tex2D(BaseSampler, itex);
				float1 Opacity = tex2D(GradientSampler, PreOpacity).r;
				oColor = float4(otex.rgb, Opacity);

			}

			technique jp13
			{
				pass p0
				{
					AlphaBlendEnable = true;
					SrcBlend = SRCALPHA;
					DestBlend = ONE;

					Vertexshader = compile vs_2_0 VS();
					Pixelshader = compile ps_2_0 PS();
				}

				/*
				 pass p1
				 {
				  AlphaBlendEnable = true;
				  Vertexshader = compile vs_2_0 VS1();
				  Pixelshader = compile ps_2_0 PS1();
				 }*/
			}
}
