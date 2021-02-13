using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine;

namespace Visualizer
{
    public class GenerateBubbles: GameObjectComponent
    {
        public int Radius { get; }
        public static bool I_Executed_First = false;

        private Vector2 Dimensions;
        private int Spacing;
        private Color color = Color.DimGray;
        private Vector2 FinalDimensions;
        private SpriteFont SF;

        public GenerateBubbles(int Spacing, int Radius, SpriteFont SF)
        {
            this.SF = SF;
            this.Radius = Radius;
            this.Spacing = Spacing;
            int NumberToFit = 1;

            int WidthTillNow = Radius * 2;

            while(WidthTillNow <= Setup.graphics.PreferredBackBufferWidth)
            {
                Dimensions.X = NumberToFit;
                FinalDimensions.X = WidthTillNow;
                WidthTillNow += Spacing + Radius * 2;
                NumberToFit++;
            }

            WidthTillNow = Radius * 2;
            NumberToFit = 1;
            while (WidthTillNow <= Setup.graphics.PreferredBackBufferHeight)
            {
                Dimensions.Y = NumberToFit;
                FinalDimensions.Y = WidthTillNow;
                WidthTillNow += Spacing + Radius * 2;
                NumberToFit++;
            }
        }

        public override void Start()
        {
            GameObject Bubble = new GameObject();
            Bubble.Tag = "Bubble";
            Bubble.AddComponent<Transform>(new Transform());
            Bubble.AddComponent<SpriteRenderer>(new SpriteRenderer());
            Bubble.AddComponent<CircleCollider>(new CircleCollider(Radius));
            //Bubble.AddComponent<Text>(new Text("0", SF));
            Bubble.Layer = 0.75f;

            Bubble.Start();

            Bubble.GetComponent<SpriteRenderer>().Sprite.Texture = HitBoxDebuger.CreateCircleTextureShell(Radius, (int)(Radius * 0.9f), color);
            Bubble.GetComponent<SpriteRenderer>().Sprite.SetCenterAsOrigin();
            Bubble.Transform.Position = new Vector2(Setup.graphics.PreferredBackBufferWidth - FinalDimensions.X + Radius*2, Setup.graphics.PreferredBackBufferHeight - FinalDimensions.Y + Radius*2) / 2;
            //Bubble.GetComponent<Text>().Color = Color.Black;

            NameGenerator NG = new NameGenerator("");

            for (int i=0; i<Dimensions.Y; i++)
            {
                for (int j = 0; j < Dimensions.X; j++)
                {
                    GameObject Instance = GameObject.Instantiate(Bubble);
                    //Instance.GetComponent<Text>().text = NG.GenerateName().Replace(" ", "");
                    Instance.Transform.Position += new Vector2(j * (Radius * 2 + Spacing), i * (Radius * 2 + Spacing));
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(!Arrow.AnyOneActive)
                foreach (GameObject GO in SceneManager.ActiveScene.FindGameObjectsWithTag("Bubble"))
                {
                    if (GO.GetComponent<CircleCollider>().Contains(Input.GetMousePosition()))
                    {
                        GO.GetComponent<SpriteRenderer>().Color = Color.Black;
                        break;
                    }
                    else
                        GO.GetComponent<SpriteRenderer>().Color = Color.White;
                }
        }
    }
}
