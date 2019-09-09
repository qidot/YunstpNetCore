using System;
namespace yunstp.common.Graph
{
    public class Node<TValue>
    {
        public Vertex<TValue> adjvex; //顶点
        public Node<TValue> next; //下一个邻接点

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        public Node(Vertex<TValue> value)
        {
            adjvex = value;
        }
    }
}
