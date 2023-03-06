using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Classes
{
    internal class Graph
    {

        private List<List<short>> graph = new List<List<short>>();
        private List<short> vertexColors = new List<short>();
        private List<short> vertexDegrees = new List<short>();


        public Graph(List<List<short>> _graph) {

            graph = _graph;
            CountingDegreesVertices();
        }

        public Graph() { } //Временный конструктор по умолчанию

        #region Getters

        public List<List<short>> GetGraph() => graph;

        public List<short> GetVertexColors() => vertexColors;

        public List<short> GetVertexDegrees() => vertexDegrees;

        public int GetDegreeVertex(short vertexNumber) => vertexColors[vertexNumber];

        public short GetCountVertex() => Convert.ToInt16(graph[0].Count);

        #endregion

        #region Methods

        public void CountingDegreesVertices() {

            vertexDegrees = new List<short>(graph[0].Count);
           
            for (short i = 1; i < graph.Count(); i++)
                for (short j = 0; j < graph[0].Count(); j++)
                    if (graph[i][0] == j || graph[i][1] == j)
                        vertexDegrees[j]++;
        }

        public short SearchVertexWithMaxDegree() {

            List<short> DegreeAndVertex = new List<short> { 1, 0 };

            for (short i = 0; i < vertexDegrees.Count(); i++)
                if (vertexDegrees[i] > DegreeAndVertex[0])
                    DegreeAndVertex = new List<short> { DegreeAndVertex[0]++, i };

            return DegreeAndVertex[1];
        }

        #endregion

        public static Graph operator +(Graph graph1, Graph graph2) {
		


            return new Graph();
        }
    }
}
