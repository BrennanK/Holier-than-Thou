using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGOAP : MonoBehaviour
{
    // Use Idea of State Machine to build this basic AI (Actually, GOAP)
    // It include two branches:
        // 1. Pathfinder: AI is tryig to arrive the target destination.
        // 2. Aggresion: AI is attacking another ball, player or AI.
    // Stage -> Goal (AI features/characters) -> Action -> Effets
        // Stage: No Destination (Game Start)
            // Sample Goal: Get the points
            // Action: Find a Path
            // Effects: (x)havaDestination
        // Stage: (x)havaDestination
            // Goal: Get a point
            // Action: Go!
            // Effects: Untill get one point
        // Stage: detect others(ball)
            // Goal: expulse others
            // Action: Calculate the next location for the enemy -> SHOOT!
            // effect:
                // 1. cannot detect another ball -> go back to find path
                // 2. it is stil there -> hit it again
        // Stage: Use power ups.
        // Stage: To jump
    // Methods:
        // State Check
            // detect any enemies - bool
            // Have a path - bool
        // Action
            // Path Find
            // Renew Stage
        // Result Check
        // 

}
