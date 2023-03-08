using System;
using System.Collections.Generic;
using System.Linq;

namespace Classes
{
    public class Graph
    {

        public Graph(List<List<short>> _graphAsAlgebraicStructurePage) {

            GraphAsAlgebraicStructure = _graphAsAlgebraicStructurePage;
        }

        #region Properties

        public List<List<short>> GraphAsAlgebraicStructure { get; set; } 
            = new List<List<short>>();

        public List<short> vertexColors { get; private set; } = new List<short>();

        public List<short> VertexDegrees { get; private set; } = new List<short>();

        public short CountVertex { 
            
            get {
               return GraphAsAlgebraicStructure[0][0];
            }
            set {
                GraphAsAlgebraicStructure[0][0] = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Цвет вершины по её номеру
        /// </summary>
        /// <param name="vertexNumber">Номер вершины</param>
        /// <returns>Возвращает цвет вершины</returns>
        public int GetColorVertexByNumber(short vertexNumber) => vertexColors[vertexNumber];

        /// <summary>
        /// Создаёт массив с кол-вом степеней для каждой вершины
        /// </summary>
        public void CountingDegreesVertices() {

            for (int i = 0; i <= CountVertex; i++)
                VertexDegrees.Add(0);

            for (short i = 1; i < GraphAsAlgebraicStructure.Count(); i++)
                for (short j = 0; j <= CountVertex; j++)
                    if (GraphAsAlgebraicStructure[i][0] == j || GraphAsAlgebraicStructure[i][1] == j)
                        VertexDegrees[j]++;
        }

        /// <summary>
        /// Выполняет поиск вершины с максимальной степенью
        /// </summary>
        /// <returns>Возвращает первую вершину с макс степенью</returns>
        public short SearchVertexWithMaxDegree() {

           return (short)VertexDegrees.FindIndex(x => x == VertexDegrees.Max()); 
        }
        /// <summary>
        /// Генирирует новый граф
        /// </summary>
        /// <param name="countVertex">Кол-во вершин графа</param>
        /// <returns>Возращает объект типа Graph</returns>
        public Graph GenerationGraph(int countVertex) {

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

	        return new Graph(graph);
        }
        #endregion

        #region Operator
        /// <summary>
        /// Объединяет два графа
        /// </summary>
        /// <param name="graph1">Граф 1</param>
        /// <param name="graph2">Граф 2</param>
        /// <returns>Возвращяет новый граф</returns>
        public static Graph operator +(Graph graph1, Graph graph2) {

            graph1.CountingDegreesVertices();
            graph2.CountingDegreesVertices();
            short vertexCount1 = graph1.CountVertex;
            var matrix1 = new List<List<short>>(graph1.GraphAsAlgebraicStructure.);
            var matrix2 = new List<List<short>>(graph2.GraphAsAlgebraicStructure);

            matrix1[0][0] = (short)(graph1.CountVertex + graph2.CountVertex);

            for (short i = 1; i < matrix2.Count; i++) { 

                short vertex1 = (short)(matrix2[i][0] + vertexCount1);
                short vertex2 = (short)(matrix2[i][1] + vertexCount1);
                matrix1.Add(new List<short>{ vertex1, vertex2});
            }
            short vertex3 = graph1.SearchVertexWithMaxDegree();
            short vertex4 = (short)(graph2.SearchVertexWithMaxDegree() + vertexCount1);
            matrix1.Add(new List<short> {vertex3 , vertex4});

            return new Graph(matrix1);
        }

        #endregion

    }
}
