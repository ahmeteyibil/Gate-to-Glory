# Gate To Glory (Prototype / MVP)

**Gate To Glory** is a high-tempo mobile arcade runner that blends lightning-fast mathematical gate decision-making with tactical roguelike deck-building[cite: 2]. This repository contains the Minimum Viable Product (MVP) phase of the project, built to demonstrate core gameplay loops, UI architecture, and object management in Unity.

## 🕹️ Core Mechanics (Current State)
The game currently features a fully functional, synchronized core loop:
* **The Flight Phase:** Players navigate through multiplier and additive gates. 
* **Dynamic HUD:** Real-time player statistics (Damage, Armor, Lifesteal, Crit) are dynamically updated on the left side of the screen.
* **Distance Tracking:** A top-mounted `EnemyDistanceBar` calculates and displays the remaining distance before combat initiation.
* **The Draft Market:** Upon completing the flight phase, players are presented with a functional card draft system to apply passive modifications before facing enemies.

## 🛠️ Technical Architecture
This project focuses on scalable and optimized software engineering practices for mobile development:
* **Engine:** Unity (C#)
* **Asynchronous Process Control:** Utilizes Coroutines and `Action` (Lambda) delegates for non-blocking instantiation, ensuring zero frame drops during heavy object spawning.
* **Data Management:** Leverages custom `ScriptableObjects` to modularize character base stats, gate sets, and card data[cite: 2].
* **UI Optimization:** Built with DOTween pipelines to handle real-time stat modifications and layout transitions without triggering expensive Canvas layout rebuilds.

## 🚀 Roadmap & Future Development
As this is currently an MVP, commercial balance systems are intentionally excluded. The upcoming development pipeline includes:
* **Difficulty Budgeting Algorithm:** Implementing a dynamic scaling system that adjusts enemy stats based on the real-time "Player Power Score"[cite: 2].
* **Economy Balancing:** Refining the mathematical weights of the card market based on player wallet thresholds[cite: 2].
* **Expanded Roster:** Scaling up from the current 2-character prototype to a diverse pool of 15 unique units with distinct passive skills[cite: 2].

## ⚙️ Installation & Setup
1. Clone this repository: `git clone https://github.com/cpplover52/GateToGlory.git`
2. Open the project using **Unity 2022.3 LTS** (or your current version).
3. Navigate to `Scenes/SampleScene` and press Play.