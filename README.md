# Survival Horror Prototype (WIP – Solmoria)

This project is a personal Unity prototype focused on building scalable gameplay and UI systems for a survival horror experience.

The goal is not just to implement features, but to explore how systems can be structured in a clean, modular, and extensible way.

---

## Core Systems

### UI & Navigation
- Title screen with functional navigation and audio feedback
- InputRouter system to manage UI control context (e.g. pause menu, inventory, title screen)
- Prevents input conflicts by ensuring only one system handles navigation at a time

### Player Controller
- Third-person movement using Cinemachine camera
- Movement on X/Z axes with camera-relative controls
- (Currently improving diagonal speed normalization and smoothing)

### Inventory System
- Data-driven item database
- World items linked to database entries via simple components
- Items are added to inventory upon interaction
- Inventory UI currently being refactored into an MVC structure

### Item Combination System
- Prototype system for combining items into new ones
- Designed to support rule-based combinations (planned via dictionary or similar structure)
- Example: combining a spray can + cloth → new crafted item

### Interaction System
- Modular interaction setup using interfaces:
  - `IInteractable`
  - `IObtainable`
- Supports flexible expansion of interactable object types

### Conditional Item Pickup
- Items can require specific conditions before being collected
- Designed as a modular system to allow easy extension of new conditions

### Cinematics & Feedback
- Intro cinematic sequence
- In-game cinematic triggered by item-based conditions
- Audio feedback implemented in UI and interactions

---

## Architecture Focus

This project is being built with an emphasis on:

- Separation of concerns (moving toward MVC for UI)
- Modular systems that can scale
- Reusable gameplay logic
- Clean input handling across multiple systems

---

## Where to Look

If you're reviewing the project, these are key areas:

- InputRouter → UI control flow and input ownership
- Inventory System → data-driven structure and item handling
- Conditional Pickup System → modular condition checks
- UI Navigation → custom navigation independent of Unity defaults

---

## Tech

- Unity (C#)
- Unity Input System
- Cinemachine

---

## Notes

This is an ongoing work in progress. Some systems are still being refined, particularly UI architecture and gameplay polish.

---

## License

This project is proprietary and all rights are reserved.  
See the LICENSE file for full details.
