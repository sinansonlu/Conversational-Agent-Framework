v1.0
Initial release.

HOW TO USE:
Aubergine_Lights.cginc file has a lighting function which does the cel shading effect.
Take any surface shader, include this file:
//Absolute path
#include "Assets/Aubergine/Shaders/Includes/Aubergine_Lights.cginc"
//Or you can use relative path as below, whatever suits you
//#include "../../../Includes/Aubergine_Lights.cginc"

and change the lighting to AUB_Toon (#pragma surface surf Aub_Toon)
and there you will have cel shading applied.

FUTURE UPDATES:
Future updates will bring a simple shader which uses 1 artificial light source(Vector3 direction)
to calculate all lighting. And a PRO only image effect to do edge detection and toony shadows.

KNOWN ISSUES:
None

Support:
For any questions, you can contact me from aubergine2010@gmail.com.

Thanks for buying the package.