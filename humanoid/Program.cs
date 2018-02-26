using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace humanoid
{
    class Program
    {
        static void Main(string[] args)
        {
            myGraph g = new myGraph();

            Console.WriteLine("Step1-Node formation....");
            Console.WriteLine("Enter initial number of nodes...");
            int j = int.Parse(Console.ReadLine());

            for (int i = 0; i < j; i++)
            {
                vertex v = new vertex();
                Console.Write("Enter vertex weight...");
                v.Weigth = float.Parse(Console.ReadLine());
                g.addNode(v);
            }
            Console.Clear();
            Console.WriteLine("Step2-Graph Formation....");
            //foreach (vertex vi in g.GraphNode)
            //{
            //    foreach (vertex vc in g.GraphNode)
            //    {
            //        if (vi != vc)
            //        {
            //            Console.WriteLine("Is vertex ( " + vi.Weigth + " )is connected with vertex ( " + vc.Weigth+" ).....");
            //            if (Console.ReadLine().Equals("y"))
            //            {
            //                Console.Write("\n\n\nEnter edge weigth...");
            //                int weights = int.Parse(Console.ReadLine());
            //                vi.addEdge(weights, vc);
            //            }
            //            Console.WriteLine("---------------------------------------");
            //        }
            //    }
            //}

            List<vertex> vi = g.GraphNode;

            vi[0].addEdge(1, vi[1]);
            vi[0].addEdge(3, vi[2]);
            vi[1].addEdge(1, vi[0]);
            vi[1].addEdge(3, vi[5]);
            vi[1].addEdge(2, vi[3]);
            vi[2].addEdge(3, vi[0]);
            vi[2].addEdge(4, vi[4]);
            vi[3].addEdge(2, vi[1]);
            vi[3].addEdge(5, vi[4]);
            vi[4].addEdge(4, vi[2]);
            vi[4].addEdge(5, vi[3]);
            vi[4].addEdge(6, vi[5]);
            vi[5].addEdge(6, vi[4]);
            vi[5].addEdge(3, vi[1]);







            g.SetFirstSource();
            Console.WriteLine("\n=====================\n");
            Console.WriteLine("For stimuli 5");
            Console.WriteLine("\n=====================\n");
            g.stimuliOccur(5);

            foreach (vertex v in g.GraphNode)
            {
                g.traverse(v);
                Console.WriteLine("\n-------------------------------\n");
            }

            
            Console.WriteLine("\n=====================\n");
            Console.WriteLine("For stimuli 6");
            Console.WriteLine("\n=====================\n");
            g.stimuliOccur(6);

            foreach (vertex v in g.GraphNode)
            {
                g.traverse(v);
                Console.WriteLine("\n-------------------------------\n");
            }
            Console.ReadKey();
        }
    }
    //=============================
    class edge
    {
        float eWeigth;

        public float EWeigth
        {
            get { return eWeigth; }
            set { eWeigth = value; }
        }
        vertex connected;

        internal vertex Connected
        {
            get { return connected; }
            set { connected = value; }
        }
    }
    //==============================
    class vertex
    {
        float weigth;

        public float Weigth
        {
            get { return weigth; }
            set { weigth = value; }
        }
        List<edge> adjacencyList;
        bool isVisited, isDead;

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }


        public bool IsVisited
        {
            get { return isVisited; }
            set { isVisited = value; }
        }
        internal List<edge> AdjacencyList
        {
            get { return adjacencyList; }
            set { adjacencyList = value; }
        }
        public vertex()
        {
            adjacencyList = new List<edge>();
            this.IsNewBorn = true;
            this.isDead = false;
            this.isVisited = false;
        }

        public void addEdge(float weigth, vertex v)
        {
            edge e = new edge();
            e.EWeigth = weigth;
            e.Connected = v;
            adjacencyList.Add(e);
        }

        int totalVisits;

        public int TotalVisits
        {
            get { return totalVisits; }
            set { totalVisits = value; }
        }

        vertex parent;

        internal vertex Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        bool isNewBorn;

        public bool IsNewBorn
        {
            get { return isNewBorn; }
            set { isNewBorn = value; }
        }
    }
    //==============================
    class iputStimuliObj
    {
        float stimuli;

        public float Stimuli
        {
            get { return stimuli; }
            set { stimuli = value; }
        }

        vertex source;

        internal vertex Source
        {
            get { return source; }
            set { source = value; }
        }

    }
    //==============================
    class myGraph
    {
        List<vertex> graphNode;
        bool isDynamic;
        int totalVisits;
        vertex parentSource, startNode;
        internal List<vertex> GraphNode
        {
            get { return graphNode; }
            set { graphNode = value; }
        }
        public myGraph()
        {
            graphNode = new List<vertex>();
            this.isDynamic = false;
            this.totalVisits = 0;
        }
        public void SetFirstSource()
        {
            this.parentSource = this.graphNode.First();
            this.startNode = this.parentSource;
            parentSource.Parent = null;
        }
        public void addNode(vertex v)
        {
            graphNode.Add(v);
        }
        public void stimuliOccur(float i)
        {
            //var sort = this.startNode.AdjacencyList.OrderByDescending(x => x.EWeigth);
            //startNode = sort.First().Connected;
            if (this.totalVisits.Equals(0))
            {
                //Console.WriteLine("First Visit");
                this.totalVisits++;
                iputStimuliObj obj = new iputStimuliObj();
                obj.Stimuli = i;
                obj.Source = this.parentSource;
                obj.Source.IsVisited = true;
                if (this.parentSource.Weigth < i)
                    this.parentSource.Weigth = (i - this.parentSource.Weigth);
                else
                {
                    this.parentSource.Weigth += i;
                }
                this.minStimuli(obj);

            }
            else
            {
               
                this.clearGraph();
                this.totalVisits++;
                var sortedSource = this.startNode.AdjacencyList.OrderByDescending(x => x.EWeigth);
                float largest = sortedSource.First().EWeigth;
                //Console.WriteLine("Next parent" + sortedSource.First().Connected.Weigth);

                foreach (edge e in sortedSource)
                {
                    if (e.EWeigth.Equals(largest))
                    {

                        iputStimuliObj obj = new iputStimuliObj();
                        obj.Stimuli = i;
                        obj.Source = e.Connected;
                        e.Connected.IsVisited = true;
                        if (e.Connected.Weigth < i)
                        {
                            e.Connected.Weigth = (i - e.Connected.Weigth);
                        }
                        else
                        {
                            e.Connected.Weigth += i;
                        }
                        // Thread f = new Thread(this.minStimuli);
                        //f.Start(obj);
                        this.minStimuli(obj);
                    }

                }
            }

            

            if (!isDynamic)
            {
                //Console.WriteLine("Node not found creating now....");
                vertex c = this.addNewNode(i);
                this.graphNode.Add(c);
            }

           
            this.isDynamic = false;
            this.ClearDead();
        }
        public void minStimuli(object ob)
        {
            iputStimuliObj obj = (iputStimuliObj)ob;
            var sorted = obj.Source.AdjacencyList.OrderBy(x => x.EWeigth);
            float smallestWeight = 0;
            foreach (edge v in sorted)
            {
                if (!v.Connected.IsVisited)
                {
                    smallestWeight = v.EWeigth;
                    break;
                }
            }
            if (smallestWeight.Equals(0))
            {
                return;
            }
            foreach (edge c in sorted)
            {
                if (c.EWeigth.Equals(smallestWeight))
                {
                    parentChild p = new parentChild();
                    p.I = obj.Stimuli;
                    // Console.WriteLine("obj.Stimuli" + obj.Stimuli);
                    p.Parent = obj.Source;
                    p.Child1 = c;
                    //Console.WriteLine("Creating thread");
                    //Thread t = new Thread(this.traversel);
                    //t.Start(p);
                    // Console.WriteLine("Ending thread");
                    //t.Abort();

                    this.traversel(p);
                }

            }
        }
        public void ClearDead()
        {
            foreach (vertex v in this.graphNode)
            {
                float deadlyVisits = (v.TotalVisits / this.totalVisits) * 100;

                if (deadlyVisits < 20 && !v.IsNewBorn)
                    v.IsDead = true;
            }
        }
        public void traversel(object parentAndChild)
        {
            // Console.WriteLine("IN traversal");
            if (!isDynamic)
            {
                parentChild obj = (parentChild)parentAndChild;
                if (obj.Child1.Connected.IsVisited || obj.Child1.Connected.IsDead)
                {
                    //Console.WriteLine("Child visited");
                    return;
                }
                else
                {
                    obj.Child1.Connected.IsNewBorn = false;
                    obj.Child1.Connected.Parent = obj.Parent;
                    obj.Child1.Connected.TotalVisits++;
                    obj.Child1.Connected.IsVisited = true;
                    obj.Child1.EWeigth *= 2;
                    var li = obj.Child1.Connected.AdjacencyList;

                    foreach (edge e in li)
                    {
                        if (e.Connected == obj.Parent)
                            e.EWeigth *= 2;
                    }
                    iputStimuliObj i = new iputStimuliObj();
                    i.Source = obj.Child1.Connected;
                    i.Stimuli = obj.I;
                    if (obj.Child1.Connected.Weigth.Equals(obj.I))
                    {

                        //Console.WriteLine("\n===============================\n");
                        Console.WriteLine("Moving to dynamic on stilmuli " + obj.I + "\n====================");
                        this.isDynamic = true;
                        this.dynamicApproach(obj.I);
                    }
                    else if (obj.Child1.Connected.Weigth < obj.I)
                    {
                        //Console.WriteLine("child weigth is less..."+obj.Child1.Connected.Weigth);
                        obj.Child1.Connected.Weigth = (obj.I - obj.Child1.Connected.Weigth);
                        //Console.WriteLine("child weigth is less update..." + obj.Child1.Connected.Weigth);
                        //Console.WriteLine("Edge Weight" + obj.Child1.EWeigth);
                        this.minStimuli(i);
                    }
                    else
                    {
                        //Console.WriteLine("child weigth is greater" + obj.Child1.Connected.Weigth);
                        obj.Child1.Connected.Weigth += obj.I;
                        //Console.WriteLine("child weigth is greater update" + obj.Child1.Connected.Weigth);
                        //Console.WriteLine("Edge Weight" + obj.Child1.EWeigth);
                        this.minStimuli(i);
                    }

                }
            }
            return;
        }
        public void dynamicApproach(float i)
        {
            foreach (vertex v in this.graphNode)
            {
                v.IsVisited = false;
                //Console.WriteLine(v.Weigth);
                //foreach (edge e in v.AdjacencyList)
                //{
                //    Console.Write(e.EWeigth);
                //}
                //Console.WriteLine("\n-------------------------");
            }

            if (this.parentSource.Weigth >= i)
            {

                this.parentSource.Weigth += i;

                //Console.WriteLine("updating first parent..."+this.parentSource.Weigth);
            }
            else
            {
                this.parentSource.Weigth = (this.parentSource.Weigth - i);
            }
            this.parentSource.IsVisited = true;
            this.parentSource.Parent = null;
            this.parentSource.TotalVisits++;
            this.threadedTraverse(this.parentSource);
        }
        public void threadedTraverse(vertex obj)
        {
            var sorted = obj.AdjacencyList.OrderBy(x => x.EWeigth);

            float smallest = 0;

            foreach (edge v in sorted)
            {
                if (!v.Connected.IsVisited)
                {
                    smallest = v.EWeigth;
                    break;
                }

            }



            foreach (edge e in sorted)
            {
                if (e.EWeigth.Equals(smallest))
                {
                    parentChild p = new parentChild();
                    p.Parent = obj;
                    p.Child1 = e;
                    p.I = 0;
                    // Console.WriteLine("Creating thread in threaded traverse");
                    // Thread t = new Thread(this.dynamicTraverse);
                    //t.Start(p);
                    //Console.WriteLine("Ending thread");
                    //t.Abort();

                    this.dynamicTraverse(p);
                }

            }
        }
        public void dynamicTraverse(object obj)
        {
            parentChild p = (parentChild)obj;

            if (p.Child1.Connected.IsVisited || p.Child1.Connected.IsDead)
            {
                //Console.WriteLine("already visted:" + p.Child1.Connected.Weigth);
                return;
            }
            else
            {
                p.Child1.Connected.Parent = p.Parent;
                p.Child1.Connected.IsNewBorn = false;
                //Console.WriteLine("Parent is " + p.Parent.Weigth + " child is " + p.Child1.Connected.Weigth+" edge weigth "+p.Child1.EWeigth);
                p.Child1.Connected.IsVisited = true;
                p.Child1.EWeigth *= 2;
                var li = p.Child1.Connected.AdjacencyList;

                foreach (edge e in li)
                {
                    if (e.Connected == p.Parent)
                        e.EWeigth *= 2;
                }
                //Console.WriteLine("Edge in dyamic " + p.Child1.EWeigth);
                p.Child1.Connected.TotalVisits++;
                if (p.Child1.Connected.Weigth < p.Parent.Weigth)
                {
                    p.Child1.Connected.Weigth = (p.Parent.Weigth - p.Child1.Connected.Weigth);

                }
                else
                {
                    p.Child1.Connected.Weigth += p.Parent.Weigth;
                }

                this.threadedTraverse(p.Child1.Connected);
            }
        }
        public vertex addNewNode(float i)
        {
            var sort = this.graphNode.OrderBy(x => x.Weigth);

            foreach (vertex v in sort)
            {
                foreach (vertex v1 in sort)
                {
                    if (v.Weigth - v1.Weigth > i)
                    {
                        return this.Combine(i, v, v1);

                    }

                }
            }


            var largest = this.graphNode.OrderByDescending(x => x.Weigth);
            vertex vl = largest.First();
            return this.CombineWithLarget(i, vl);



        }
        public vertex Combine(float i, vertex source1, vertex source2)
        {
            Console.WriteLine("============Adding New Node between=============");
            Console.WriteLine("     source1= " + source1.Weigth + " source2= " + source2.Weigth);
            Console.WriteLine("=================================");
            vertex newComer = new vertex();
            newComer.Weigth = i;
            var s1 = source1.AdjacencyList.OrderBy(x => x.EWeigth);

            float s1Average = 0;
            float s1Weight1 = s1.ElementAt(0).EWeigth;
            float s1Weight2 = s1.ElementAt(1).EWeigth;
            // Console.WriteLine("w1 " + s1Weight1 + " w2 " + s1Weight2);
            var s2 = source2.AdjacencyList.OrderBy(x => x.EWeigth);
            float s2Average = 0;

            float s2Weigth1 = s2.ElementAt(0).EWeigth;
            float s2Weigth2 = s2.ElementAt(1).EWeigth;
            // Console.WriteLine("w1 " + s2Weigth1 + " w2 " + s2Weigth2);
            s1Average = (s1Weight1 + s1Weight2) / 2;
            s2Average = (s2Weigth1 + s2Weigth2) / 2;
            //Console.WriteLine("S1 " + s1Average + " S2 " + s2Average);
            newComer.addEdge(s1Average, source1);
            newComer.addEdge(s2Average, source2);
            source1.addEdge(s1Average, newComer);
            source2.addEdge(s2Average, newComer);
            newComer.Parent = null;
            return newComer;
            //this.addNode(newComer);
        }
        public vertex CombineWithLarget(float i, vertex source)
        {
            vertex v = new vertex();
            v.Weigth = i;
            Console.WriteLine("=========Attaching Node with largest=========");
            Console.WriteLine("sourec " + source.Weigth);
            Console.WriteLine("==============================");
            var sorted = source.AdjacencyList.OrderBy(x => x.EWeigth);

            float average = 0;
            float s1Weight = sorted.ElementAt(0).EWeigth;
            float s2Weight = sorted.ElementAt(1).EWeigth;

            average = (s1Weight + s2Weight) / 2;

            v.addEdge(average, source);
            source.addEdge(average, v);
            v.Parent = null;

            return v;
            //this.addNode(v);
        }
        public void traverse(vertex v)
        {
            if (v == null)
                return;
            else
            {
                traverse(v.Parent);
                Console.Write(v.Weigth + "->");
            }
        }
        public void clearGraph()
        {
            foreach (vertex v in this.graphNode)
                v.IsVisited = false;

            this.parentSource.IsVisited = true;
        }
    }



    class parentChild
    {
        vertex parent;

        internal vertex Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        edge Child;

        internal edge Child1
        {
            get { return Child; }
            set { Child = value; }
        }



        float i;

        public float I
        {
            get { return i; }
            set { i = value; }
        }
    }
}
