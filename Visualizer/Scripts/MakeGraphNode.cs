using Microsoft.Xna.Framework;
using MyEngine;

namespace Visualizer
{
    public class MakeGraphNode: GameObjectComponent
    {
        private GameObject[] Bubbles;
        private NameGenerator NodeNames;

        public override void Start()
        {
            Bubbles = SceneManager.ActiveScene.FindGameObjectsWithTag("Bubble");
            NodeNames = new NameGenerator("");
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.GetMouseClickDown(MouseButtons.LeftClick))
            {
                foreach (GameObject GO in Bubbles)
                {
                    if (GO.GetComponent<CircleCollider>().Contains(Input.GetMousePosition()))
                    {
                        if (Arrow.AnyOneActive == false)
                        {
                            if (GO.GetChild("GraphNode") == null)
                            {
                                int Radius = SceneManager.ActiveScene.FindGameObjectWithName("BubbleGenerator").GetComponent<GenerateBubbles>().Radius;

                                GameObject graphNode = new GameObject();
                                graphNode.AddComponent<Transform>(new Transform());
                                graphNode.AddComponent<SpriteRenderer>(new SpriteRenderer());
                                graphNode.AddComponent<CircleCollider>(new CircleCollider(Radius));
                                graphNode.AddComponent<GraphNode>(new GraphNode());

                                string Name = NodeNames.GenerateName();
                                Name = Name.Replace(" ", "");

                                GameObject TextGN = new GameObject();
                                TextGN.AddComponent<Transform>(new Transform());
                                TextGN.AddComponent<Text>(new Text(Name, Main.spriteFont));

                                graphNode.Start();
                                TextGN.Start();

                                TextGN.Layer = 0.1f;
                                TextGN.GetComponent<Text>().text = Name;
                                TextGN.GetComponent<Text>().Color = Color.Black;
                                graphNode.AddChild(TextGN);
                                TextGN.Transform.LocalPosition = Vector2.Zero;

                                graphNode.Tag = "GraphNode";
                                GO.AddChild(graphNode);
                                graphNode.GetComponent<GraphNode>().Radius = (int)(Radius * 0.9f);
                                graphNode.Transform.LocalPosition = Vector2.Zero;

                                SceneManager.ActiveScene.AddGameObject(TextGN);
                                SceneManager.ActiveScene.AddGameObject(graphNode);
                                SceneManager.ActiveScene.SortGameObjectsWithLayer();

                                break;
                            }
                            else if (!GenerateBubbles.I_Executed_First)
                            {
                                GameObject Arrow1 = new GameObject();
                                Arrow1.AddComponent<Transform>(new Transform());
                                Arrow1.AddComponent<Arrow>(new Arrow());

                                Arrow1.Start();

                                Arrow1.Tag = "Arrow";
                                GO.AddChild(Arrow1);
                                Arrow1.Transform.LocalPosition = Vector2.Zero;
                                Arrow1.GetComponent<Arrow>().Source = GO;

                                SceneManager.ActiveScene.AddGameObject(Arrow1);
                                SceneManager.ActiveScene.SortGameObjectsWithLayer();

                                break;
                            }
                        }
                    }
                }
            }
        }           
    }
}
