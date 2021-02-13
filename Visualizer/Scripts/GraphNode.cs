using Microsoft.Xna.Framework;
using MyEngine;

namespace Visualizer
{
    public class GraphNode: GameObjectComponent
    {
        public int Radius
        {
            set
            {
                radius = (int)MathCompanion.Clamp(value, 1, Setup.graphics.PreferredBackBufferWidth/2);
                gameObject.GetComponent<SpriteRenderer>().Sprite.Texture = HitBoxDebuger.CreateCircleTexture(radius, color);
                gameObject.GetComponent<SpriteRenderer>().Sprite.Origin = radius * Vector2.One;
                gameObject.GetComponent<SpriteRenderer>().Color = color;
            }
            get
            {
                return radius;
            }
        }
        public Color color = Color.White;

        //private static NameGenerator nameGenerator;
        private int radius;

        static GraphNode()
        {
            //nameGenerator = new NameGenerator("Graph Node");
        }

        public override void Start()
        {
            //gameObject.Name = nameGenerator.GenerateName();
            gameObject.Name = "Graph Node 0";
            gameObject.Layer = 0.25f;
            gameObject.Tag = "GraphNode";
        }

        public override GameObjectComponent DeepCopy(GameObject Clone)
        {
            return this.MemberwiseClone() as GraphNode;
        }
    }
}