namespace BehaviorTree {
    public class BehaviorTree {
        protected Behavior m_treeRoot;

        public BehaviorTree(Behavior _treeRoot) {
            m_treeRoot = _treeRoot;
        }

        public void Update() {
            m_treeRoot.Update();
        }
    }
}
