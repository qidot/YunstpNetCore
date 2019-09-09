using System;
namespace yunstp.common.Graph
{
    public class Vertex<TValue>
    {
        public TValue data; // 数据
        public Node<TValue> firstLinkNode; // 第一个邻接节点
        public bool visited; // 访问标志,遍历时使用
        public int inDegree; // 表示该节点入度

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        public Vertex(TValue value)
        {
            data = value;
        }
    }
}
