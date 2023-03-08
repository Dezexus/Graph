#include <iostream>
#include <stdlib.h>
#include <vector>
#include <time.h>
#include <windows.h>

using namespace std;

class Graph {
	vector <vector<int>> graph;
	vector <int> vertexColors;
	vector <int> vertexDegrees;

public:
	Graph(vector <vector<int>> graph) {
		this->graph = graph;
		findingDegreeVertex();
	}
	vector <vector <int>> getGraph() {
		return graph;
	}
	vector <int> getVertexColors() {
		return vertexColors;
	}
	vector <int> getVertexDegrees() {
		return vertexDegrees;
	}
	int getDegreeVertex(int vertexNumber) {
		return vertexDegrees[vertexNumber];
	}
	int getCountVertex() {
		return graph[0].size();
	}
	void findingDegreeVertex() {
		vertexDegrees.resize(graph[0].size());
		fill(vertexDegrees.begin(), vertexDegrees.end(), 0);
		for (int i = 1; i < graph.size(); i++) {
			for (int j = 0; j < graph[0].size(); j++) {
				if (graph[i][0] == j || graph[i][1] == j)
					vertexDegrees[j]++;
			}
		}
	}
	int searchVertexWithMaxDegree() {
		vector <int> DegreeAndVertex = { 0, 0 };
		for (int i = 0; i < vertexDegrees.size(); i++) {
			if (vertexDegrees[i] > DegreeAndVertex[0]) {
				DegreeAndVertex = { vertexDegrees[i], i };
			}
		}
		return DegreeAndVertex[1];
	}
	Graph operator + (Graph& graphTwo) {
		int countVertex1 = getCountVertex();
		vector <vector <int>> graph3 = graph;
		vector <vector <int>> graph2 = graphTwo.getGraph();
		for (int i = 0; i < graph[0].size(); i++) {
			graph3[0].push_back(i + countVertex1);
		}
		for (int i = 1; i < graph2.size(); i++) {
			graph3.push_back({ graph2[i][0] + countVertex1, graph2[i][1] + countVertex1 });
		}
		graph3.push_back({ searchVertexWithMaxDegree(), graphTwo.searchVertexWithMaxDegree() + countVertex1 });
		return Graph(graph3);
	}
};

vector <vector <int>> generationGraph(int countVertex) {
	srand(time(0));
	vector <vector <int>> graph;
	int countEdge = countVertex - 1 + rand() % (((countVertex - 1) * countVertex) / 2 + 1);
	vector <int> vertex;
	for (int i = 0; i < countVertex; i++)
		vertex.push_back(i);
	graph.push_back(vertex);
	for (int i = 0; i < countEdge; i++) {
		int firsVertex = 0 + rand() % countVertex;
		int secondVertex = 0 + rand() % countVertex;
		graph.push_back({ firsVertex, secondVertex });
	}
	return graph;
}

void graphOutput(Graph& graph) {
	for (int i = 0; i < graph.getGraph().size(); i++) {
		for (int j = 0; j < graph.getGraph()[i].size(); j++) {
			cout << graph.getGraph()[i][j] << " ";
		}
		cout << endl;
	}

}

int main()
{
	//vector <vector <int>> incomingGraph = { {0, 1, 2, 3, 4}, {0, 1}, {0, 2}, {2, 1}, {2, 3} };
	int countVertex1, countVertex2;
	cin >> countVertex1 >> countVertex2;
	Graph graph1(generationGraph(countVertex1));
	Sleep(801);
	Graph graph2(generationGraph(countVertex2));
	graphOutput(graph1);
	cout << endl << endl;
	graphOutput(graph2);
	cout << endl << endl;
	Graph graph3 = graph1 + graph2;
	graphOutput(graph3);

}


