// Learn more about F# at http://fsharp.org

open System
open System.Numerics
open System.Drawing
open System.Windows.Forms
open System.Windows.Forms



let mandelFunc (c:Complex) (z:Complex) = z**3.0 + c
//let mandelFunc (c:Complex) (z:Complex) = z*Complex.Tan(z+c)

let rec loop f iter = function
   | 0 -> iter
   | n -> loop f (f iter) (n - 1) 



let isConvergence c = Complex.Abs(loop (mandelFunc c) Complex.Zero 70) < 2.0

let scale (x:float, y:float) (u,v) n = float(n-u) / float(v-u) * (y-x) + x

let colorize c =
    let r = (4 * c) % 255
    let g = (6 * c) % 255
    let b = (8 * c) % 255
    Color.FromArgb(r,g,b)

let form =
   let image = new Bitmap(600, 600)
   let a = scale (-1.2,1.2)(0,image.Height-1)
   for i = 0 to (image.Height-1) do
      for j = 0 to (image.Width-1) do
         let t = Complex (a i, a j) in
         image.SetPixel(i,j,if isConvergence t then Color.Black else Color.White)
   let form = new Form()
   let width = (float32) image.Width
   let height = (float32) image.Height
   let destinationRect = new RectangleF(0.0f, 0.0f, width, height)
   let sourceRect = new RectangleF((float32)Cursor.Position.X-120.0f,
                                   (float32)Cursor.Position.Y-120.0f, 0.4f * width, 0.4f * height)
   //form.Paint.Add(fun e -> e.Graphics.DrawImage(image, 0, 0))
   form.Paint.Add(fun e -> e.Graphics.DrawImage(image, destinationRect, sourceRect, GraphicsUnit.Pixel))

do Application.Run(form)