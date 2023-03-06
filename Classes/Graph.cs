using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Classes
{
    internal class Graph
    {

        private List<List<short>> graph = new List<List<short>>();
        private List<short> vertexColors = new List<short>();
        private List<short> vertexDegrees = new List<short>();


        public Graph(List<List<short>> _graph) {

            graph = _graph;
            findingDegreeVertex();
        }
        public List<List<short>> getGraph() => graph;
        
        public List<short> getVertexColors() => vertexColors;
        
        public List<short> getVertexDegrees()=> vertexDegrees;
        public int getDegreeVertex(short vertexNumber) => vertexColors[vertexNumber];

        public short getCountVertex() => Convert.ToInt16(graph[0].Count);

        public void findingDegreeVertex() {
        /*
            vertexDegrees.resize(graph[0].size());
            fill(vertexDegrees.begin(), vertexDegrees.end(), 0);
            for (short i = 1; i < graph.size(); i++)
            {
                for (short j = 0; j < graph[0].size(); j++)
                {
                    if (graph[i][0] == j || graph[i][1] == j)
                        vertexDegrees[j]++;
                }
            }*/
        }

    }
}
