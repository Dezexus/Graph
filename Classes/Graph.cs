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

        public Graph() { }

        #region Properties

        public List<List<short>> GraphAsAlgebraicStructure { get; set; } 
            = new List<List<short>>();

        public List<short> VertexColors { get; private set; } = new List<short>();

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
        public int GetColorVertexByNumber(short vertexNumber) => VertexColors[vertexNumber];

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

        /// <summary>
        /// Конвертирует матрицу смежности в объект типа Graph
        /// </summary>
        /// <param name="matrix">Матрица смежности</param>
        /// <returns>Вовзращает объект типа Graph</returns>
        public static Graph AdjacencyMatrixToGraph(List<List<short>> matrix) {

            var graph = new List<List<short>> {
                new List<short>(){ (short)matrix.Count }
            };  

            for (short i = 0; i < matrix.Count; i++) {
                for (short j = 0; j < matrix.Count; j++) {

                    if (matrix[i][j] == 1)
                        graph.Add(new List<short> { (short)(i + 1), (short)(j + 1) });
                }
            }

            return new Graph(graph);
        }

        /// <summary>
        /// Проверяет, есть в графе рёбра или нет
        /// </summary>
        /// <returns>Возвращает булево значение, где true - есть, false - нет</returns>
        public bool ExistEdges() {

            if (GraphAsAlgebraicStructure == null)
                return false;

            int sum = 0;

            for (int i = 1; i < GraphAsAlgebraicStructure.Count; i++) {

                sum += GraphAsAlgebraicStructure[i][0];
                sum += GraphAsAlgebraicStructure[i][1];
            }
            return (sum != 0);
        }

        /// <summary>
        /// Конвертирует матрицу инцидентности в объект типа Graph
        /// </summary>
        /// <param name="matrix">Матрица инцидентности</param>
        /// <returns>Вовзращает объект типа Graph</returns>
        public static Graph IncidenceMatrixToGraph(List<List<short>> matrix) {

            var graph = new List<List<short>> {
                new List<short>(){ (short)matrix[0].Count }
            };

            for (short i = 0; i < matrix.Count; i++) {
                var tmp = new List<short>();
                for (short j = 0; j < matrix[0].Count; j++) {

                    if (matrix[i][j] == 1)
                        tmp.Add((short)(j + 1));
                }

                if (tmp.Count == 1) {//Если элемент один, то делаем петлю
                    graph.Add(new List<short> { tmp[0], tmp[0] });
                    continue;
                }
                graph.Add(new List<short> { tmp[0], tmp[1] });
            }

            return new Graph(graph);
        }

        #endregion

        #region Operator overloading
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
            var matrix1 = new List<List<short>>(graph1.GraphAsAlgebraicStructure);
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
