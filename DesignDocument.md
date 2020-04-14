# Game Design Document
---
### Rough Sketch for now

## Design Guidelines
---
### Overall System
1. Unity version 2019.3.7f1
2. AR Packages
   1. AR Foundation 3.0.1
   2. AR Subsystems 3.0.0
   3. ARCore 3.0.1
   4. ARKit 3.0.1
3. VS Code
4. Build Settings
   1. Android
      - Remove Vulkan from Graphics APIs
      - Uncheck Multithreaded Rendering
      - Minimum API Level: Android 8.0 "Oreo"
      - Package Name: com.augustluhrs.DYAD_AR
      - Default Orientation: Landscape Right (camera by right hand, opposite left scroll
   2. iOS
      - Package Name: com.augustluhrs.DYAD_AR
      - Camera Usage Description: "Camera is required for AR"
      - target minimum iOS version: 11.0
      - Architecture: ARM64
      - Default Orientation: Landscape Right (camera by right hand, opposite left scroll

### Editor//UI
1. Canvas
   - Canvas Scaler: Scale to Screen Size && Expand
     - Nope that doesn't work either...
