using System;
using System.Collections.Generic;

namespace yunstp.common.Graph
{
    public class AdjacencyList<T>
    {
        List<Vertex<T>> items; // 图的顶点集合

        /// <summary>
        /// 构造函数
        /// </summary>
        public AdjacencyList()
        {
            items = new List<Vertex<T>>();
        }

        /// <summary>
        /// 添加一个顶点
        /// </summary>
        /// <param name="item"></param>
        public void AddVertex(T item)
        {   // 顶点不存在
            if (!Contains(item))
            {
                items.Add(new Vertex<T>(item));
            }
        }

        /// <summary>
        /// 添加无向边
        /// </summary>
        /// <param name="from">头顶点</param>
        /// <param name="to">尾顶点</param>
        public void AddEdge(T from, T to)
        {
            Vertex<T> fromVer = Find(from); //找到起始顶点
            if (fromVer == null)
                throw new ArgumentException("头顶点并不存在！");

            Vertex<T> toVer = Find(to); //找到结束顶点
            if (toVer == null)
                throw new ArgumentException("尾顶点并不存在！");

            //无向图的两个顶点都需记录边信息，有向图只需记录单边信息
            //即无相图的边其实就是两个双向的有向图边
            AddDirectedEdge(fromVer, toVer);
            AddDirectedEdge(toVer, fromVer);
        }

        /// <summary>
        /// 查找图中是否包含某项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T data)
        {
            foreach (Vertex<T> v in items)
            {
                if (v.data.Equals(data))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 根据顶点数据查找顶点
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public Vertex<T> Find(T data)
        {
            foreach (Vertex<T> v in items)
            {
                if (v.data.Equals(data))
                    return v;
            }
            return null;
        }

        /// <summary>
        /// 添加有向边
        /// </summary>
        /// <param name="fromVer">头顶点</param>
        /// <param name="toVer">尾顶点</param>
        public void AddDirectedEdge(Vertex<T> fromVer, Vertex<T> toVer)
        {
            if (fromVer.firstLinkNode == null) //无邻接点时，当前添加的尾顶点就是firstLinkNode
            {
                fromVer.firstLinkNode = new Node<T>(toVer);
            }
            else // 该头顶点已经存在邻接点，则找到该头顶点链表最后一个Node，将toVer添加到链表末尾                              
            {
                Node<T> tmp, node = fromVer.firstLinkNode;
                do
                {   // 检查是否添加了重复有向边
                    if (node.adjvex.data.Equals(toVer.data))
                    {
                        throw new ArgumentException("添加了重复的边！");
                    }
                    tmp = node;
                    node = node.next;
                } while (node != null);
                tmp.next = new Node<T>(toVer); //添加到链表未尾
            }
        }

        /// <summary>
        /// 拓扑排序是否能成功执行
        /// 对有向图来说，如果能够用拓扑排序完成对图中所有节点的排序的话，就说明这个图中没有环，而如果不能完成，则说明有环。
        /// </summary>
        /// <returns></returns>
        public bool TopologicalSort()
        {
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>(); // 定义栈
            items.ForEach(it =>    // 循环顶点集合，将入度为0的顶点入栈
            {
                if (it.inDegree == 0)
                    stack.Push(it);         //入度为0的顶点入栈
            });
            int count = 0;   // 定义查找到的顶点总数
            while (stack.Count > 0)
            {
                Vertex<T> t = stack.Pop();  // 出栈
                count++;
                if (t.firstLinkNode != null)
                {
                    Node<T> tmp = t.firstLinkNode;
                    while (tmp != null)
                    {
                        tmp.adjvex.inDegree--;  // 邻接点入度-1
                        if (tmp.adjvex.inDegree == 0) // 如果邻接点入度为0，则入栈
                            stack.Push(tmp.adjvex);
                        tmp = tmp.next; // 递归所有邻接点
                    }
                }
            }
            if (count < items.Count) // 找到的结果数量小于图顶点个数相同，表示拓扑排序失败，表示有闭环
            {
                return false;
            }
            return true;
        }
    }
}
