shader_type canvas_item;

uniform vec2 pixelCount = vec2(32);
uniform vec4 outlineColor: source_color = vec4(0,0,0,1);
uniform float outlineWidth = 0.5;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	vec2 uv = floor(UV*pixelCount)/pixelCount;
	
	vec4 colorUp = texture(TEXTURE, (uv+vec2(0,TEXTURE_PIXEL_SIZE.y * pixelCount.y)*outlineWidth));
	vec4 colorDown = texture(TEXTURE, (uv+vec2(0,-TEXTURE_PIXEL_SIZE.y * pixelCount.y)*outlineWidth));
	vec4 colorLeft = texture(TEXTURE, (uv+vec2(TEXTURE_PIXEL_SIZE.x * pixelCount.x,0)*outlineWidth));
	vec4 colorRight = texture(TEXTURE, (uv+vec2(-TEXTURE_PIXEL_SIZE.x * pixelCount.x,0)*outlineWidth));
	
	vec4 outline = colorUp + colorDown + colorLeft + colorRight;
	outline.rgb = outlineColor.rgb;
	outline.a = min(outline.a, 1);
	
	vec4 original = texture(TEXTURE, uv);
	
	COLOR = mix(outline, original, original.a);
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
