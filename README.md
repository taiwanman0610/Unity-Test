# Immersive Wisdom's Unity Test

Install Unity Hub for editing the project.

https://unity3d.com/get-unity/download

In Unity Hub, download the Unity version corresponding to the version in

`ProjectSettings/ProjectVersion.txt`

Upon opening the project, navigate to SampleScene and complete the Color Panel by implementing the following:
1. Introduce drag handlers on the top, bottom, left, right, and all 4 corners for resizing the panel.
2. The panel will always be contained in its bounded view.
3. The color UI controls will adjust size appropriately based on resizing of their panel.
4. Implement either the Color Wheel or RGB Slider for managing the camera's color (see below).
5. The controls do not need to precisely match the mockups but they should contain the same capabilities.
6. The controls should change their backdrop color to match what picking at a control position would make the camera color. Options for implementation are as follows:
   1. UIVertex - https://docs.unity3d.com/ScriptReference/UIVertex.html
   2. Dynamic texture creation.
   3. Static textures placed on the UI
7. The panel will be properly profiled and efficient. We are looking for Unity Best Practices here.
8. Usage of external references is allowed. Please annotate where external references are used. I am familiar with almost all of them and will know if code is copy-pasted without attribution.
9. External libraries are allowed but is not preferred and should be done sparingly, if at all. This test is assessing your development, not another developer's. Please annotate and attribute appropriately.

## RGB Slider (Intermediate)
![RGB Slider](References/RGB-Slider.png)

## Color Wheel (Advanced)
![Color Wheel](References/Color-Wheel.png)
