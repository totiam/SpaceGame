
Shader "ZF/Stars_Close" {

	Properties {

		_MainTex ("Diffuse RGBA", 2D) = "black" {}

	}

	SubShader {

		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off
		Cull front
		Blend SrcAlpha OneMinusSrcAlpha 

		Pass {
		
			Lighting Off
			SetTexture [_MainTex] { combine texture } 
		
		}

	}

}


