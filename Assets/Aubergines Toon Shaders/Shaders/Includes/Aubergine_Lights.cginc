#ifndef AUBERGINE_LIGHTS_INCLUDED
#define AUBERGINE_LIGHTS_INCLUDED

inline fixed4 LightingAub_Toon(SurfaceOutput s, fixed3 lightDir, fixed atten) {
	//Calculate Diffuse Term
	fixed NL = dot(s.Normal, lightDir);
	fixed diff = 0.2 + max(NL, 0);
	fixed3 diffColor = s.Albedo * _LightColor0.rgb * (diff * (atten * 2));
	//Sum up
	fixed4 c;
	//You can try to adjust the multipliers below for your needs
	//Remember to reimport all your shaders which use this lighting function
	//If you make any changes to this file inorder for changes to take effect.
	if (diff < 0.4) c.rgb = diffColor * 0.3; // Low shades multiplier
	else if (diff < 0.7) c.rgb = diffColor * 0.6; //Medium shades multiplier
	else c.rgb = diffColor * 1.3; // Light shades multiplier
	c.a = s.Alpha;
	return c;
}
#endif