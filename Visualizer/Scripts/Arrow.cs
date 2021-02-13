using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine;

namespace Visualizer
{
    public class Arrow: GameObjectComponent
    {
        public int Thickness = 5;
        public Color color = Color.GreenYellow;
        public static bool AnyOneActive = false;
        public GameObject Source = null;
        public GameObject Destination = null;

        private Vector2 EndPosition;
        private bool EditMode = true;
        private Rectangle Rect;
        private float Angle = 0;
        private Vector2 Origin = Vector2.Zero;

        public override void Start()
        {
            EndPosition = Vector2.Zero;
            Rect = new Rectangle(0, 0, 50, Thickness);
            gameObject.Layer = 0.8f;
        }

        public override void Update(GameTime gameTime)
        {
            if(EditMode)
            {
                AnyOneActive = true;

                if(Input.GetMouseClickDown(MouseButtons.RightClick))
                {
                    gameObject.ShouldBeDeleted = true;
                    return;
                }

                color = Color.White;
                EndPosition = Input.GetMousePosition();

                GameObject[] GNs = SceneManager.ActiveScene.FindGameObjectsWithTag("GraphNode");
                foreach (GameObject GN in GNs)
                    if (GN.GetComponent<CircleCollider>().Contains(Input.GetMousePosition()))
                    {
                        EndPosition = GN.Transform.Position;
                        color = Color.Red;

                        if (Input.GetMouseClickDown(MouseButtons.LeftClick))
                        {
                            EditMode = false;
                            GenerateBubbles.I_Executed_First = true;

                            Destination = GN;
                        }
                    }

                Rect.Width = (int)(EndPosition - gameObject.Transform.Position).Length();
                Rect.Height = Thickness;

                Angle = MathCompanion.GetAngle(EndPosition, gameObject.Transform.Position);
                Rect.Location = gameObject.Transform.Position.ToPoint();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            HitBoxDebuger.DrawLine(Rect, color, 180 + Angle, gameObject.Layer, Origin);
        }

        public override GameObjectComponent DeepCopy(GameObject Clone)
        {
            return this.MemberwiseClone() as Arrow;
        }
    }
}
