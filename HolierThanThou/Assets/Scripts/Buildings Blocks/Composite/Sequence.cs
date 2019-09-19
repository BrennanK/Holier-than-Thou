﻿namespace BehaviorTree {
    /// <summary>
    /// <para>Sequences execute each of their child behaviors in sequence until all of the children have executed successfully or one of them has failed.</para>
    /// <para>Sequences allows Behavior Trees to follow plans that are specified by the designers.</para>
    /// </summary>
    public class Sequence : Composite {
        public Sequence(string _nodeName) : base(_nodeName) {
            // TODO
        }

        /// <summary>
        /// Runs all children Update() functions until one of them fails or is still running
        /// </summary>
        /// <returns>Success when all children returned success, otherwise, return the child status that was different than success.</returns>
        public override EReturnStatus Update() {
            // The next child behavior is processed immediately after the previous one succeeds.
            // This is important so the BT does not miss a frame before having found a low-level action to perform.
            foreach(Behavior childBehavior in m_childrenBehaviors) {
                EReturnStatus childStatus = childBehavior.Update();

                if (childStatus != EReturnStatus.SUCCESS) {
                    return childStatus;
                }
            }

            return EReturnStatus.SUCCESS;
        }
    }
}
