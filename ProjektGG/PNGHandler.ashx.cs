using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjektGG
{
    /// <summary>
    /// Opis podsumowujący dla PNGHandler
    /// </summary>
    public class PNGHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "image/png";
            if (File.Exists(context.Request.PhysicalPath))
            {
                Image image = Image.FromFile(context.Request.PhysicalPath);
                Image mrkImg = Image.FromFile(@"C:\Users\wiedy\source\repos\ProjektGG\ProjektGG\Images\watermark.png");
                Graphics g = Graphics.FromImage(image);
                float width = (image.Width / 4);
                float height = (width / mrkImg.Width) * mrkImg.Height;
                g.DrawImage(mrkImg, 100, 100, width, height);
                using (MemoryStream mStream = new MemoryStream())
                {
                    image.Save(mStream, image.RawFormat);
                    mStream.WriteTo(context.Response.OutputStream);
                }
            }
            else
            {
                context.Response.ContentType = "text /plain";
                context.Response.Write("Podany obrazek nie istnieje");
            }
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}