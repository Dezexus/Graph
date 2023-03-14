using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Classes
{
    public class Graph
    {

        #region Fields

        private List<List<short>> adjList;
        private List<short> colors = new List<short>();
        private Dictionary<short, short> result = new Dictionary<short, short>();

        #endregion

        public Graph(List<List<short>> _graphAsAlgebraicStructurePage) {

            GraphAsAlgebraicStructure = _graphAsAlgebraicStructurePage;
        }

        public Graph() { }

        #region Properties

        public List<List<short>> GraphAsAlgebraicStructure { get; set; } 
            = new List<List<short>>();

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

            if (matrix is null)
                return null;

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
        /// Конвертирует матрицу инцидентности в объект типа Graph
        /// </summary>
        /// <param name="matrix">Матрица инцидентности</param>
        /// <returns>Вовзращает объект типа Graph</returns>
        public static Graph IncidenceMatrixToGraph(List<List<short>> matrix) {

            if (matrix is null)
                return null;

            var graph = new List<List<short>> {
                new List<short>(){ (short)matrix.Count }
            };

            for (short i = 0; i < matrix[0].Count; i++) {
                var tmp = new List<short>();
                for (short j = 0; j < matrix.Count; j++) {

                    if (matrix[j][i] == 1)
                        tmp.Add((short)(j + 1));
                }

                if (tmp.Count == 1) {//Если элемент один, то делаем петлю
                    graph.Add(new List<short> { tmp[0], tmp[0] });
                    continue;
                }

                if (tmp.Count != 0)
                    graph.Add(new List<short> { tmp[0], tmp[1] });
            }

            return new Graph(graph);
        }

        /// <summary>
        /// Создаёт список смежности графа
        /// </summary>
        private void CreateAdjList() {

            // изменить размер вектора, чтобы он содержал `n` элементов типа `vector<int>`
            adjList = new List<List<short>>();
            for (int i = 0; i <= CountVertex; i++)
                adjList.Add(new List<short>());

            // добавляем ребра в неориентированный graph
            for (int i = 1; i < GraphAsAlgebraicStructure.Count; i++) {
                adjList[GraphAsAlgebraicStructure[i][0]].Add(GraphAsAlgebraicStructure[i][1]);
                adjList[GraphAsAlgebraicStructure[i][1]].Add(GraphAsAlgebraicStructure[i][0]);
            }

        }

        /// <summary>
        /// Раскрышивает вершины графа
        /// </summary>
        public void ColorGraph() {

            CreateAdjList();
            for (short i = 1; i <= CountVertex; i++)
                colors.Add(i);
            // отслеживаем цвет, присвоенный каждой вершине
            result = new Dictionary<short, short>();

            // назначаем цвет вершине одну за другой
            for (short u = 1; u <= CountVertex; u++) {
                // устанавливаем для хранения цвета смежных вершин `u`
                HashSet<short> assigned = new HashSet<short>();

                // проверяем цвета смежных вершин `u` и сохраняем их в наборе
                foreach (var i in adjList[u]) {
                    if (result.ContainsKey(i))
                        assigned.Add(result[i]);
                }

                // проверяем первый свободный цвет
                short color = 1;
                foreach (var c in assigned) {
                    if (color != c)
                        break;
                    color++;
                }

                // назначаем вершине `u` первый доступный цвет
                result[u] = color;
            }
        }

        public short GetColorNumberByVertexNumber(short vertexNumber) => colors[result[vertexNumber]];

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
            short vertexCount = graph1.CountVertex;
            var matrix = new List<List<short>> {
                new List<short>() { (short)(graph1.CountVertex + graph2.CountVertex) }
            };

            for (short i = 1; i < graph1.GraphAsAlgebraicStructure.Count; i++) {

                short vertex1 = graph1.GraphAsAlgebraicStructure[i][0];
                short vertex2 = graph1.GraphAsAlgebraicStructure[i][1];
                matrix.Add(new List<short> { vertex1, vertex2 });
            }

            for (short i = 1; i < graph2.GraphAsAlgebraicStructure.Count; i++) {

                short vertex1 = (short)(graph2.GraphAsAlgebraicStructure[i][0] + vertexCount);
                short vertex2 = (short)(graph2.GraphAsAlgebraicStructure[i][1] + vertexCount);
                matrix.Add(new List<short> { vertex1, vertex2 });
            }
            short vertex3 = graph1.SearchVertexWithMaxDegree();
            short vertex4 = (short)(graph2.SearchVertexWithMaxDegree() + vertexCount);
            matrix.Add(new List<short> { vertex3, vertex4 });

            return new Graph(matrix);
        }

        #endregion

    }
}
