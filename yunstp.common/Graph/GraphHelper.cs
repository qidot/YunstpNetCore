using System;
using System.Collections.Generic;

namespace yunstp.common.Graph
{
    /// <summary>
    /// 图操作辅助类
    /// </summary>
    public class GraphHelper
    {
        /// <summary>
        /// 检测有向图是否有闭环回路
        /// </summary>
        /// <param name="originalData">初始数据：逗号分割的from跟to字符串集合</param>
        /// <returns></returns>
        public static bool CheckDigraphLoop(List<string> originalData)
        {
            AdjacencyList<string> adjacencyList = new AdjacencyList<string>();
            string fromData = string.Empty;
            string toData = string.Empty;

            //构造有向图的邻接表表示
            originalData.ForEach(it =>
            {
                fromData = it.Split(',')[0]; //得到from顶点数据
                toData = it.Split(',')[1];   //得到to定点数据
                adjacencyList.AddVertex(fromData);
                adjacencyList.AddVertex(toData);

                var fromVertex = adjacencyList.Find(fromData); // 找到起始顶点
                var toVertex = adjacencyList.Find(toData); // 找到目标顶点
                toVertex.inDegree++; //目标顶点的入度+1
                adjacencyList.AddDirectedEdge(fromVertex, toVertex); //添加有向边
            });

            return adjacencyList.TopologicalSort();
        }
    }
}
