
// basicly this is a very simple shader, no per-pixel calculations. It is surely work on very old desktop machines,
// and should mostly work on current entry-level mobiles. The default subshader may incompatible on a few
// simple android phones like the Alcatel OT-990, but the second one will work well instead.
//
// the shaders main texture stand for albedo(RGB) and mask(A) for the spheremap.
// the spheremap act as environment map for specular reflections.
// Simple vertex light calculations used, only with the Dissue and Ambient components.
// the self illum map just added to the result.

Shader "ZF/ReflectiveSelfIllumDiffuse" { 

	Properties { 

		_MainTex ("Diffuse RGB, Gloss A", 2D) = "white" 
		_EnvMap ("EnvMap RGB", 2D) = "white" { TexGen SphereMap }
		_SelfIllum ("SelfIllum RGB", 2D) = "black" { }

	} 

	// 4 textures, maybe incompatible on mobile
	SubShader {
	
		LOD 20
		
		Pass { 
		
            Material {
			
                Diffuse (1,1,1,1)
                Ambient (1,1,1,1)
				
	         }
			 
            Lighting On
   
			SetTexture [_MainTex] { 
					
				combine texture * primary DOUBLE, texture

			}

			SetTexture [_EnvMap] { 
			
				combine previous, texture * previous
				
			} 

			SetTexture [_EnvMap] { 
					
				combine texture lerp(previous alpha) previous

			}

			SetTexture [_SelfIllum] { 
			
				combine previous + texture 
				
			} 

		}

	}
	
	// 2 textures, works well on mobile
	SubShader {
	
		Pass { 
		
            Material {
			
                Diffuse (1,1,1,1)
                Ambient (1,1,1,1)
				
	         }
			 
            Lighting On
   
			SetTexture [_MainTex] { 
					
				combine texture * primary DOUBLE, texture

			}

			SetTexture [_SelfIllum] { 
			
				combine previous + texture 
				
			} 

		}

	}	

}