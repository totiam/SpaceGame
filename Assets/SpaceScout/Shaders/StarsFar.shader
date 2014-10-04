
Shader "ZF/Stars_Far" {

	Properties {

		_MainTex ("Diffuse RGB", 2D) = "black" {}

	}

	SubShader {

		Cull front
		
		Pass {
		
			Lighting Off
			SetTexture [_MainTex] { combine texture } 
		
		}

	}

}


