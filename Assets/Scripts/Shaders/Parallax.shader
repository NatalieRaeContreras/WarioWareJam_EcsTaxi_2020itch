Shader "Unlit/Parallax"
{
   Properties
   {
       _MainTex("Texture", 2D) = "white" {}
       _Speed("Scroll Speed", Vector) = (0,0,0,0)
       _Spacing("Spacing", Float) = 0
   }
      SubShader
   {
       Tags { "RenderType" = "Opaque" }
       LOD 100

       Pass
       {
           CGPROGRAM
           #pragma vertex vert
           #pragma fragment frag
           // make fog work
           #pragma multi_compile_fog

           #include "UnityCG.cginc"

           struct appdata
           {
               float4 vertex : POSITION;
               float2 uv : TEXCOORD0;
           };

           struct v2f
           {
               float2 uv : TEXCOORD0;
               UNITY_FOG_COORDS(1)
               float4 vertex : SV_POSITION;
           };

           sampler2D _MainTex;
           float4 _MainTex_ST;
           float4 _Speed;
           float _Spacing;

           v2f vert(appdata v)
           {
               v2f o;
               o.vertex = UnityObjectToClipPos(v.vertex);
               o.uv = TRANSFORM_TEX(v.uv, _MainTex);
               UNITY_TRANSFER_FOG(o,o.vertex);
               return o;
           }

           fixed4 frag(v2f i) : SV_Target
           {
              float2 speed = _Time[0] * _Speed;
              float2 uv = 0;
              uv.x = i.uv.x + tan(i.uv.y* 3.14159276) + speed.y;
              uv.y = i.uv.y + speed.x;

              // clip(1 - i.uv.y + 0.25);
              // clip(i.uv.y - 0 + 0.25);

              if ((_MainTex_ST.w - i.uv.y + _Spacing < 0) && ((i.uv.y - (_MainTex_ST.w + 1) + _Spacing) < 0))
              {
                 uv.x = i.uv.y + speed;
                 uv.y = i.uv.x + speed;
              }

              // sample the texture
              fixed4 col = tex2D(_MainTex, uv);

              // apply fog
              UNITY_APPLY_FOG(i.fogCoord, col);
              return col;
          }
          ENDCG
      }
   }
}