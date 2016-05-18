using System;
using System.Collections.Generic;
using CPI.Graphing.GraphingEngine.Contracts.Dc;
using MiddleConsumer.Property;

namespace MiddleConsumer.Factory
{
    public interface IGraphFactory
    {
        IGraph Create();
    }
    public class GraphFactory : IGraphFactory
    {
        #region Declarations

        private static IList<INode> NodeList = new List<INode>();
        private static IList<IEdge> EdgeList = new List<IEdge>();

        #endregion Declarations 
        public IGraph Create()
            {
                assignNodeList();
                assignEdgeList();
                IGraph graph = new Graph() { NodeList = NodeList, EdgeList = EdgeList };
                return graph;
            }

            private Node statNode = new Node();
            private Node firstNode = new Node();
            private Node secondNode = new Node();
            private Node thirdNode = new Node();
            private Node fourthNode = new Node();
            private Node finishNode = new Node();
            private IEdge edgeA = new Task();
            private IEdge edgeB = new Task();
            private IEdge edgeC = new Task();
            private IEdge edgeD = new Task();
            private IEdge edgeE = new Task();
            private IEdge edgeF = new Task();
            private void assignNodeList()
            {
                NodeList.Add(statNode);
                NodeList[0].Id = 0;
                NodeList[0].Name = "Start";
                NodeList[0].HeadList.Add(edgeA);
                NodeList[0].TailList = new List<IEdge>();

                NodeList.Add(firstNode);
                NodeList[1].Id = 1;
                NodeList[1].Name = "First";
                NodeList[1].HeadList.Add(edgeB); NodeList[0].HeadList.Add(edgeC);
                NodeList[1].TailList.Add(edgeA);

                NodeList.Add(secondNode);
                NodeList[2].Id = 2;
                NodeList[2].Name = "Second";
                NodeList[2].HeadList.Add(edgeD);
                NodeList[2].TailList.Add(edgeB);

                NodeList.Add(thirdNode);
                NodeList[3].Id = 3;
                NodeList[3].Name = "Third";
                NodeList[3].HeadList.Add(edgeE);
                NodeList[3].TailList.Add(edgeC);

                NodeList.Add(fourthNode);
                NodeList[4].Id = 4;
                NodeList[4].Name = "Fourth";
                NodeList[4].HeadList.Add(edgeF);
                NodeList[4].TailList.Add(edgeE); NodeList[4].TailList.Add(edgeD);

                NodeList.Add(finishNode);
                NodeList[5].Id = 5;
                NodeList[5].Name = "Finish";
                NodeList[5].HeadList = new List<IEdge>();
                NodeList[5].TailList.Add(edgeF);
            }

            private void assignEdgeList()
            {
                EdgeList.Add(edgeA);
                EdgeList[0].Id = 0;
                EdgeList[0].Name = "A";
                EdgeList[0].HeadNode = statNode;
                EdgeList[0].TailNode = firstNode;
                EdgeList[0].DependsOnList = new List<IEdge>();
                var dependentList_A = new List<IEdge>();
            dependentList_A.Add(edgeB); dependentList_A.Add(edgeC);
            EdgeList[0].DependentList = dependentList_A;
                (EdgeList[0] as Task).Timing.Duration = 1;
                (EdgeList[0] as Task).Timing.Est = 0;

                EdgeList.Add(edgeB);
                EdgeList[1].Id = 1;
                EdgeList[1].Name = "B";
                EdgeList[1].HeadNode = firstNode;
                EdgeList[1].TailNode = secondNode;
                EdgeList[1].DependsOnList.Add(edgeA);
            var dependentList_B = new List<IEdge>();
            dependentList_B.Add(edgeD);
            EdgeList[1].DependentList = dependentList_B;
            (EdgeList[1] as Task).Timing.Duration = 1;
                (EdgeList[1] as Task).Timing.Est = 1;

                EdgeList.Add(edgeC);
                EdgeList[2].Id = 2;
                EdgeList[2].Name = "C";
                EdgeList[2].HeadNode = firstNode;
                EdgeList[2].TailNode = thirdNode;
                EdgeList[2].DependsOnList.Add(edgeA);
            var dependentList_C = new List<IEdge>();
            dependentList_C.Add(edgeE);
            EdgeList[2].DependentList = dependentList_C;
            (EdgeList[2] as Task).Timing.Duration = 1;
                (EdgeList[2] as Task).Timing.Est = 1;

                EdgeList.Add(edgeD);
                EdgeList[3].Id = 3;
                EdgeList[3].Name = "D";
                EdgeList[3].HeadNode = secondNode;
                EdgeList[3].TailNode = fourthNode;
                EdgeList[3].DependsOnList.Add(edgeB);
            var dependentList_D = new List<IEdge>();
            dependentList_D.Add(edgeF);
            EdgeList[3].DependentList = dependentList_D;
            (EdgeList[3] as Task).Timing.Duration = 1;
                (EdgeList[3] as Task).Timing.Est = 2;

                EdgeList.Add(edgeE);
                EdgeList[4].Id = 4;
                EdgeList[4].Name = "E";
                EdgeList[4].HeadNode = thirdNode;
                EdgeList[4].TailNode = fourthNode;
                EdgeList[4].DependsOnList.Add(edgeC);
            EdgeList[4].DependentList = dependentList_D;
                (EdgeList[4] as Task).Timing.Duration = 2;
                (EdgeList[4] as Task).Timing.Est = 2;

                EdgeList.Add(edgeF);
                EdgeList[5].Id = 5;
                EdgeList[5].Name = "F";
                EdgeList[5].HeadNode = fourthNode;
                EdgeList[5].TailNode = finishNode;
                EdgeList[5].DependentList = new List<IEdge>();
                EdgeList[5].DependsOnList.Add(edgeD); EdgeList[5].DependsOnList.Add(edgeE);
                (EdgeList[5] as Task).Timing.Duration = 1;
                (EdgeList[5] as Task).Timing.Est = 3;
            }
        }
}
