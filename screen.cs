﻿namespace MouseLock
{

    class screen
    {
        private int width;
        private int height;
        private int zero = 0;
        public screen(int w, int h)
        {
            width = w;
            height = h;
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Zero
        {
            get { return zero; }
            set { zero = value; }
        }
     
    }
}
