using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DM
{
    public class CFlake
    {

        public Size Size;
        public Point Position;
        public Image Image;
        public int Speed;
        public int Direction;
        public CFlake()
        {
            Speed = 30;
            Direction = 50;
        }
        public Rectangle Bounds
        {
            get { return new Rectangle(this.Position, this.Size); }
            set
            {
                this.Position = value.Location;
                this.Size = value.Size;
            }
        }
        public int Top
        {
            get { return this.Position.Y; }
            set { this.Position.Y = value; }
        }
        public int Left
        {
            get { return this.Position.X; }
            set { this.Position.X = value; }
        }
        public int Right
        {
            get { return this.Position.X + this.Size.Width; }
        }
    }
}
