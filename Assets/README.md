# MyRobot

## Overview
This project is an attempt to simulate realistic movement for a two-wheeled robot within a 3D environment.

## Requirements
- Unity version 2022.3.4f1 or later

## Usage
The project initializes with the 'SampleScene.' To operate the robot:
1. Start the game; you'll be in a scene where you can interact with the floor.
2. Click on any location on the floor, and a ghost robot will appear, indicating the target point.
3. Drag your mouse; the ghost robot will rotate.
4. Release the right mouse button, and the actual robot begins moving towards the designated position and orientation.
5. Subsequent clicks set new destinations for the robot, without the need to complete the previous path.

## Code Structure
- `Scripts/`: Contains the core classes. `Controller` manages the robot's movements, `PointerInput` handles mouse interactions, and `FollowRobot` ensures a top-down camera perspective.
- `Scenes/`: The project utilizes a single 'SampleScene.'
- `Assets/`: Assets include downloaded elements like wheels and custom-created directional triangles i did with Photoshop. A third-person camera setup is available for an up-close view of the robot.

## Acknowledgments
- 3D model of wheels sourced from Baggi on Sketchfab: [Wheels Baggi](https://sketchfab.com/3d-models/wheels-baggi-34a3e4e103754887b7f3c1cf3b4778eb)

## Contact
Eirini Kolokytha  
Email: [eir.k.olokytha@gmail.com](mailto:eir.k.olokytha@gmail.com)
