using UnityEngine;

namespace Erase
{
    public class TextureHelpers
    {
        private static void LineX(Texture2D tex,int x0, int y0, int xend, int yend,Color color)
        {
            int x, y, dx = Mathf.Abs(xend - x0), dy = Mathf.Abs(yend - y0), p = 2 * dy - dx;
            if (x0 > xend)
            {
                x = xend;
                y = yend;
                xend = x0;
                yend = y0;
            }
            else
            {
                x = x0;
                y = y0;
            }
            bool k = y < yend;
            tex.SetPixel(x, y,color);
            while (x < xend)
            {
                x++;
                if (p < 0)
                {
                    p += 2 * dy;
                }
                else
                {
                    if (k)
                        y++;
                    else y--;
                    p += 2 * (dy - dx);
                }
                tex.SetPixel(x, y,color);
            }
        }
        private static void BoldLineX(Texture2D tex,int x0, int y0, int xend, int yend,int width,Color color)
        {
            int x, y, dx = Mathf.Abs(xend - x0), dy = Mathf.Abs(yend - y0), p = 2 * dy - dx;
            float tangent = (float)dy / dx;
            
            var dx1 = width * tangent / Mathf.Sqrt(1 + tangent * tangent);
            var dy1 = width / Mathf.Sqrt(1 + tangent * tangent);
            int x1, y1, x1End, y1End;
            
            if (x0 > xend)
            {
                
                x = xend;
                y = yend;
                xend = x0;
                yend = y0;
            }
            else
            {
                x = x0;
                y = y0;
            }

            if (y>yend)
            {
                x1 = x + (int)dx1;
                y1 = y + (int)dy1;
                x1End = x - (int)dx1;
                y1End = y - (int)dy1;
            }
            else
            {
                x1 = x + (int) dx1;
                y1 = y - (int) dy1;
                x1End = x - (int) dx1;
                y1End = y + (int) dy1;
            }

            bool k = y < yend;

            LineNoApply(tex,x1,y1, x1End,y1End,color);
            LineNoApply(tex,x1+1,y1, x1End+1,y1End,color);
            LineNoApply(tex,x1-1,y1, x1End-1,y1End,color);
            while (x < xend)
            {
                x++;
                
                x1++;
                x1End++;
                if (p < 0)
                {
                    p += 2 * dy;
                }
                else
                {
                    if (k)
                    {
                        y++;
                        
                        y1++;
                        y1End++;
                    }
                    else
                    {
                        y--;
                        
                        y1--;
                        y1End--;
                    }
                    p += 2 * (dy - dx);
                }
                LineNoApply(tex,x1,y1, x1End,y1End,color);
                LineNoApply(tex,x1+1,y1, x1End+1,y1End,color);
                LineNoApply(tex,x1-1,y1, x1End-1,y1End,color);
            }
        }

        private static void LineY(Texture2D tex, int x0, int y0, int xend, int yend, Color color)
        {
            int x, y, dx = Mathf.Abs(xend - x0), dy = Mathf.Abs(yend - y0), p = 2 * dy - dx;
            if (x0 > xend)
            {
                x = xend;
                y = yend;
                xend = x0;
                yend = y0;
            }
            else
            {
                x = x0;
                y = y0;
            }

            bool k = y < yend;
            tex.SetPixel(y, x, color);
            while (x < xend)
            {
                x++;
                if (p < 0)
                {
                    p += 2 * dy;
                }
                else
                {
                    if (k)
                        y++;
                    else y--;
                    p += 2 * (dy - dx);
                }

                tex.SetPixel(y, x, color);
            }
        }
        private static void BoldLineY(Texture2D tex,int x0, int y0, int xend, int yend,int width,Color color)
        {
            int x, y, dx = Mathf.Abs(xend - x0), dy = Mathf.Abs(yend - y0), p = 2 * dy - dx;
            float tangent = (float)dy / dx;
            
            var dx1 = width * tangent / Mathf.Sqrt(1 + tangent * tangent);
            var dy1 = width / Mathf.Sqrt(1 + tangent * tangent);
            int x1, y1, x1End, y1End;
            if (x0 > xend)
            {
                x = xend;
                y = yend;
                xend = x0;
                yend = y0;
            }
            else
            {
                x = x0;
                y = y0;
            }
            bool k = y < yend;
            
            if (y>yend)
            {
                x1 = x + (int)dx1;
                y1 = y + (int)dy1;
                x1End = x - (int)dx1;
                y1End = y - (int)dy1;
            }
            else
            {
                x1 = x + (int) dx1;
                y1 = y - (int) dy1;
                x1End = x - (int) dx1;
                y1End = y + (int) dy1;
            }
            
            LineNoApply(tex,y1,x1, y1End,x1End,color);
            LineNoApply(tex,y1+1,x1, y1End+1,x1End,color);
            LineNoApply(tex,y1-1,x1, y1End-1,x1End,color);
            
            while (x < xend)
            {
                x++;
                
                x1++;
                x1End++; 
                if (p < 0)
                {
                    p += 2 * dy;
                }
                else
                {
                    if (k)
                    {
                        y++;
                        
                        y1++;
                        y1End++;
                        
                    }
                    else
                    {
                        y--;
                        
                        y1--;
                        y1End--;
                    }
                    p += 2 * (dy - dx);
                }
                LineNoApply(tex,y1,x1, y1End,x1End,color);
                LineNoApply(tex,y1+1,x1, y1End+1,x1End,color);
                LineNoApply(tex,y1-1,x1, y1End-1,x1End,color);
            }
        }
        
        private static void LineNoApply(Texture2D tex,int x0, int y0, int xend, int yend,Color color)
        {
            int dx = x0 - xend, dy = y0 - yend;
            if (Mathf.Abs(dx) > Mathf.Abs(dy))
                LineX(tex, x0, y0, xend, yend, color);
            else LineY(tex, y0, x0, yend, xend, color);
        }

        public static void LineNoApply(Texture2D tex,int x0, int y0, int xend, int yend,int width,Color color)
        {
            if(x0 == xend && y0 == yend)
                return;
            int dx = x0 - xend, dy = y0 - yend;
            if (Mathf.Abs(dx) > Mathf.Abs(dy))
            {
                BoldLineX(tex, x0, y0, xend, yend, width, color);
            }
            else
            {
                BoldLineY(tex, y0, x0, yend, xend,width, color);
            }
            CircleNoApply(tex,new Vector2(x0,y0),width,Color.clear);
            CircleNoApply(tex,new Vector2(xend,yend),width,Color.clear);
        }
        private static void CircleNoApply(Texture2D tex,Vector2 circleCenter, int circleRadius ,Color color)
        {
            for (int i = (int)circleCenter.x - circleRadius; i < (int)circleCenter.x + circleRadius; i++)
            {
                for (int j = (int)circleCenter.y - circleRadius; j < (int)circleCenter.y + circleRadius; j++)
                {
                    var x = i - (int)circleCenter.x;
                    var y = j - (int)circleCenter.y;
                    if (x * x + y * y < 
                        circleRadius * circleRadius)
                        tex.SetPixel(i, j, color);
                }
            }
        }
    }
}