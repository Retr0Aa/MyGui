using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyGui;

namespace Example
{
    class Example
    {
        // Just start the program and you will see window with MyGui example
        public static void Main(String[] args)
        {
            Form form = new Form() {
                Size = new Size(1080, 800),
                Text = "MyGui Example"
            };

            MyGuiPanel exampleGui = new MyGuiPanel("Example Panel", new Size(200, 200), DockStyle.None, form);
            exampleGui.Init();

            exampleGui.addControl(new Label() { Text = "Example Text In MyGui" });

            Application.Run(form);
        }
    }
}
