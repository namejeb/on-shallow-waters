using System.Collections.Generic;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Behaviour_Tree__1._0_version_{
    public enum NodeState {
        Running,
        Success,
        Failure
    }
    
    public class Node {
        protected NodeState state;

        private Node _parent;
        private List<Node> children = new List<Node>();

        public Node(){
            _parent = null;
        }

        public Node(List<Node> children){
            foreach (Node child in children){
                _Attach(child);
            }
        }

        private void _Attach(Node node){
            node._parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.Failure;
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public void SetData(string key, object value){
            _dataContext[key] = value;
        }
    }
}
