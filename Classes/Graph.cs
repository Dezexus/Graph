using System;
using System.Collections.Generic;
using System.Linq;

namespace Classes
{
    public class Graph
    {

        #region Field

        
        private List<short> vertexColors = new List<short>();
        private List<short> vertexDegrees = new List<short>();

        #endregion

        public Graph(List<List<short>> _graphAsAlgebraicStructurePage) {

            GraphAsAlgebraicStructure = _graphAsAlgebraicStructurePage;
        }

        #region Getters

        public List<List<short>> GraphAsAlgebraicStructure { get; set; } 
            = new List<List<short>>();

        public List<short> GetVertexColors() => vertexColors;

        public List<short> GetVertexDegrees() => vertexDegrees;

        public int GetDegreeVertex(short vertexNumber) => vertexColors[vertexNumber];

        public short GetCountVertex() => GraphAsAlgebraicStructure[0][0];

        #endregion

        #region Methods

        public void CountingDegreesVertices() {

            //vertexDegrees = new List<short>(n);
            for (int i = 0; i < GraphAsAlgebraicStructure[0][0]; i++)
                vertexDegrees.Add(0);



            for (short i = 1; i < GraphAsAlgebraicStructure.Count(); i++)
                for (short j = 0; j < GraphAsAlgebraicStructure[0][0]; j++)
                    if (GraphAsAlgebraicStructure[i][0] == j || GraphAsAlgebraicStructure[i][1] == j)
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

        #region Operator

        public static Graph operator +(Graph graph1, Graph graph2) {

            graph1.CountingDegreesVertices();
            graph2.CountingDegreesVertices();
            short countVertex1 = graph1.GetCountVertex();
            List<List<short>> matrix1 = graph1.GraphAsAlgebraicStructure;
            List<List<short>> matrix2 = graph2.GraphAsAlgebraicStructure;

            matrix1[0][0] = (short)(graph1.GraphAsAlgebraicStructure[0][0] + graph2.GraphAsAlgebraicStructure[0][0]);

            for (short i = 1; i < matrix2.Count; i++) { 

                short vertex1 = (short)(matrix2[i][0] + countVertex1);
                short vertex2 = (short)(matrix2[i][1] + countVertex1);
                matrix1.Add(new List<short>{ vertex1, vertex2});
            }

            return new Graph(matrix1);
        }

        #endregion

        public List<List<short>> GenerationGraph(int countVertex) {

            var rnd = new Random();

            var graph = new List<List<short>>();
            int countEdge = countVertex - 1 + rnd.Next(1, (((countVertex - 1) * countVertex) / 2 + 1));
            var vertex = new List<short>();
            for (short i = 0; i < countVertex; i++)
                vertex.Add(i);
            graph.Add(vertex);

            for (short i = 0; i < countEdge; i++) { 
            
                short firsVertex = (short)rnd.Next(0, countVertex);
                short secondVertex = (short)rnd.Next(0, countVertex);
                graph.Add(new List<short>{ firsVertex, secondVertex });
            }

	        return graph;
        }

    }
}
