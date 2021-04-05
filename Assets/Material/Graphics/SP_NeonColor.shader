// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Neon Color"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_Frequency ("Frequency", float) = 2
		_Vue ("Vue", float) = 1
		_Saturation ("Saturation", float) = 1
		_Value ("Value", float) = 1
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
			float _Frequency;
			float _Vue;
			float _Saturation;
			float _Value;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed3 rgb2hsv(fixed3 rgb){
				fixed3 hsv;
				hsv.z = max(max(rgb.r, rgb.g), rgb.b);
				fixed minValue = min(max(rgb.r, rgb.g), rgb.b);
				fixed delta = hsv.z - minValue;
				if(hsv.z > 0){
					hsv.y = delta / hsv.z;
				}
				else{
					hsv.y = 0;
				}

				if(hsv.z == rgb.r){
					hsv.x = (rgb.g - rgb.b) / delta;
				}
				else if(hsv.z == rgb.g){
					hsv.x = 2 + (rgb.b - rgb.r) / delta;
				}
				else if(hsv.z == rgb.b){
					hsv.x = 4 + (rgb.g - rgb.b) / delta;
				}

				hsv.x /= 6.0;
				if(hsv.x < 0){
					hsv.x += 1;
				}

				return hsv;
			}

			fixed3 hsv2rgb(fixed3 hsv){
				fixed3 rgb;
				if(hsv.y == 0){
					rgb.r = rgb.g = rgb.b = hsv.z;
					return rgb;
				}
				fixed delta = hsv.y * hsv.z;
				fixed h6 = hsv.x * 6;
				fixed f = floor(h6);
				fixed minValue = hsv.z - delta;
				fixed middlePos = delta * (h6 - f) + minValue;
				fixed middleNeg = delta * (1 - (h6 - f)) + minValue;
				
				if(f < 1){
					rgb.r = hsv.z;
					rgb.g = middlePos;
					rgb.b = minValue;
				}
				else if(f < 2){
					rgb.g = hsv.z;
					rgb.b = minValue;
					rgb.r = middleNeg;
				}
				else if(f < 3){
					rgb.g = hsv.z;
					rgb.b = middlePos;
					rgb.r = minValue;
				}
				else if(f < 4){
					rgb.b = hsv.z;
					rgb.r = minValue;
					rgb.g = middleNeg;
				}
				else if(f < 5){	
					rgb.b = hsv.z;
					rgb.r = middlePos;
					rgb.g = minValue;
				}
				else{
					rgb.r = hsv.z;
					rgb.g = minValue;
					rgb.b = middleNeg;
				}

				return rgb;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				// c.rgb = sin(_Frequency * _Time) * c.brg;
				fixed3 hsv = rgb2hsv(c.rgb);
				// fixed3 hsv = fixed3(1, 0.8, 0.5);
				hsv.x = abs(sin(_Frequency * _Time));
				c.rgb = hsv2rgb(hsv);

				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}